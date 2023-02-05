using InfinityComparable;

namespace IntervalRecord
{
    internal class TimeOnlyInterval : AbstractInterval<TimeOnly>
        , IIntervalConverter<TimeOnly, TimeSpan>
        , IIntervalMeasurements<TimeSpan, TimeSpan, TimeOnly>
    {
        private readonly Interval<TimeOnly> value;
        public TimeOnlyInterval(Interval<TimeOnly> value, TimeSpan step)
        {
            this.value = ToClosed(value, x => x.Add(step), x => x.Add(-step));
        }

        public TimeOnlyInterval(Interval<TimeOnly> value)
        {
            this.value = value;
        }

        public Interval<TimeOnly> Canonicalize(BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        public Interval<TimeOnly> Closure(TimeSpan step)
            => ToClosed(value, x => x.Add(step), x => x.Add(-step));

        public Interval<TimeOnly> Interior(TimeSpan step)
            => ToOpen(value, x => x.Add(step), x => x.Add(-step));

        public Infinity<TimeSpan> Length() => Length(value, (end, start) => end - start);

        public TimeSpan? Radius()
            => CalculateOrNull(value, (end, start) => (end.ToTimeSpan() - start.ToTimeSpan())/2);

        public TimeOnly? Centre()
            => CalculateOrNull(value, (end, start) => start.Add((end - start)/2));
    }
}
