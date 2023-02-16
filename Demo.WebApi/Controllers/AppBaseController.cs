using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pm.WebApi.Ergodat.Common.Extensions;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pm.WebApi.Ergodat.Controllers
{
	
    [ApiController]
    [Route("Api/[controller]")]
    public class AppBaseController : ControllerBase
    {

        protected T TryExecute<T>(Func<T> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                //logger.WriteError("Error ocurred", ex);
                throw ex.ToHttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        protected async Task<T> TryExecuteAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                //logger.WriteError("Error ocurred", ex);
                throw ex.ToHttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        protected void TryExecute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                //logger.WriteError("Error ocurred", ex);
                throw ex.ToHttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}