using System;

namespace Demo.Core.Data
{
    public interface ITrackableEntity
    {
        Nullable<System.DateTime> CreatedOn { get; set; }
        Nullable<System.DateTime> ModifiedOn { get; set; }
        Nullable<System.Guid> ModifiedBy { get; set; }
    }
}
