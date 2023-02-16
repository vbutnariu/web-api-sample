using Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Notifications
{
    public interface IDataNotifyClient
    {
        Task RecordUpdateGranted(Guid userId, TransportRecordModel record);
        Task RecordUpdated(Guid userId, TransportRecordModel record);
        Task RecordUpdateDenied(Guid userId, TransportRecordModel record);
        Task Notify(string message);
    }
}
