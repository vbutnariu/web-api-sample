

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Demo.Common.Core.Exceptions.WebApi;
using Demo.Common.Exceptions.Validation;
using Demo.WebApi.Ergodat.Common.Extensions;
using System.Linq;
using System.Net;

namespace Demo.WebApi.Ergodat.Middleware
{
	public class ValidateModelStateFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToArray();

                var exception = new ModelValidationException(string.Join(";", errors));

                BuildResponseResult(context, exception.ToHttpResponseException(HttpStatusCode.BadRequest));
            }
        }

        private static void BuildResponseResult(ActionExecutingContext context, HttpResponseException exception)
        {
            context.Result = new ObjectResult(exception.ErrorInfo)
            {
                StatusCode = (int)exception.Status
            };
        }
    }
}
