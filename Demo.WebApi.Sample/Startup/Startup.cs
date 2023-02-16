using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Demo.WebApi.Ergodat.Core.Middleware;
using Demo.WebApi.Ergodat.Middleware;
using Demo.Core.Data;
using Demo.WebApi.Ergodat.Infrastructure.Extensions;
using System.Text;
using Demo.WebApi.Ergodat.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Demo.WebApi.Ergodat
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{



			//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

			//	.AddJwtBearer(options =>
			//	{

			//		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKeyToken"]));
			//		options.TokenValidationParameters = new TokenValidationParameters
			//		{
			//			ValidAudience = "https://localhost:5001",
			//			ValidIssuer = "https://localhost:5001",
			//			ValidateIssuerSigningKey = true,
			//			IssuerSigningKey = key,
			//			ValidateIssuer = false,
			//			ValidateAudience = false
			//		};					

			//		options.Events = new JwtBearerEvents
			//		{
			//			OnMessageReceived = context =>
			//			{
			//				var accessToken = context.Request.Query["access_token"];

			//				// If the request is for our hub...
			//				var path = context.HttpContext.Request.Path;
			//				if (!string.IsNullOrEmpty(accessToken) &&
			//					(path.StartsWithSegments("/hubs")))
			//				{
			//					// Read the token out of the query string
			//					context.Token = accessToken;
			//				}
			//				return Task.CompletedTask;
			//			}
			//		};
			//	});



			services.AddLogging();


			services.AddMvc(options =>
			{
				options.Filters.Add<HttpResponseExceptionFilterAttribute>();
				options.Filters.Add(typeof(ValidateModelStateFilterAttribute));
			});



			services.AddControllers(options =>
			{
				options.Filters.Add<ControllerActionFilter>();

			}).AddFluentValidation();

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});


			services.ConfigureApplicationServices(Configuration);

			services.AddSwaggerGen(c =>
			{

				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo Web API service", Version = "v1" });
				c.UseAllOfForInheritance();
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "Oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,

						},
						new List<string>()
					}
				});

			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env,  ILogger<Startup> log)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}



			app.UseCors(x => x
			   .AllowAnyMethod()
			   .AllowAnyHeader()
			   .SetIsOriginAllowed(x => true)
			   .AllowCredentials());
			
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.ConfigureRequestPipeline();

			//app.UseHttpsRedirection();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "BOSS API V1");
				c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
			});

			var useMigrations = this.Configuration.GetValue<bool>("UseMigrations", false);

			var usePostgres = this.Configuration.GetValue<bool>("UsePostgres", false);

			log.LogInformation(string.Format("Database is -> {0}", usePostgres ? "Postgres" : "SqlServer"));

			if (useMigrations)
			{
				log.LogInformation("Start database migration...");
				app.MigrateDatabase();
				log.LogInformation("Database migration completed.");
			}


			app.InitDatabaseContextFactory();

			var backgroundServicesEnabled = this.Configuration.GetValue<bool>("EnableBackgroundServices", false);
			if (backgroundServicesEnabled)
			{
				app.StartBackgroundServices();

			}
		}
	}
}
