using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Pm.WebApi.Ergodat.Infrastructure.Extensions
{
    public static class HostingExtensions
    {
        private static bool uploadFolderInit = false;
        private static bool tempFolderInit = false;

        public static string GetUploadFilePath(this IWebHostEnvironment host)
        {
            var location= Path.Combine(host.ContentRootPath, "UploadedFiles");
            EnsureUploadFolder(location);
            return location;
           
        }

        private static void EnsureUploadFolder(string location)
        {
            if (!uploadFolderInit && !Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
                uploadFolderInit = true;
            }
        }

        public static string GetTempFilePath(this IWebHostEnvironment host)
        {
            var location = Path.Combine(host.ContentRootPath, "Temp");
            EnsureTempFolder(location);
            return location;
        }

        private static void EnsureTempFolder(string location)
        {
            if (!tempFolderInit && !Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
                tempFolderInit = true;
            }
        }
    }
}
