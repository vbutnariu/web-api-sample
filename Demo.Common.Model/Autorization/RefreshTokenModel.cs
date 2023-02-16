using Demo.Common.Attributes;
using Demo.Core.BaseModels;
using System;

namespace Demo.Common.Model.Autorization
{
    [GenerateWrapper]
    public class RefreshTokenModel : BaseModel
    {
        public virtual Guid RestClientId { get; set; }
        public virtual DateTime IssuedUtc { get; set; }
        public virtual DateTime ExpiresUtc { get; set; }
        public virtual string ProtectedTicket { get; set; }
        public virtual string RemoteIPAddress { get; set; }
    }
}
