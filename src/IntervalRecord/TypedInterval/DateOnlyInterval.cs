using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> value, BoundaryType boundaryType, int step)
            => new DateOnlyInterval(value).Canonicalize(boundaryType, step);
        public static Interval<DateOnly> Closure(this Interval<DateOnly> value, int step)
            => new DateOnlyInterval(value).Closure(step);
        public static Interval<DateOnly> Interior(this Interval<DateOnly> value, int step)
            => new DateOnlyInterval(value).Interior(step);
        
        public static Infinity<int> Length(this Interval<DateOnly> value, int closureStep)
            => new DateOnlyInterval(value, closureStep).Length();
        public static double? Radius(this Interval<DateOnly> value, int closureStep)
            => new DateOnlyInterval(value, closureStep).Radius();
        public static DateOnly? Centre(this Interval<DateOnly> value, int closureStep)
            => new DateOnlyInterval(value, closureStep).Centre();
        
        public static Infinity<int> Length(this Interval<DateOnly> value) => new DateOnlyInterval(value).Length();
        public static double? Radius(this Interval<DateOnly> value) => new DateOnlyInterval(value).Radius();
        public static DateOnly? Centre(this Interval<DateOnly> value) => new DateOnlyInterval(value).Centre();
    }

    public class DateOnlyInterval : AbstractInterval<DateOnly>,
        IIntervalConverter<DateOnly, int>,
        IIntervalMeasurements<int, double, DateOnly>
    {
        private readonly Interval<DateOnly> value;

        public DateOnlyInterval(Interval<DateOnly> value, int step)
        {
            this.value = ToClosed(value, x => x.AddDays(step), x => x.AddDays(-step));
        }

        public DateOnlyInterval(Interval<DateOnly> value)
        {
            this.value = value;
        }

        public Interval<DateOnly> Canonicalize(BoundaryType boundaryType, int step)
            => Canonicalize(value, boundaryType, x => x.AddDays(step), x => x.AddDays(-step));
        
        public Interval<DateOnly> Closure(int step)
            => ToClosed(value, x => x.AddDays(step), x => x.AddDays(-step));

        public Interval<DateOnly> Interior(int step)
            => ToOpen(value, x => x.AddDays(step), x => x.AddDays(-step));

        public Infinity<int> Length()
            => ValueOrInfinity(value, (end, start) => end.DayNumber - start.DayNumber);

        public double? Radius()
            => ValueOrNull(value, (end, start) => (end.DayNumber - start.DayNumber) / 2);

        public DateOnly? Centre()
            => ValueOrNull(value, (end, start) => start.AddDays((end.DayNumber - start.DayNumber)/2));
    }
}
