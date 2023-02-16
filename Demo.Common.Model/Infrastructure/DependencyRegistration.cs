using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Demo.Common.Model.Mapper;
using Demo.Core.DepeandencyManagement;
using Demo.Core.Infrastructure;

namespace Demo.Common.Model.Infrastructure
{
	public class DependencyRegistration : IDependencyRegistration
    {
        public int Order => 1;

        public void Register(IServiceCollection builder, ITypeFinder typeFinder, IConfiguration configuration)
        {
            builder.AddScoped<IModelMapperConfiguration, ModelMapperConfiguration>();
        }
    }
}
