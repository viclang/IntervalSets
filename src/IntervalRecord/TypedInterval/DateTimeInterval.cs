using InfinityComparable;

namespace IntervalRecord
{
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
