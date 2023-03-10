using Demo.Core.BaseModels;
using System.ComponentModel;

namespace Demo.Common.Model.Autorization
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginInfoModel 
	{
        /// <summary>
        /// Values: "password" "refresh_token"
        /// </summary>
        [DefaultValue("password")]
        public string grant_type { get; set; }
       
        public string username { get; set; }
  
        public string password { get; set; }
   
        public string client_id { get; set; }
     
        public string client_secret { get; set; }
        public string refresh_token{ get; set; }

    }
}
