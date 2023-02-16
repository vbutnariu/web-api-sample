using Demo.Core.Data;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Demo.Core.BaseModels;

namespace Demo.Core.Events
{
    /// <summary>
    /// Event publisher extensions
    /// </summary>
    public static class EventPublisherExtensions
    {
        /// <summary>
        /// Entity inserted
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="entity">Entity</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public static async Task EntityInsertedAsync<T>(this IEventPublisher eventPublisher, T entity, bool throwOnErrors = false) where T : BaseEntity
        {
            await eventPublisher.PublishAsync(new EntityInsertedEvent<T>(entity), throwOnErrors);
        }



        /// <summary>
        /// Entity updated
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="entity">Entity</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public static async Task EntityUpdatedAsync<T>(this IEventPublisher eventPublisher, T entity) where T : BaseEntity
        {
            await eventPublisher.PublishAsync(new EntityUpdatedEvent<T>(entity));
        }

        /// <summary>
        /// Entity deleted
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="entity">Entity</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public static async Task EntityDeletedAsync<T>(this IEventPublisher eventPublisher, T entity) where T : BaseEntity
        {
            await eventPublisher.PublishAsync(new EntityDeletedEvent<T>(entity));
        }


        /// <summary>
        /// Entity inserted
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="entity">Entity</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public static async Task DataChangedAsync<T>(this IEventPublisher eventPublisher, T data) where T : BaseModel
        {
            await eventPublisher.PublishAsync(new ModelChangedEvent<T>(data));
        }

       
    }
}