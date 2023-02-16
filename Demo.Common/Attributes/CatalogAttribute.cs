using Demo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Attributes
{
	public  class CatalogAttribute :Attribute
	{
		private readonly Catalog catalog;

		public CatalogAttribute( Catalog catalog)
		{
			this.catalog = catalog;
		}
		public Catalog Catalog => catalog;
	}
}
