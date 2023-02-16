using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pm.Core.Data;
using Pm.Services.Installation;
using Pm.WebApi.Ergodat.Infrastructure.Extensions;
using System.IO;

namespace Pm.WebApi.Ergodat.Controllers
{

	public class ClientController : AppBaseController
    {
        private readonly IInstallService service;
        private readonly IWebHostEnvironment host;
        private readonly IDbContext dbContext;

        public ClientController(IInstallService service, IWebHostEnvironment host, IDbContext dbContext)
        {
            this.service = service;
            this.host = host;
            this.dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/api/[controller]/version")]
        public string GetVersion()
        {
            return TryExecute(() => service.GetClientVersion());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/api/[controller]/environment")]
        public string GetConfigurationEnvironment()
        {
            return host.EnvironmentName;
        }


        [Route("/api/[controller]/download")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult DownloadFileContent(bool forceCreate = false)
        {
            var zipFile = service.PrepareZip(host.GetTempFilePath(), forceCreate);
            var stream = new FileStream(zipFile, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.RandomAccess);
            return File(stream, "application/zip", "AppClient.Wpf.zip");
        }

        [Route("/api/[controller]/migrate")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult MigrateDatabase()
        {
            try
            {
                if (dbContext.Provider == Pm.Common.Enums.DatabaseProviderEnum.Postgres)
                {
                    dbContext.MigrateDatabase();
                    return this.Ok();
                }else
                {
                    return this.ValidationProblem("Operation available only for Postgres database!");
                }
            }
            catch
            {
                throw;
            }
           
        }

        [Route("/api/[controller]/drop")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult DropDatabase()
        {
            try
            {
                if (dbContext.Provider == Pm.Common.Enums.DatabaseProviderEnum.Postgres)
                {
                    dbContext.DropDatabase();
                    return this.Ok();
                }
                else
                {
                    return this.ValidationProblem("Operation available only for Postgres database!");
                }
            }
            catch
            {
                throw;
            }

        }

    }
}
