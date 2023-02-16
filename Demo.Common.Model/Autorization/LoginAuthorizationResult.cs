using Demo.Common.Attributes;
using Demo.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Model.Autorization
{
	[GenerateWrapper]
	public class LoginAuthorizationResult : BaseModel
	{
		public bool Authorised { get; set; }
		public int UserId { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

	}
}
