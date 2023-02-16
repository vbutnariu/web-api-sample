using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Demo.Core.Data;
using Demo.Core.Infrastructure;
using Demo.Core.Data;
using Demo.Core.Data.DbContext;
using Demo.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebApi.Ergodat.Infrastructure.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Configure the application HTTP request pipeline
		/// </summary>
		/// <param name="application">Builder for configuring an application's request pipeline</param>
		public static void ConfigureRequestPipeline(this IApplicationBuilder application)
		{
			WebEngineContext.Current.ConfigureRequestPipeline(application);
		}




		public static void StartBackgroundServices(this IApplicationBuilder application)
		{
			TaskManager.Instance.Initialize();
			TaskManager.Instance.Start();

		}

		public static void InitDatabaseContextFactory(this IApplicationBuilder application)
		{
			using (var serviceScope = application.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
			{
				
				if (serviceScope == null) throw new NullReferenceException(nameof(serviceScope));
				var contextFactory = serviceScope.ServiceProvider.GetRequiredService<IDbContextFactory>();
				contextFactory.Initialize();
			}
		}

		public static void MigrateDatabase(this IApplicationBuilder application)
		{
			using (var serviceScope = application.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
			{
				if (serviceScope == null) throw new NullReferenceException(nameof(serviceScope));
				var context = serviceScope.ServiceProvider.GetRequiredService<IDbContext>();

				if (context is AppDbObjectContextPostgres)
				{
					context.MigrateDatabase();
				}
				else
				{
					throw new ApplicationException("Database can be created with postgres database only");
				}
			}
		}
	}
}
