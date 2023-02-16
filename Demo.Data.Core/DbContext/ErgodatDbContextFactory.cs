using Microsoft.Extensions.Configuration;
using Demo.Common.Enums;
using Demo.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Data.DbContext
{
	public class ErgodatDbContextFactory : IDbContextFactory
	{

		private readonly ErgodatContext context;
		private readonly IConfiguration configuration;

		
		public ErgodatDbContextFactory(IConfiguration configuration)
		{
			context = new ErgodatContext(configuration.GetConnectionString("DbContext"));
			this.configuration = configuration;
		}
		public Catalog DefaultCatalog { get; set; } = Catalog.Ergonomed;

		public IDbContext GetDbContext(Catalog catalog)
		{
			return context;
		}

		public IDbContext GetDefaultDbContext()
		{
			return context;
		}

		public void Initialize()
		{
			
		}

		private string GetConnectionString()
		{
			return configuration["ConnectionStrings:Ergodat"];
		}

	}
}
