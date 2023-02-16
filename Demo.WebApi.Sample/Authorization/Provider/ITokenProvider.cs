using Microsoft.AspNetCore.Mvc;
using Demo.Common.Model.Autorization;

namespace Demo.WebApi.Ergodat.Authorization.Provider
{
	public interface ITokenProvider
    {
        IActionResult RetrieveToken(LoginInfoModel model);
        IActionResult RetrieveTokenByRefreshToken(string refreshToken);
    }
}