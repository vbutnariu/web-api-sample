using Microsoft.EntityFrameworkCore;
using Demo.Core.Data;
using Demo.Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services.Tasks
{
	/// <summary>
	/// Task service
	/// </summary>
	public partial class ScheduleTaskService : IScheduleTaskService
    {
        #region Fields

        private readonly IRepository<ScheduleTask> taskRepository;

        #endregion

        #region Ctor

        public ScheduleTaskService(IRepository<ScheduleTask> taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a task
        /// </summary>
        /// <param name="task">Task</param>
        public virtual async System.Threading.Tasks.Task DeleteTaskAsync(ScheduleTask task)
        {
            await taskRepository.DeleteAsync(task);
        }

        /// <summary>
        /// Gets a task
        /// </summary>
        /// <param name="taskId">Task identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the schedule task
        /// </returns>
        public virtual async Task<ScheduleTask> GetTaskByIdAsync(Guid taskId)
        {
            return await taskRepository.GetByIdAsync(taskId);
        }

        /// <summary>
        /// Gets a task by its type
        /// </summary>
        /// <param name="type">Task type</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the schedule task
        /// </returns>
        public virtual async Task<ScheduleTask> GetTaskByTypeAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return null;

            var query = taskRepository.Table;
            query = query.Where(st => st.Type == type);
            query = query.OrderByDescending(t => t.Id).Select(t=>t);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all tasks
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of schedule task
        /// </returns>
        public virtual async Task<IList<ScheduleTask>> GetAllTasksAsync(bool showHidden = false)
        {
            var tasks = await taskRepository.GetAllAsync(query =>
            {
                if (!showHidden) 
                    query = query.Where(t => t.Enabled);

                query = query.OrderByDescending(t => t.Seconds);

                return query;
            });

            return tasks;
        }

        /// <summary>
        /// Inserts a task
        /// </summary>
        /// <param name="task">Task</param>
        public virtual async System.Threading.Tasks.Task InsertTaskAsync(ScheduleTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            await taskRepository.InsertAsync(task);
        }

        /// <summary>
        /// Updates the task
        /// </summary>
        /// <param name="task">Task</param>
        public virtual async System.Threading.Tasks.Task UpdateTaskAsync(ScheduleTask task)
        {
            await taskRepository.UpdateAsync(task, task.Id);
        }

        #endregion
    }
}