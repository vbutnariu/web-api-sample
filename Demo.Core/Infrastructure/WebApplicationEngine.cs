using Demo.Core.DepeandencyManagement;
using Demo.Core.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Demo.Common.Exceptions;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Text.RegularExpressions;

namespace Demo.Core.Infrastructure
{
    public class WebApplicationEngine : IWebApplicationEngine
    {

        public WebApplicationEngine()
        {

        }

        #region Utilities
        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider(IServiceScope scope = null)
        {
            if (scope == null)
            {
                var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
                var context = accessor?.HttpContext;
                return context?.RequestServices ?? ServiceProvider;
            }
            return scope.ServiceProvider;
        }

        #endregion




        protected virtual void AddAutoMapper(IServiceCollection services, ITypeFinder typeFinder)
        {
            //find mapper configurations provided by other assemblies
            var mapperConfigurations = typeFinder.FindClassesOfType<IModelMapperConfiguration>();

            var modelMapper = Activator.CreateInstance(mapperConfigurations.First()) as IModelMapperConfiguration;

            //create and sort instances of mapper configurations
            AutoMapperConfiguration.Init(modelMapper.GetConfiguration());
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }




        protected virtual void RegisterDependencies(IServiceCollection services, IConfiguration configuration, ITypeFinder typeFinder)
        {

           this.UsePostgres = configuration.GetValue<bool>("UsePostgres");
            //dependencies
            services.AddSingleton<IWebApplicationEngine>(this);
            services.AddSingleton<ITypeFinder>(typeFinder);
            //register dependencies provided by other assemblies
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistration>();
            var drInstances = new List<IDependencyRegistration>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistration)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(services, typeFinder, configuration);

        }

        public virtual object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            throw new PmApplicationException("Unknown dependency");
                        return service;
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new PmApplicationException("No constructor was found that had all the dependencies satisfied.", innerException);
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var typeFinder = new ApplicationTypeFinder();
            this.Configuration = configuration;
            RegisterDependencies(services, configuration, typeFinder);
            AddAutoMapper(services, typeFinder);
        }

        /// <summary>
        /// Configure HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            ServiceProvider = application.ApplicationServices;
            ServerAddresses = application.ServerFeatures.Get<IServerAddressesFeature>();
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="scope">Scope</param>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public T Resolve<T>(IServiceScope scope = null) where T : class
        {
            return (T)Resolve(typeof(T), scope);
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <param name="scope">Scope</param>
        /// <returns>Resolved service</returns>
        public object Resolve(Type type, IServiceScope scope = null)
        {
            return GetServiceProvider(scope)?.GetService(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">Type of resolved services</typeparam>
        /// <returns>Collection of resolved services</returns>
        public virtual IEnumerable<T> ResolveAll<T>(IServiceScope scope = null)
        {
            return (IEnumerable<T>)GetServiceProvider(scope)?.GetServices(typeof(T));
        }

        /// <summary>
        /// Create Http client based on current host service
        /// </summary>
        /// <returns></returns>
        public HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            return client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetHostUrl()
        {
            // read confguration server address
            var address = (ServerAddresses.Addresses.FirstOrDefault() ?? "http://localhost:5000") + "/";
            // retrieve the protocol and the port
            Regex r = new Regex(@"^(?<protocol>\w+)://[^/]+?(?<port>:\d+)?/", RegexOptions.None, TimeSpan.FromMilliseconds(150));
            Match m = r.Match(address);
            if (m.Success)
            {
                var localhostUrl = m.Result("${protocol}://localhost${port}/");
                return localhostUrl;
            }
            else
            {
                return address;
            }
        }

        public virtual IServiceProvider ServiceProvider { get; protected set; }
        public virtual IServerAddressesFeature ServerAddresses { get; protected set; }
        public virtual IConfiguration Configuration { get; protected set; }
        public bool ApplicationStarted { get; set; }
        public bool UsePostgres { get; private set; }
    }
}
