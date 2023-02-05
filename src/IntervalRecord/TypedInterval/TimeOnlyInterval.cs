using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> value, BoundaryType boundaryType, TimeSpan step)
            => new TimeOnlyInterval(value).Canonicalize(boundaryType, step);
        public static Interval<TimeOnly> Closure(this Interval<TimeOnly> value, TimeSpan step)
            => new TimeOnlyInterval(value).Closure(step);
        public static Interval<TimeOnly> Interior(this Interval<TimeOnly> value, TimeSpan step)
            => new TimeOnlyInterval(value).Interior(step);
        
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value, TimeSpan closureStep)
            => new TimeOnlyInterval(value, closureStep).Length();
        public static TimeSpan? Radius(this Interval<TimeOnly> value, TimeSpan closureStep)
            => new TimeOnlyInterval(value, closureStep).Radius();
        public static TimeOnly? Centre(this Interval<TimeOnly> value, TimeSpan closureStep)
            => new TimeOnlyInterval(value, closureStep).Centre();
        
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value) => new TimeOnlyInterval(value).Length();
        public static TimeSpan? Radius(this Interval<TimeOnly> value) => new TimeOnlyInterval(value).Radius();        
        public static TimeOnly? Centre(this Interval<TimeOnly> value) => new TimeOnlyInterval(value).Centre();
    }

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

        public Infinity<TimeSpan> Length() => CalculateOrInfinity(value, (end, start) => end - start);

        public TimeSpan? Radius()
            => CalculateOrNull(value, (end, start) => (end.ToTimeSpan() - start.ToTimeSpan())/2);

        public TimeOnly? Centre()
            => CalculateOrNull(value, (end, start) => start.Add((end - start)/2));
    }
}
