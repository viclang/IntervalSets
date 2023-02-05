using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RangeExtensions.Tests.Extensions
{
    internal static class TheoryDataExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this IEnumerable<object[]> value)
        {
            return value.SelectMany(x => x.Cast<T>());
        }
    }
}
