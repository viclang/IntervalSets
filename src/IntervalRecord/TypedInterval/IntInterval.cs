using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<int> Canonicalize(this Interval<int> value, BoundaryType boundaryType, int step)
            => new IntInterval(value).Canonicalize(boundaryType, step);
        public static Interval<int> Closure(this Interval<int> value, int step)
            => new IntInterval(value).Closure(step);
        public static Interval<int> Interior(this Interval<int> value, int step)
            => new IntInterval(value).Interior(step);
        
        public static Infinity<int> Length(this Interval<int> value, int closureStep)
            => new IntInterval(value, closureStep).Length();
        public static double? Radius(this Interval<int> value, int closureStep)
            => new IntInterval(value, closureStep).Radius();
        public static double? Centre(this Interval<int> value, int closureStep)
            => new IntInterval(value, closureStep).Centre();
        
        public static double? Radius(this Interval<int> value) => new IntInterval(value).Radius();
        public static Infinity<int> Length(this Interval<int> value) => new IntInterval(value).Length();
        public static double? Centre(this Interval<int> value) => new IntInterval(value).Centre();
    }

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
            => ValueOrInfinity(value, (end, start) => end - start);

        public double? Radius()
            => ValueOrNull(value, (end, start) => (end - start) / 2);

        public double? Centre()
            => ValueOrNull(value, (end, start) => (end + (double)start) / 2);
    }
}
