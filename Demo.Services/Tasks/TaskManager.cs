using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Demo.Core.Infrastructure;

namespace Demo.Services.Tasks
{
    /// <summary>
    /// Represents task manager responsible to execute scheduled tasks
    /// </summary>
    /// <returns>A
    public partial class TaskManager
    {
        #region Fields

        private readonly List<TaskThread> taskThreads = new List<TaskThread>();

        #endregion

        #region Ctor

        /// <returns>A task that represents the asynchronous operation</returns>
        private TaskManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the task manager
        /// </summary>
        public void Initialize()
        {
            taskThreads.Clear();

            var serviceScopeFactory = WebEngineContext.Current.Resolve<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {

                var taskService = WebEngineContext.Current.Resolve<IScheduleTaskService>(scope);
                var random = new Random();

                var scheduleTasks = taskService
                    .GetAllTasksAsync().Result
                    .OrderBy(x => x.Seconds)
                    .ToList();

                foreach (var scheduleTask in scheduleTasks)
                {
                    var taskThread = new TaskThread
                    {
                        Seconds = scheduleTask.Seconds
                    };

                    //sometimes a task period could be set to several hours (or even days)
                    //in this case a probability that it'll be run is quite small (an application could be restarted)
                    //calculate time before start an interrupted task
                    if (scheduleTask.LastStartUtc.HasValue)
                    {
                        //seconds left since the last start
                        var secondsLeft = (DateTime.UtcNow - scheduleTask.LastStartUtc).Value.TotalSeconds;

                        if (secondsLeft >= scheduleTask.Seconds)
                            //run now (immediately)
                            taskThread.InitSeconds = 0;
                        else
                            //calculate start time
                            //and round it (so "ensureRunOncePerPeriod" parameter was fine)
                            taskThread.InitSeconds = (int)(scheduleTask.Seconds - secondsLeft) + 1;
                    }
                    else
                    {
                        //first start of a task use a random between 30 and 60 seconds
                        taskThread.InitSeconds = random.Next(30, 60);
                    }

                    taskThread.AddTask(scheduleTask);
                    taskThreads.Add(taskThread);
                }
            }
        }


        /// <summary>
        /// Starts the task manager
        /// </summary>
        public void Start()
        {
            foreach (var taskThread in taskThreads)
            {
                taskThread.InitTimer();
            }
        }

        /// <summary>
        /// Stops the task manager
        /// </summary>
        public void Stop()
        {
            foreach (var taskThread in taskThreads)
            {
                taskThread.Dispose();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the task manger instance
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public static TaskManager Instance { get; } = new TaskManager();

        /// <summary>
        /// Gets a list of task threads of this task manager
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public IList<TaskThread> TaskThreads => new ReadOnlyCollection<TaskThread>(taskThreads);

        #endregion
    }
}
