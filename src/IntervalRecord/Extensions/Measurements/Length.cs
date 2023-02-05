using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Infinity<int> Length(this Interval<int> value, int closureStep) => new IntInterval(value, closureStep).Length();
        public static Infinity<double> Length(this Interval<double> value, double closureStep) => new DoubleInterval(value, closureStep).Length();
        public static Infinity<int> Length(this Interval<DateOnly> value, int closureStep) => new DateOnlyInterval(value, closureStep).Length();
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value, TimeSpan closureStep) => new TimeOnlyInterval(value, closureStep).Length();
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value, TimeSpan closureStep) => new DateTimeInterval(value, closureStep).Length();
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value, TimeSpan closureStep) => new DateTimeOffsetInterval(value, closureStep).Length();

        public static Infinity<int> Length(this Interval<int> value) => new IntInterval(value).Length();
        public static Infinity<double> Length(this Interval<double> value) => new DoubleInterval(value).Length();
        public static Infinity<int> Length(this Interval<DateOnly> value) => new DateOnlyInterval(value).Length();
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value) => new TimeOnlyInterval(value).Length();
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value) => new DateTimeInterval(value).Length();
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value) => new DateTimeOffsetInterval(value).Length();
    }
}
