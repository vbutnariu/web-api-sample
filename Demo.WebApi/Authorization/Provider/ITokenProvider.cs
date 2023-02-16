using Microsoft.AspNetCore.Mvc;
using Pm.Common.Model.Autorization;

namespace Pm.WebApi.Ergodat.Authorization.Provider
{
	public interface ITokenProvider
    {
        IActionResult RetrieveToken(LoginInfoModel model);
        IActionResult RetrieveTokenByRefreshToken(string refreshToken);
    }
}