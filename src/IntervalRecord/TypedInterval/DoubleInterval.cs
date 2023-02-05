using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static bool IsEmpty(this Interval<double> value, int closureStep)
            => Closure(value, closureStep).IsEmpty();

        public static Infinity<double> Length(this Interval<double> value, double closureStep)
            => Closure(value, closureStep).Length();

        public static double? Radius(this Interval<double> value, double closureStep)
            => Closure(value, closureStep).Radius();

        public static double? Centre(this Interval<double> value, double closureStep)
            => Closure(value, closureStep).Centre();

        public static Infinity<double> Length(this Interval<double> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        public static double? Radius(this Interval<double> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end - start) / 2);

        public static double? Centre(this Interval<double> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end + start) / 2);

        public static Interval<double> Canonicalize(this Interval<double> value, BoundaryType boundaryType, double step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x + step, x => x - step);

        public static Interval<double> Closure(this Interval<double> value, double step)
            => IntervalHelper.ToClosed(value, x => x + step, x => x - step);

        public static Interval<double> Interior(this Interval<double> value, double step)
            => IntervalHelper.ToOpen(value, x => x + step, x => x - step);
    }
}
