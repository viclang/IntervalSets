using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.Extensions
{
    internal static class TheoryDataExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this IEnumerable<object[]> other)
        {
            return other.SelectMany(x => x.Cast<T>());
        }
    }
}
