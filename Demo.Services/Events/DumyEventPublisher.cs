using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Demo.Core.Infrastructure;
using Demo.Core.Events;

namespace Demo.Services.Events
{
    /// <summary>
    /// Represents the event publisher implementation
    /// </summary>
    public partial class DumyEventPublisher : IEventPublisher
    {
       
        #region Methods

        public virtual Task PublishAsync<TEvent>(TEvent @event, bool throwOnErrors = false)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}