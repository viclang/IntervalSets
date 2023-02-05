using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static bool IsEmpty(this Interval<DateOnly> value, int closureStep)
            => Closure(value, closureStep).IsEmpty();

        public static Infinity<int> Length(this Interval<DateOnly> value, int closureStep)
            => Closure(value, closureStep).Length();

        public static double? Radius(this Interval<DateOnly> value, int closureStep)
            => Closure(value, closureStep).Radius();

        public static DateOnly? Centre(this Interval<DateOnly> value, int closureStep)
            => Closure(value, closureStep).Centre();

        public static Infinity<int> Length(this Interval<DateOnly> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end.DayNumber - start.DayNumber);

        public static double? Radius(this Interval<DateOnly> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end.DayNumber - start.DayNumber) / 2);

        public static DateOnly? Centre(this Interval<DateOnly> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => start.AddDays((end.DayNumber - start.DayNumber) / 2));

        public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> value, BoundaryType boundaryType, int step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x.AddDays(step), x => x.AddDays(-step));

        public static Interval<DateOnly> Closure(this Interval<DateOnly> value, int step)
            => IntervalHelper.ToClosed(value, x => x.AddDays(step), x => x.AddDays(-step));

        public static Interval<DateOnly> Interior(this Interval<DateOnly> value, int step)
            => IntervalHelper.ToOpen(value, x => x.AddDays(step), x => x.AddDays(-step));
    }
}
