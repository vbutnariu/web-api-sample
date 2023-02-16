using Demo.Common.Attributes;

namespace Demo.Common.Model.Autorization
{
	
	public class TokenResultModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string full_name { get; set; }
       
                     
    }
}
