using Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Notifications
{
	public class TransportRecordModel
	{
		public Guid RecordId { get; set; }
		public EntityTypeEnum RecordType { get; set; }
		public Guid OwnerId { get; set; }
		public OwnerModel Owner { get; set; }
	}
}
