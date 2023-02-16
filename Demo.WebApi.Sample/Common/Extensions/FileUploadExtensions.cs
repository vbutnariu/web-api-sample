using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace Demo.WebApi.AbdaCommon.Extensions
{
    public static class FileUploadExtensions
    {
        public static string GetContentType(this IFormFile value)
        {
            var provider = new FileExtensionContentTypeProvider();
            string? contentType = value.ContentType;

            if (string.IsNullOrEmpty(contentType) && !provider.TryGetContentType(value.FileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
