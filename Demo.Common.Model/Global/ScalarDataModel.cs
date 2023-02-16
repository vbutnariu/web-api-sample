using Demo.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Model.Global
{
	public abstract class ScalarDataModel<T> : BaseModel 
	{
		public T Value { get; set; }
	}
}
