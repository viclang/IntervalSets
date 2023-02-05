using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class IntervalExtensions
    {
        [Pure]
        public static IEnumerable<T> Iterate<T>(this Interval<T> value, Func<T, T> AddStep)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.Start.IsInfinite)
            {
                return Enumerable.Empty<T>();
            }
            var start = value.StartInclusive ? value.Start.Finite.Value : AddStep(value.Start.Finite.Value);
            return value.Iterate(start, AddStep);
        }

        [Pure]
        public static IEnumerable<T> Iterate<T>(this Interval<T> value, T start, Func<T, T> AddStep)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.Contains(start) && !value.IsEmpty() && !value.End.IsInfinite)
            {
                for (var i = start; value.EndInclusive ? i <= value.End : i < value.End; i = AddStep(i))
                {
                    yield return i;
                }
            }
        }
    }
}
