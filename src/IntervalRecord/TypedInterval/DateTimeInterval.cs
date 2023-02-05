using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<DateTime> Canonicalize(this Interval<DateTime> value, BoundaryType boundaryType, TimeSpan step) => new DateTimeInterval(value).Canonicalize(boundaryType, step);
        public static Interval<DateTime> Closure(this Interval<DateTime> value, TimeSpan step) => new DateTimeInterval(value).Closure(step);
        public static Interval<DateTime> Interior(this Interval<DateTime> value, TimeSpan step) => new DateTimeInterval(value).Interior(step);
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value, TimeSpan closureStep) => new DateTimeInterval(value, closureStep).Length();
        public static TimeSpan? Radius(this Interval<DateTime> value, TimeSpan closureStep) => new DateTimeInterval(value, closureStep).Radius();
        public static DateTime? Centre(this Interval<DateTime> value, TimeSpan closureStep) => new DateTimeInterval(value, closureStep).Centre();
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value) => new DateTimeInterval(value).Length();
        public static TimeSpan? Radius(this Interval<DateTime> value) => new DateTimeInterval(value).Radius();
        public static DateTime? Centre(this Interval<DateTime> value) => new DateTimeInterval(value).Centre();
    }

    public class DateTimeInterval : AbstractInterval<DateTime>,
        IIntervalConverter<DateTime, TimeSpan>,
        IIntervalMeasurements<TimeSpan, TimeSpan, DateTime>
    {
        private readonly Interval<DateTime> value;
        public DateTimeInterval(Interval<DateTime> value, TimeSpan step)
        {
            this.value = ToClosed(value, x => x.Add(step), x => x.Add(-step));
        }

        public DateTimeInterval(Interval<DateTime> value)
        {
            this.value = value;
        }

        public Interval<DateTime> Canonicalize(BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        public Interval<DateTime> Closure(TimeSpan step)
            => ToClosed(value, x => x.Add(step), x => x.Add(-step));

        public Interval<DateTime> Interior(TimeSpan step)
            => ToOpen(value, x => x.Add(step), x => x.Add(-step));

        public Infinity<TimeSpan> Length() => Length(value, (end, start) => end - start);

        public TimeSpan? Radius()
            => CalculateOrNull(value, (end, start) => (end - start) / 2);

        public DateTime? Centre()
            => CalculateOrNull(value, (end, start) => start.Add((end - start) / 2));
    }
}
