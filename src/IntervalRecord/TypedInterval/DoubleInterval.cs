using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<double> Canonicalize(this Interval<double> value, BoundaryType boundaryType, double step)
            => new DoubleInterval(value).Canonicalize(boundaryType, step);
        public static Interval<double> Closure(this Interval<double> value, double step)
            => new DoubleInterval(value).Closure(step);
        public static Interval<double> Interior(this Interval<double> value, double step)
            => new DoubleInterval(value).Interior(step);
        
        public static Infinity<double> Length(this Interval<double> value, double closureStep)
            => new DoubleInterval(value, closureStep).Length();
        public static double? Radius(this Interval<double> value, double closureStep)
            => new DoubleInterval(value, closureStep).Radius();
        public static double? Centre(this Interval<double> value, double closureStep)
            => new DoubleInterval(value, closureStep).Centre();
        
        public static Infinity<double> Length(this Interval<double> value)  => new DoubleInterval(value).Length();
        public static double? Radius(this Interval<double> value) => new DoubleInterval(value).Radius();
        public static double? Centre(this Interval<double> value)  => new DoubleInterval(value).Centre();
    }

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
            => CalculateOrInfinity(value, (end, start) => end - start);

        public double? Radius()
            => CalculateOrNull(value, (end, start) => (end - start) / 2);

        public double? Centre()
            => CalculateOrNull(value, (end, start) => (end + start) / 2);
    }
}
