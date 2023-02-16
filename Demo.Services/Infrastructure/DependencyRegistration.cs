using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Demo.Core.DepeandencyManagement;
using Demo.Core.Infrastructure;

namespace Demo.Web.Api.AbdaInfrastructure
{
	public class DependencyRegistration : IDependencyRegistration
    {
        public int Order => 2;


        public void Register(IServiceCollection builder, ITypeFinder typeFinder, IConfiguration configuration)
        {          
            //Transient builders
           
        }
    }
}
