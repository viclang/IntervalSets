using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<int> Canonicalize(this Interval<int> source, IntervalType boundaryType, int step)
            => Canonicalize(source, boundaryType, b => b.Add(step), b => b.Substract(step));

        [Pure]
        public static Interval<int> Closure(this Interval<int> source, int step)
            => ToClosed(source, start => start.Add(step), end => end.Substract(step));

        [Pure]
        public static Interval<int> Interior(this Interval<int> source, int step)
            => ToOpen(source, end => end.Add(step), start => start.Substract(step));
    }
}
