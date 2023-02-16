using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Helpers
{
    public class ObjectHelper
    {
        public static bool NotDefault<T>(T prop)
        {
            return !EqualityComparer<T>.Default.Equals(prop, default);
        }
    }
}
