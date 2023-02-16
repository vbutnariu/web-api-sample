

using Demo.Common.Cache;
using Demo.Core.Caching;
using Demo.Core.Data;
using Demo.Core.Data.DbContext;
using Demo.Core.DepeandencyManagement;
using Demo.Core.Events;
using Demo.Core.Infrastructure;

using Demo.Resources.Localization;
using Demo.Services.Core.Device;
using Demo.Services.Events;
using Demo.Services.Tasks;
using Demo.WebApi.Ergodat.Authorization.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Demo.WebApi.Ergodat.Infrastructure
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
			//builder.AddTransient<IAuthorizationService, AuthorizationService>();
			//builder.AddTransient<ITokenProvider, JwtAuthorizationTokenProvider>();
			builder.AddTransient(typeof(ICache<>), typeof(Cache<>));
			builder.AddTransient<IScheduleTaskService, ScheduleTaskService>();
			builder.AddTransient<IDeviceService, DeviceService>();
			//builder.AddTransient<IAppointmentService, AppointmentService>();



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