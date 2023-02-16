using Demo.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Events
{
    public class ModelChangedEvent<T> where T : BaseModel
    {
        public ModelChangedEvent(T data)
        {
            this.Model = data;
        }

        /// <summary>
        /// Entity
        /// </summary>
        public T Model { get; }
    }
}
