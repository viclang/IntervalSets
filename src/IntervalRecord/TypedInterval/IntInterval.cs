using InfinityComparable;

namespace IntervalRecord
{
    public class IntInterval : AbstractInterval<int>,
        IIntervalConverter<int, int>,
        IIntervalMeasurements<int, double, double>
    {
        private readonly Interval<int> value;

        public IntInterval(Interval<int> value, int step)
        {
            this.value = ToClosed(value, x => x + step, x => x - step);
        }

        public IntInterval(Interval<int> value)
        {
            this.value = value;
        }
        public Interval<int> Canonicalize(BoundaryType boundaryType, int step)
            => Canonicalize(value, boundaryType, x => x + step, x => x - step);

        public Interval<int> Closure(int step)
            => ToClosed(value, x => x + step, x => x - step);

        public Interval<int> Interior(int step)
            => ToOpen(value, x => x + step, x => x - step);

        public Infinity<int> Length()
            => Length(value, (end, start) => end - start);

        public double? Radius()
            => CalculateOrNull(value, (end, start) => (end - start) / 2);

        public double? Centre()
            => CalculateOrNull(value, (end, start) => (end + start) / 2);
    }
}
