using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pm.Common.Core.Exceptions.WebApi;
using Pm.WebApi.Ergodat.Common.Extensions;
using System.Net;

namespace Pm.WebApi.Ergodat.Core.Middleware
{
    public class HttpResponseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is HttpResponseException exc)
            {
                BuildResponseResult(context, exc);
            }
            else // convert any unexpected exception to HttpResponse exception
            {
                BuildResponseResult(context, exception.ToHttpResponseException(HttpStatusCode.BadRequest));
            }
            base.OnException(context);
        }

        private static void BuildResponseResult(ExceptionContext context, HttpResponseException exception)
        {
            context.Result = new ObjectResult(exception.ErrorInfo)
            {
                StatusCode = (int)exception.Status
            };
            context.ExceptionHandled = true;
        }
    }
}
