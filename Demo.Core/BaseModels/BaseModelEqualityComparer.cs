using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Core.BaseModels
{
    public class BaseModelEqualityComparer<T> : IEqualityComparer<T>
        where T : BaseModel
    {
        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
