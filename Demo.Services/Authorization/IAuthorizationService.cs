
using Demo.Common.Model.Autorization;
using Demo.Common.Model.User;
using System;

namespace Demo.Services.Core.Authorization
{
	public interface IAuthorizationService
	{
		#region OAuth
		void CreateClient(RestClientModel client);
		RestClientModel FindClient(string name);
		RestClientModel FindClientByNameAndSecret(string name, string secret);
		bool SaveRefreshToken(RefreshTokenModel token);
		RefreshTokenModel FindRefreshToken(Guid refreshTokenId);
		RestClientModel GetClientById(Guid clientId);
		void RevokeToken(Guid refreshTokenId);
		LoginAuthorizationResult AuthorizeUserByCredentials(string username, string password);
		UserModel GetUserById(int userId);
		LoginAuthorizationResult ValidateUser(UserModel user);
		#endregion OAuth
	}
}