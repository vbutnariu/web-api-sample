using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pm.Common.Model.Autorization;
using Pm.WebApi.Ergodat.Authorization.Provider;
using System;
using System.Web;

namespace Pm.WebApi.Ergodat.Controllers
{

	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ITokenProvider provider;

		public ILogger logger { get; }

		public AuthController(ITokenProvider provider, ILogger<AuthController> logger)
		{
			this.provider = provider;
			this.logger = logger;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("auth/token")]

		public IActionResult Token([FromForm] LoginInfoModel model)
		{
			return provider.RetrieveToken(model);
		} 


		[AllowAnonymous]
		[HttpGet("auth/refresh")]

		public IActionResult RefreshToken(string token)
		{
			return provider.RetrieveTokenByRefreshToken(token);
		}
	}
}
