using System.Collections.Generic;
using System.Linq;

namespace Assets
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> list, T item)
        {
            return list.Concat(new[] { item });
        }
    }
}