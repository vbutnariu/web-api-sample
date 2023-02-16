using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pm.WebApi.Ergodat.Authorization.Provider
{
    public class BearerTokenDocumentProvider : IDocumentFilter
    {

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
             //TODO add future implementation for swagger doc generations                   
        }
    }
}
