using Demo.Resources.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> destination,
                                       IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                destination.Add(item);
            }
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static string ToLimitedString(this IEnumerable<string> collection, int maximum = 3)
        {
            if (collection.Count() > maximum)
            {
                return string.Join(", ", collection.Take(maximum).ToArray()) + LocalizationService.Localize("Common.AndFurther");
            }
            else
            {
                return string.Join(", ", collection.ToArray());
            }
        }
    }
}
