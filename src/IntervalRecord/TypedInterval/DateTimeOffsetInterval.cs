using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> value, BoundaryType boundaryType, TimeSpan step) => new DateTimeOffsetInterval(value).Canonicalize(boundaryType, step);
        public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> value, TimeSpan step) => new DateTimeOffsetInterval(value).Closure(step);
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> value, TimeSpan step) => new DateTimeOffsetInterval(value).Interior(step);
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value, TimeSpan closureStep) => new DateTimeOffsetInterval(value, closureStep).Length();
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value) => new DateTimeOffsetInterval(value).Length();
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value, TimeSpan closureStep) => new DateTimeOffsetInterval(value, closureStep).Radius();
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value) => new DateTimeOffsetInterval(value).Radius();
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value, TimeSpan closureStep) => new DateTimeOffsetInterval(value, closureStep).Centre();
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value) => new DateTimeOffsetInterval(value).Centre();
    }

    internal class DateTimeOffsetInterval : AbstractInterval<DateTimeOffset>,
        IIntervalConverter<DateTimeOffset, TimeSpan>,
        IIntervalMeasurements<TimeSpan, TimeSpan, DateTimeOffset>
    {
        private readonly Interval<DateTimeOffset> value;
        public DateTimeOffsetInterval(Interval<DateTimeOffset> value, TimeSpan step)
        {
            this.value = ToClosed(value, x => x.Add(step), x => x.Add(-step));
        }

        public DateTimeOffsetInterval(Interval<DateTimeOffset> value)
        {
            this.value = value;
        }

        public Interval<DateTimeOffset> Canonicalize(BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        public Interval<DateTimeOffset> Closure(TimeSpan step)
            => ToClosed(value, x => x.Add(step), x => x.Add(-step));

        public Interval<DateTimeOffset> Interior(TimeSpan step)
            => ToOpen(value, x => x.Add(step), x => x.Add(-step));

        public Infinity<TimeSpan> Length() => Length(value, (end, start) => end - start);

        public TimeSpan? Radius()
            => CalculateOrNull(value, (end, start) => (end - start) / 2);

        public DateTimeOffset? Centre()
            => CalculateOrNull(value, (end, start) => start.Add((end - start) / 2));
    }
}
