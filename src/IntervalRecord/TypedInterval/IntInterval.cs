using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static bool IsEmpty(this Interval<int> value, int closureStep)
            => Closure(value, closureStep).IsEmpty();

        public static Infinity<int> Length(this Interval<int> value, int closureStep)
            => Closure(value, closureStep).Length();

        public static double? Radius(this Interval<int> value, int closureStep)
            => Closure(value, closureStep).Radius();

        public static double? Centre(this Interval<int> value, int closureStep)
            => Closure(value, closureStep).Centre();

        public static double? Radius(this Interval<int> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end - start) / 2);

        public static Infinity<int> Length(this Interval<int> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        public static double? Centre(this Interval<int> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end + (double)start) / 2);

        public static Interval<int> Canonicalize(this Interval<int> value, BoundaryType boundaryType, int step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x + step, x => x - step);

        public static Interval<int> Closure(this Interval<int> value, int step)
            => IntervalHelper.ToClosed(value, x => x + step, x => x - step);

        public static Interval<int> Interior(this Interval<int> value, int step)
            => IntervalHelper.ToOpen(value, x => x + step, x => x - step);
    }
}
