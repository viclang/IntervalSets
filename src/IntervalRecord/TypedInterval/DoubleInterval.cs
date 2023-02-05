using InfinityComparable;

namespace IntervalRecord
{
    public class DoubleInterval : AbstractInterval<double>,
        IIntervalConverter<double, double>,
        IIntervalMeasurements<double, double, double>
    {
        private readonly Interval<double> value;

        public DoubleInterval(Interval<double> value, double step)
        {
            this.value = ToClosed(value, x => x + step, x => x - step);
        }

        public DoubleInterval(Interval<double> value)
        {
            this.value = value;
        }
        public Interval<double> Canonicalize(BoundaryType boundaryType, double step)
            => Canonicalize(value, boundaryType, x => x + step, x => x - step);

        public Interval<double> Closure(double step)
            => ToClosed(value, x => x + step, x => x - step);

        public Interval<double> Interior(double step)
            => ToOpen(value, x => x + step, x => x - step);

        public Infinity<double> Length()
            => Length(value, (end, start) => end - start);

        public double? Radius()
            => CalculateOrNull(value, (end, start) => (end - start) / 2);

        public double? Centre()
            => CalculateOrNull(value, (end, start) => (end + start) / 2);
    }
}
