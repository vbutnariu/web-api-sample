using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Demo.Common.Extensions
{
    public static class TypeExtensions
    {
        #region Methods

        public static bool IsNullOrDefault<T>(this T? self)
            where T : struct
        {
            return !self.HasValue || self.Value.Equals(default(T));
        }

        public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider source, bool inherit) where T : Attribute
        {
            var attrs = source.GetCustomAttributes(typeof(T), inherit);

            return (attrs != null) ? (T[])attrs : Enumerable.Empty<T>();
        }

        public static object Cast(this Type Type, object data)
        {
            var DataParam = Expression.Parameter(typeof(object), "data");
            var Body = Expression.Block(Expression.Convert(Expression.Convert(DataParam, data.GetType()), Type));

            var Run = Expression.Lambda(Body, DataParam).Compile();
            var ret = Run.DynamicInvoke(data);
            return ret;
        }

        #endregion Methods
    }
}