using InfinityComparable;

namespace IntervalRecord
{
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
