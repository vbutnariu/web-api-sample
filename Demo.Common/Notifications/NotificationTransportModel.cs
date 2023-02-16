using Demo.Common.Enums;
using System;

namespace Demo.Common.Notifications
{
    public class NotificationTransportModel
    {
        public Guid NotificationId { get; set; }
        public NotificationCategoryEnum Type { get; set; }
        public NotificationInfoTypeEnum? InfoType { get; set; }
        public string Payload { get; set; }
    }
}
