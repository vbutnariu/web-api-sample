using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Enums
{
	public enum Catalog
	{
		Undefined = 0,
		Abda = 1,
		Global = 3,
		Host = 4,
		Wawi = 5,
		Kunden = 6,
		Patienten = 7,
		Ergonomed = 8
	}

	public static class CatalogEnumExtensions
	{
		public static string GetCatalogName(this Catalog value, DateTime? asOfDate = null)
		{
			var targeDate = asOfDate ?? DateTime.Today;
			switch (value)
			{
				case Catalog.Abda:
					return targeDate.Day > 14 ? "ABDATA_15" : "ABDATA_01";
				case Catalog.Global:
					return "GLOB";
				case Catalog.Host:
					return "HOST";
				case Catalog.Wawi:
					return "WAWI";
				case Catalog.Kunden:
					return "KUNDE";
				case Catalog.Patienten:
					return "PATIENT";
				case Catalog.Ergonomed:
					return "ERGONOMED";
				default:
					throw new Exception("catalog not found!");
			}
		}
	}
}
