using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.Notifications
{
    public class RecordRemovedTransportModel
    {
        public TransportRecordModel Record { get; set; }
        public EvictionReason Reason { get; set; }
    }
}
