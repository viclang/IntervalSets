using InfinityComparable;

namespace IntervalRecord
{
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
            => Length(value, (end, start) => end.DayNumber - start.DayNumber);

        public double? Radius()
            => CalculateOrNull(value, (end, start) => (end.DayNumber - start.DayNumber) / 2);

        public DateOnly? Centre()
            => CalculateOrNull(value, (end, start) => start.AddDays((end.DayNumber - start.DayNumber)/2));
    }
}
