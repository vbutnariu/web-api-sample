using Demo.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Demo.Services.Installation
{
    /// <summary>
    /// Service responsible for WPF client installation
    /// </summary>
    public class InstallService : IInstallService
    {
        private readonly IApplicationInfo applicationInfo;
        private string assemblyPath;

        public InstallService(IApplicationInfo applicationInfo)
        {
            this.applicationInfo = applicationInfo;
            this.assemblyPath = Path.Combine(applicationInfo.Path, "App", "Wpf", "bin");
        }

        public string GetClientVersion()
        {
            try
            {

                var assemblyName = Path.Combine(assemblyPath, "NC.WPF.Common.dll");
                var version = AssemblyName.GetAssemblyName(assemblyName).Version.ToString(4);

                return version;
            }
            catch (Exception)
            {
                throw;
            }


        }



        public string PrepareZip(string destinationFolder, bool forceCreate)
        {
            var targetFolder = Path.Combine(destinationFolder, "bin");
            var version = GetClientVersion();
            var zipFilename = string.Format("{0}.{1}", version.Replace(".", "_"), "zip");
            var zipFile = Path.Combine(targetFolder, zipFilename);

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            if (forceCreate && File.Exists(zipFile))
            {
                File.Delete(zipFile);
            }

            if (!File.Exists(zipFile))
            {
                ZipFile.CreateFromDirectory(assemblyPath, zipFile);
            }

            return zipFile;
        }
    }
}
