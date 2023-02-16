using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pm.Common.Cryptography;
using Pm.Common.Model.Autorization;
using Pm.Services.Authorization;
using Pm.WebApi.Ergodat.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Pm.WebApi.Ergodat.Authorization.Provider
{
	public class JwtAuthorizationTokenProvider : ITokenProvider
	{

		private readonly ILogger logger;
		private readonly IConfiguration configuration;
		private readonly IAuthorizationService authorizationService;

		public JwtAuthorizationTokenProvider(

					  ILogger<JwtAuthorizationTokenProvider> logger,
					  IConfiguration configuration,
					  IAuthorizationService service)
		{
			this.logger = logger;
			this.configuration = configuration;
			this.authorizationService = service;
		}

		public IActionResult RetrieveToken(LoginInfoModel model)
		{
			try
			{
				var client = authorizationService.FindClient(model.client_id);

				if (client == null)
				{
					return new BadRequestObjectResult(new { error = "unsupported_grant_type", error_description = string.Format("Client '{0}' is not known in the system.", model.client_id) });
				}

				if (!client.Active)
				{
					return new BadRequestObjectResult(new { error = "unsupported_grant_type", error_description = "Client is inactive." });
				}

				logger.LogInformation(string.Format("Authenticating user: {0} ", model.username));
				var authorizationResult = authorizationService.AuthorizeUserByCredentials(model.username, model.password);

				var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyToken"]));
				var signingCredential = new SigningCredentials(encryptionKey, SecurityAlgorithms.HmacSha256);

				var autorizationResult = authorizationService.AuthorizeUserByCredentials(model.username, model.password);
				if (authorizationResult.Authorised)
				{

					var userClaims = new List<Claim>() {
							   new Claim(ClaimTypes.Name, authorizationResult.Username),
							   new Claim("user_id", authorizationResult.UserId.ToString()),
							   new Claim("first_name", authorizationResult.FirstName),
							   new Claim("user_name", authorizationResult.Username),
							   new Claim("last_name", authorizationResult.LastName),
							   new Claim("client_name", client.Name) };
					var tokenExpirationSeconds = configuration.TokenTimeout();
					var token = new JwtSecurityToken(
						issuer: "https://localhost",
						audience: "https://localhost",

						claims: userClaims,
						expires: DateTime.Now.AddSeconds(tokenExpirationSeconds),
						signingCredentials: signingCredential);
					var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


					return new OkObjectResult(new TokenResultModel()
					{
						access_token = tokenString,
						token_type = "bearer",
						expires_in = tokenExpirationSeconds.ToString(),
						refresh_token = GenerateRefreshToken(client.Name, authorizationResult),
						user_id = $"{authorizationResult.UserId}",
						user_name = authorizationResult.Username,
						full_name = $"{authorizationResult.FirstName} {authorizationResult.LastName}",

					});
				}
				else
				{
					if (!authorizationResult.Authorised)
					{
						return new BadRequestObjectResult(new { error = "invalid_grant", error_description = "User or password mismatch" });
					}
					return new BadRequestObjectResult(new { error = "invalid_grant", error_description = "User or password mismatch" });
				}

			}
			catch (ArgumentNullException ex)
			{
				logger.LogError(ex.Message, ex);
				return new BadRequestObjectResult(new { error = "invalid_grant", error_description = ex.Message });
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
				return new BadRequestObjectResult(new { error = "network_exception", error_description = ex.Message });
			}
		}

		private string GenerateRefreshToken(string clientName, LoginAuthorizationResult authorizationResult)
		{
			var result = $"{Guid.NewGuid()}_{clientName}_{DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss")}_{authorizationResult.UserId}".Encode();
			result = result.Base64Encode();
			return result;
		}

		private int GetUserIdFromRefreshToken(string refreshToken)
		{
			if (string.IsNullOrEmpty(refreshToken))
			{
				throw new ArgumentException($"'{nameof(refreshToken)}' cannot be null or empty.", nameof(refreshToken));
			}

			var decodedString = refreshToken.Base64Decode();
			decodedString = decodedString.Decode();

			return int.Parse(decodedString.Split("_").Last());
		}

		private string GetClientNameFromRefreshToken(string refreshToken)
		{
			if (string.IsNullOrEmpty(refreshToken))
			{
				throw new ArgumentException($"'{nameof(refreshToken)}' cannot be null or empty.", nameof(refreshToken));
			}
			var decodedString = refreshToken.Base64Decode().Decode();
			return decodedString.Split("_")[1];
		}

		public IActionResult RetrieveTokenByRefreshToken(string refreshToken)
		{
			try
			{
				if (string.IsNullOrEmpty(refreshToken))
					throw new ArgumentNullException(nameof(refreshToken));

			

				var userId = GetUserIdFromRefreshToken(refreshToken);
				var clientName = GetClientNameFromRefreshToken(refreshToken);

				var user = authorizationService.GetUserById(userId);
				var authorizationResult = authorizationService.ValidateUser(user);
				var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyToken"]));
				var signingCredential = new SigningCredentials(encryptionKey, SecurityAlgorithms.HmacSha256);

				if (authorizationResult.Authorised)
				{
					var userClaims = new List<Claim>() {
							   new Claim(ClaimTypes.Name, authorizationResult.Username),
							   new Claim("user_id", authorizationResult.UserId.ToString()),
							   new Claim("first_name", authorizationResult.FirstName),
							   new Claim("user_name", authorizationResult.Username),
							   new Claim("last_name", authorizationResult.LastName),
							   new Claim("client_name", clientName) };
					var tokenExpirationSeconds = configuration.TokenTimeout();
					var token = new JwtSecurityToken(
						issuer: "https://localhost",
						audience: "https://localhost",
						claims: userClaims,
						expires: DateTime.Now.AddSeconds(tokenExpirationSeconds),
						signingCredentials: signingCredential);
					var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


					return new OkObjectResult(new TokenResultModel()
					{
						access_token = tokenString,
						token_type = "bearer",
						expires_in = tokenExpirationSeconds.ToString(),
						refresh_token = GenerateRefreshToken(clientName, authorizationResult),
						user_id = $"{authorizationResult.UserId}",
						user_name = authorizationResult.Username,
						full_name = $"{authorizationResult.FirstName} {authorizationResult.LastName}",

					});
				}
				else
				{
					if (!authorizationResult.Authorised)
					{
						return new BadRequestObjectResult(new { error = "invalid_grant", error_description = "Not authorised!" });
					}
					return new BadRequestObjectResult(new { error = "invalid_grant", error_description = "Invalid or token not found!" });
				}

			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
				return new BadRequestObjectResult(new { error = "invalid_grant", error_description = "Invalid or token not found!" });
			}
		}

	}
}
