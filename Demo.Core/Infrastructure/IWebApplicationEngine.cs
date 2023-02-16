using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Demo.Core.Infrastructure
{
    public interface IWebApplicationEngine
    {
        /// <summary>   
        /// Initialize components and plugins in the nop environment.
        /// </summary>
        /// <param name="config">Config</param>

        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
        T Resolve<T>(IServiceScope scope = null) where T : class;
        object Resolve(Type type, IServiceScope scope = null);

        IEnumerable<T> ResolveAll<T>(IServiceScope scope = null);
        object ResolveUnregistered(Type type);
        HttpClient CreateHttpClient();
        public void ConfigureRequestPipeline(IApplicationBuilder application);
        string GetHostUrl();
        bool ApplicationStarted { get; set; }
        bool UsePostgres { get; }
    }
}
