using Demo.Common.Attributes;
using Demo.Common.Enums;
using Demo.Core.BaseModels;

namespace Demo.Common.Model.Autorization
{


    [GenerateWrapper]
    public class RestClientModel : BaseModel
    {
        public virtual string Secret { get; set; }
        public virtual string Name { get; set; }
        public virtual ApplicationTypes ApplicationType { get; set; }
        public virtual bool Active { get; set; }
        public virtual int RefreshTokenLifeTime { get; set; }
        public virtual string AllowedOrigin { get; set; }
    
    }
}
