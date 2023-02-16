using Demo.Core.Contracts;
using Demo.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.WebApi.Ergodat.Infrastructure
{
    public partial class ApiClientFactory : IApiClientFactory
    {
        public HttpClient CreateClient()
        {
            return WebEngineContext.Current.Resolve<IHttpClientFactory>().CreateClient();
        }
    }
}
