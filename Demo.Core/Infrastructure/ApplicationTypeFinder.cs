using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Demo.Core.Infrastructure
{
    public class ApplicationTypeFinder : AppDomainTypeFinder
    {
        #region Fields
        private bool ensureBinFolderAssembliesLoaded = true;
        private bool binFolderAssembliesLoaded;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets whether assemblies in the bin folder of the web application should be specifically checked for being loaded on application load. This is need in situations where plugins need to be loaded in the AppDomain after the application been reloaded.
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return ensureBinFolderAssembliesLoaded; }
            set { ensureBinFolderAssembliesLoaded = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets a physical disk path of \Bin directory
        /// </summary>
        /// <returns>The physical path. E.g. "c:\application\bin"</returns>
        public virtual string GetBinDirectory()
        {
            //if (HostingEnvironment.IsHosted)
            //{
            //    //hosted
            //    return HttpRuntime.BinDirectory;
            //}

            //not hosted. For example, run either in unit tests
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (this.EnsureBinFolderAssembliesLoaded && !binFolderAssembliesLoaded)
            {
                binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                Trace.WriteLine(string.Format( "binary path : {0}", binPath) );
                LoadMatchingAssemblies(binPath);
            }
            return base.GetAssemblies();
        }

        #endregion
    }
}
