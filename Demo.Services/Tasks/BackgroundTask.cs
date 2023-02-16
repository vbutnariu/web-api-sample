using Microsoft.Extensions.Logging;
using Demo.Core.Caching;
using Demo.Core.DomainModel;
using Demo.Core.Infrastructure;
using System;
using System.Linq;

namespace Demo.Services.Tasks
{
	/// <summary>
	/// Task
	/// </summary>
	/// <returns>A task that represents the asynchronous operation</returns>
	public partial class BackgroundTask
    {
        #region Fields

        private bool? _enabled;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor for Task
        /// </summary>
        /// <param name="task">Task </param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public BackgroundTask(ScheduleTask task)
        {
            ScheduleTask = task;
        }

        #endregion

        #region Utilities



        /// <summary>
        /// Initialize and execute task
        /// </summary>
        private void ExecuteTask()
        {
            var scheduleTaskService = WebEngineContext.Current.Resolve<IScheduleTaskService>();
            ScheduleTask.LastStartUtc = DateTime.UtcNow;

            if (!Enabled)
                return;

            var type = Type.GetType(ScheduleTask.Type) ??
                //ensure that it works fine when only the type name is specified (do not require fully qualified names)
                AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => a.GetType(ScheduleTask.Type))
                .FirstOrDefault(t => t != null);
            if (type == null)
                throw new Exception($"Schedule task ({ScheduleTask.Type}) cannot by instantiated");

            object instance = null;
            try
            {
                instance = WebEngineContext.Current.Resolve(type);
            }
            catch
            {
                //try resolve
            }

            if (instance == null)
                //not resolved
                instance = WebEngineContext.Current.ResolveUnregistered(type);

            if (!(instance is  IScheduleTask task))
                return;

            //update appropriate datetime properties
            scheduleTaskService.UpdateTaskAsync(ScheduleTask).Wait();
            task.ExecuteAsync().Wait();
            ScheduleTask.LastEndUtc = ScheduleTask.LastSuccessUtc = DateTime.UtcNow;
            //update appropriate datetime properties
            scheduleTaskService.UpdateTaskAsync(ScheduleTask).Wait();
        }

        /// <summary>
        /// Is task already running?
        /// </summary>
        /// <param name="scheduleTask">Schedule task</param>
        /// <returns>Result</returns>
        protected virtual bool IsTaskAlreadyRunning(ScheduleTask scheduleTask)
        {
            //task run for the first time
            if (!scheduleTask.LastStartUtc.HasValue)
                return false;

            var lastStartUtc = scheduleTask.LastStartUtc ?? DateTime.UtcNow;

            //task already finished
            if (scheduleTask.LastEndUtc.HasValue && lastStartUtc < scheduleTask.LastEndUtc)
                return false;

            //task wasn't finished last time
            if (lastStartUtc.AddSeconds(scheduleTask.Seconds) <= DateTime.UtcNow)
                return false;

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="throwException">A value indicating whether exception should be thrown if some error happens</param>
        /// <param name="ensureRunOncePerPeriod">A value indicating whether we should ensure this task is run once per run period</param>
        public async System.Threading.Tasks.Task ExecuteAsync(bool throwException = false, bool ensureRunOncePerPeriod = true)
        {
            if (ScheduleTask == null || !Enabled)
                return;

            if (ensureRunOncePerPeriod)
            {
                //task already running
                if (IsTaskAlreadyRunning(ScheduleTask))
                    return;

                //validation (so nobody else can invoke this method when he wants)
                if (ScheduleTask.LastStartUtc.HasValue && (DateTime.UtcNow - ScheduleTask.LastStartUtc).Value.TotalSeconds < ScheduleTask.Seconds)
                    //too early
                    return;
            }

            try
            {
                //get expiration time
                var expirationInSeconds = Math.Min(ScheduleTask.Seconds, 300) - 1;
                var expiration = TimeSpan.FromSeconds(expirationInSeconds);
                var locker = WebEngineContext.Current.Resolve<ILocker>();
                locker.PerformActionWithLock(ScheduleTask.Type, expiration, ExecuteTask);

            }
            catch (Exception exc)
            {
                var scheduleTaskService = WebEngineContext.Current.Resolve<IScheduleTaskService>();
                //var storeContext = WebEngineContext.Current.Resolve<IStoreContext>();
               // var scheduleTaskUrl = $"{(await storeContext.GetCurrentStoreAsync()).Url}{NopTaskDefaults.ScheduleTaskPath}";

                ScheduleTask.Enabled = !ScheduleTask.StopOnError;
                ScheduleTask.LastEndUtc = DateTime.UtcNow;
                await scheduleTaskService.UpdateTaskAsync(ScheduleTask);

                //var message = string.Format( "Error executing task {0} {1} {2}", ScheduleTask.Name,
                //    exc.Message, ScheduleTask.Type, (await storeContext.GetCurrentStoreAsync()).Name, scheduleTaskUrl);

                var message = exc.Message;

                //log error
                var logger = WebEngineContext.Current.Resolve<ILogger<BackgroundTask>>();
                logger.LogError(exc, message);
                if (throwException)
                    throw;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Schedule task
        /// </summary>
        public ScheduleTask ScheduleTask { get; }

        /// <summary>
        /// A value indicating whether the task is enabled
        /// </summary>
        public bool Enabled
        {
            get
            {
                if (!_enabled.HasValue)
                    _enabled = ScheduleTask?.Enabled;

                return _enabled.HasValue && _enabled.Value;
            }

            set => _enabled = value;
        }

        #endregion
    }
}
