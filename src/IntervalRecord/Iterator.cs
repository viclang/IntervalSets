using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static IEnumerable<T> Iterate<T>(this Interval<T> source, Func<T, T> addStep)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.Start.IsInfinite)
            {
                return Enumerable.Empty<T>();
            }
            var start = source.StartInclusive ? source.Start.Finite.Value : addStep(source.Start.Finite.Value);
            return source.Iterate(start, addStep);
        }

        [Pure]
        public static IEnumerable<T> Iterate<T>(this Interval<T> source, T start, Func<T, T> addStep)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.Contains(start) && !source.IsEmpty() && !source.End.IsInfinite)
            {
                for (var i = start; source.EndInclusive ? i <= source.End : i < source.End; i = addStep(i))
                {
                    yield return i;
                }
            }
        }
    }
}
