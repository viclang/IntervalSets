using InfinityComparable;
using IntervalRecord.Internal;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class IntervalExtensions
    {
        [Pure]
        public static Infinity<double> Length(this Interval<double> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        [Pure]
        public static double? Radius(this Interval<double> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end - start) / 2);

        [Pure]
        public static double? Centre(this Interval<double> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end + start) / 2);

        [Pure]
        public static Interval<double> Canonicalize(this Interval<double> value, BoundaryType boundaryType, double step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x + step, x => x - step);

        [Pure]
        public static Interval<double> Closure(this Interval<double> value, double step)
            => IntervalHelper.ToClosed(value, x => x + step, x => x - step);

        [Pure]
        public static Interval<double> Interior(this Interval<double> value, double step)
            => IntervalHelper.ToOpen(value, x => x + step, x => x - step);
    }
}
