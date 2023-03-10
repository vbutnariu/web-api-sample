using Microsoft.AspNetCore.Hosting;
using Demo.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebApi.Ergodat.Infrastructure
{
    public class WebApplicationInfo : IApplicationInfo
    {

        public WebApplicationInfo(IWebHostEnvironment env)
        {
            this.Path = env.ContentRootPath;
            this.Name = env.ApplicationName;
            this.Environment = env.EnvironmentName;
        }
        public string Name { get; }
        public string Environment { get; }
        public string Path { get; }
    }
}
