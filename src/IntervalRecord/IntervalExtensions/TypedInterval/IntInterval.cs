using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class IntervalExtensions
    {
        [Pure]
        public static Infinity<int> Length(this Interval<int> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        [Pure]
        public static double? Radius(this Interval<int> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end - start) / 2);

        [Pure]
        public static double? Centre(this Interval<int> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end + (double)start) / 2);

        [Pure]
        public static Interval<int> Canonicalize(this Interval<int> value, BoundaryType boundaryType, int step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x + step, x => x - step);

        [Pure]
        public static Interval<int> Closure(this Interval<int> value, int step)
            => IntervalHelper.ToClosed(value, x => x + step, x => x - step);

        [Pure]
        public static Interval<int> Interior(this Interval<int> value, int step)
            => IntervalHelper.ToOpen(value, x => x + step, x => x - step);
    }
}
