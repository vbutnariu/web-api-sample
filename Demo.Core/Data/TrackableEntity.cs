using System;

namespace Demo.Core.Data
{
    public class TrackableEntity : ITrackableEntity
    {
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
    }
}
