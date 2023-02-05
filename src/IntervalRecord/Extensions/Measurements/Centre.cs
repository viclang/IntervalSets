namespace IntervalRecord
{
    public static partial class Interval
    {
        public static double? Centre(this Interval<int> value, int closureStep) => new IntInterval(value, closureStep).Centre();
        public static double? Centre(this Interval<double> value, double closureStep) => new DoubleInterval(value, closureStep).Centre();
        public static DateOnly? Centre(this Interval<DateOnly> value, int closureStep) => new DateOnlyInterval(value, closureStep).Centre();
        public static TimeOnly? Centre(this Interval<TimeOnly> value, TimeSpan closureStep) => new TimeOnlyInterval(value, closureStep).Centre();
        public static DateTime? Centre(this Interval<DateTime> value, TimeSpan closureStep) => new DateTimeInterval(value, closureStep).Centre();
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value, TimeSpan closureStep) => new DateTimeOffsetInterval(value, closureStep).Centre();

        public static double? Centre(this Interval<int> value) => new IntInterval(value).Centre();
        public static double? Centre(this Interval<double> value) => new DoubleInterval(value).Centre();
        public static DateOnly? Centre(this Interval<DateOnly> value) => new DateOnlyInterval(value).Centre();
        public static TimeOnly? Centre(this Interval<TimeOnly> value) => new TimeOnlyInterval(value).Centre();
        public static DateTime? Centre(this Interval<DateTime> value) => new DateTimeInterval(value).Centre();
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value) => new DateTimeOffsetInterval(value).Centre();
    }
}
