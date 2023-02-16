using Demo.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DomainModel.Authorization
{
    public class RestClient : BaseEntity
    {
        public RestClient()
        {
            this.RefreshTokens = new HashSet<RefreshToken>();
        }

		public Guid Id { get; set; }
		public string Secret { get; set; }
        public string Name { get; set; }
        public int ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
