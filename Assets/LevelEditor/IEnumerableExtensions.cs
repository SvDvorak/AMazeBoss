using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
    public static IEnumerable<T> SkipAndLoop<T>(this IEnumerable<T> enumerable, int skipCount)
    {
        var list = enumerable as IList<T> ?? enumerable.ToList();
        return list.Skip(skipCount).Concat(list.Take(skipCount));
    }
}