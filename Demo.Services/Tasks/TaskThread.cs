using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Demo.Core.DomainModel;
using Demo.Core.Infrastructure;
using Demo.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Services.Tasks
{
	/// <summary>
	/// Represents task thread
	/// </summary>
	/// <returns>A task that represents the asynchronous operation</returns>
	public partial class TaskThread : IDisposable
    {
        #region Fields

        private static readonly string scheduleTaskUrl;
        private static readonly int? timeout;

        private readonly Dictionary<string, string> tasks;
        private Timer timer;
        private bool disposed;

        #endregion

        #region Ctor

        static TaskThread()
        {
            scheduleTaskUrl = $"{WebEngineContext.Current.GetHostUrl().TrimEnd('/')}/{PmTaskDefaults.ScheduleTaskPath}";
            timeout = 200;//WebEngineContext.Current.Resolve<>().CommonConfig.ScheduleTaskRunTimeout;
        }

        internal TaskThread()
        {
            tasks = new Dictionary<string, string>();
            Seconds = 10 * 60;
        }

        #endregion

        #region Utilities

        private async System.Threading.Tasks.Task RunAsync()
        {

            if (Seconds <= 0 || !WebEngineContext.Current.ApplicationStarted)
                return;

           

            StartedUtc = DateTime.UtcNow;
            IsRunning = true;
            HttpClient client = null;

            var serviceScopeFactory = WebEngineContext.Current.Resolve<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                // Resolve
                var logger = WebEngineContext.Current.Resolve<ILogger<TaskThread>>(scope);

                foreach (var taskName in tasks.Keys)
                {
                    var taskType = tasks[taskName];
                    try
                    {
                        //create and configure client
                        client = WebEngineContext.Current.CreateHttpClient();
                        if (timeout.HasValue)
                            client.Timeout = TimeSpan.FromSeconds(timeout.Value);

                        //send post data
                        var data = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>(nameof(taskType), taskType) });
                        await client.PostAsync(scheduleTaskUrl, data);

                    }
                    catch (Exception ex)
                    {

                        var localizationService = WebEngineContext.Current.Resolve<ILocalizationService>(scope);
                        var message = ex.InnerException?.GetType() == typeof(TaskCanceledException) ? localizationService.LocalizeString("ScheduleTasks.TimeoutError") : ex.Message;
                        logger.LogError(ex, message);
                    }
                    finally
                    {
                        if (client != null)
                        {
                            client.Dispose();
                            client = null;
                        }
                    }
                }
            }

            IsRunning = false;
        }

        private void TimerHandler(object state)
        {
            try
            {
                timer.Change(-1, -1);

                RunAsync().Wait();
            }
            catch
            {
                // ignore
            }
            finally
            {
                if (RunOnlyOnce)
                    Dispose();
                else
                    timer.Change(Interval, Interval);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Disposes the instance
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                lock (this)
                    timer?.Dispose();

            disposed = true;
        }

        /// <summary>
        /// Inits a timer
        /// </summary>
        public void InitTimer()
        {
            if (timer == null)
                timer = new Timer(TimerHandler, null, InitInterval, Interval);
        }

        /// <summary>
        /// Adds a task to the thread
        /// </summary>
        /// <param name="task">The task to be added</param>
        public void AddTask(ScheduleTask task)
        {
            if (!tasks.ContainsKey(task.Name))
                tasks.Add(task.Name, task.Type);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the interval in seconds at which to run the tasks
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Get or set the interval before timer first start 
        /// </summary>
        public int InitSeconds { get; set; }

        /// <summary>
        /// Get or sets a datetime when thread has been started
        /// </summary>
        public DateTime StartedUtc { get; private set; }

        /// <summary>
        /// Get or sets a value indicating whether thread is running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the interval (in milliseconds) at which to run the task
        /// </summary>
        public int Interval
        {
            get
            {
                //if somebody entered more than "2147483" seconds, then an exception could be thrown (exceeds int.MaxValue)
                var interval = Seconds * 1000;
                if (interval <= 0)
                    interval = int.MaxValue;
                return interval;
            }
        }

        /// <summary>
        /// Gets the due time interval (in milliseconds) at which to begin start the task
        /// </summary>
        public int InitInterval
        {
            get
            {
                //if somebody entered less than "0" seconds, then an exception could be thrown
                var interval = InitSeconds * 1000;
                if (interval <= 0)
                    interval = 0;
                return interval;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the thread would be run only once (on application start)
        /// </summary>
        public bool RunOnlyOnce { get; set; }

        #endregion
    }
}