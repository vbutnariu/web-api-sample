using Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Data
{
	public interface IDbContextFactory
	{
		IDbContext GetDefaultDbContext();
		public Catalog DefaultCatalog { get; set; }
		IDbContext GetDbContext(Catalog catalog);
		public void Initialize();
	}
}
