using Demo.Core.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.WebApi.Ergodat.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <param name="hostingEnvironment">Hosting environment</param>
        /// <returns>Configured service provider</returns>
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var engine = WebEngineContext.Create(true);
            engine.ConfigureServices(services, configuration);
        }
    }
}
