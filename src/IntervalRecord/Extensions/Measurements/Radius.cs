namespace IntervalRecord
{
    public static partial class Interval
    {
        public static double? Radius(this Interval<int> value, int closureStep) => new IntInterval(value, closureStep).Radius();
        public static double? Radius(this Interval<double> value, double closureStep) => new DoubleInterval(value, closureStep).Radius();
        public static double? Radius(this Interval<DateOnly> value, int closureStep) => new DateOnlyInterval(value, closureStep).Radius();
        public static TimeSpan? Radius(this Interval<TimeOnly> value, TimeSpan closureStep) => new TimeOnlyInterval(value, closureStep).Radius();
        public static TimeSpan? Radius(this Interval<DateTime> value, TimeSpan closureStep) => new DateTimeInterval(value, closureStep).Radius();
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value, TimeSpan closureStep) => new DateTimeOffsetInterval(value, closureStep).Radius();

        public static double? Radius(this Interval<int> value) => new IntInterval(value).Radius();
        public static double? Radius(this Interval<double> value) => new DoubleInterval(value).Radius();
        public static double? Radius(this Interval<DateOnly> value) => new DateOnlyInterval(value).Radius();
        public static TimeSpan? Radius(this Interval<TimeOnly> value) => new TimeOnlyInterval(value).Radius();
        public static TimeSpan? Radius(this Interval<DateTime> value) => new DateTimeInterval(value).Radius();
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value) => new DateTimeOffsetInterval(value).Radius();
    }
}
