

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pm.Common.Cache;
using Pm.Core.Caching;
using Pm.Core.Data;
using Pm.Core.DepeandencyManagement;
using Pm.Core.Events;
using Pm.Core.Infrastructure;
using Pm.Data.Core;
using Pm.Data.Core.DbContext;
using Pm.Resources.Localization;
using Pm.Services.Authorization;
using Pm.Services.Core.Appointments;
using Pm.Services.Events;
using Pm.Services.Tasks;
using Pm.WebApi.Ergodat.Authorization.Provider;
using Pm.WebApi.Ergodat.Common.Extensions;
using System;
using System.Linq;

namespace Pm.WebApi.Ergodat.Infrastructure
{
	public class DependencyRegistration : IDependencyRegistration
	{
		public int Order => 3;


		public void Register(IServiceCollection builder, ITypeFinder typeFinder, IConfiguration configuration)
		{
			//Register hosted services

			//scoped objects

			//builder.AddScoped(c => new SqlAbdaDbContext(configuration.ConnectionString()));



			//singleton services

			builder.AddSingleton<ILocalizationService, LocalizationService>();
			builder.AddSingleton<ILocker, MemoryCacheManager>();
			builder.AddSingleton<SqlParameterFactory>();
			builder.AddSingleton<IApplicationInfo, WebApplicationInfo>();
			builder.AddSingleton<IEventPublisher, EventPublisher>();

			//Add scoped services
			builder.AddScoped<IDbContextFactory, ErgodatDbContextFactory>();

			//Transient services
			builder.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
			builder.AddTransient<IAuthorizationService, AuthorizationService>();
			builder.AddTransient<ITokenProvider, JwtAuthorizationTokenProvider>();
			builder.AddTransient(typeof(ICache<>), typeof(Cache<>));
			builder.AddTransient<IScheduleTaskService, ScheduleTaskService>();
			builder.AddTransient<IAppointmentService, AppointmentService>();



			// get all event consumers
			var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
			foreach (var consumer in consumers)
			{
				foreach (var foundInterfaces in consumer.FindInterfaces((type, criteria) =>
				{
					var isMatch = type.IsGenericType && ((Type?)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
					return isMatch;
				}, typeof(IConsumer<>)))
					builder.AddScoped(foundInterfaces, consumer);
			}
		}
	}
}