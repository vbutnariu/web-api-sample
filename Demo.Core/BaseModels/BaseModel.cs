using Demo.Common.Attributes;
using System;

namespace Demo.Core.BaseModels
{
    public abstract class BaseModel 
    {
        [DoNotRender]
        public virtual Guid Id { get; set; }
    }
}
