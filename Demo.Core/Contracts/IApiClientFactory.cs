using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Demo.Core.Contracts
{
    public interface IApiClientFactory
    {
        HttpClient CreateClient();
    }
}
