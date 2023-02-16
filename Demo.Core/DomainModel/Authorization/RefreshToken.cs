using Demo.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DomainModel.Authorization
{
    public class RefreshToken : BaseEntity
    {
		public Guid Id { get; set; }
		public Guid RestClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
        public virtual RestClient RestClient { get; set; }
        public string RemoteIPAddress { get; set; }
    }
}
