using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Demo.WebApi.Ergodat.Middleware
{
	public class ControllerActionFilter : IAsyncActionFilter
    {

        public ControllerActionFilter()
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            FillHeaderInformation(context);
            Authorize(context);
            await next(); // the actual action

        }

        private void Authorize(ActionExecutingContext context)
        {
            //TODO add User role authorization for API calls
        }

        private void FillHeaderInformation(ActionExecutingContext context)
        {
          
        }

        
    }
}
