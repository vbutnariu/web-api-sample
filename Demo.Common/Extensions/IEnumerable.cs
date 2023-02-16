using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Demo.Common.Helpers.Extensions
{
    public static class ExtensionMethods
    {
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> collection)
        {
            return new BindingList<T>(collection.ToList());
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }

        public static Boolean CollectionEqual<T,TProp>(this IEnumerable<T> collection1, IEnumerable<T> collection2, Func<T, TProp> compareByFunction)
        {
            if (collection1.Count() != collection2.Count())
                return false;

            var collectionIds1 = collection1.Select(compareByFunction);
            var collectionIds2 = collection2.Select(compareByFunction);

            if (!collectionIds1.All(x => collectionIds2.Contains(x)))
                return false;

            if (!collectionIds2.All(x => collectionIds1.Contains(x)))
                return false;

            return true;
        }
    }
}
