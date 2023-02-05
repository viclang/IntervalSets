namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> Empty<T>() where T : struct, IComparable<T>, IComparable
            => new Interval<T>(default(T), default(T), false, false);

        public static Interval<T> All<T>() where T : struct, IComparable<T>, IComparable
            => new Interval<T>();

        public static Interval<T> Singleton<T>(T value) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(value, value, true, true);

        public static Interval<T> Open<T>(T start, T end) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(start, end, false, false);

        public static Interval<T> Closed<T>(T start, T end) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(start, end, true, true);

        public static Interval<T> OpenClosed<T>(T start, T end) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(start, end, false, true);

        public static Interval<T> ClosedOpen<T>(T start, T end) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(start, end, true, false);

        public static Interval<T> GreaterThan<T>(T start) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(start, null, false, false);

        public static Interval<T> AtLeast<T>(T start) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(start, null, true, false);

        public static Interval<T> LessThan<T>(T end) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(null, end, false, false);

        public static Interval<T> AtMost<T>(T end) where T : struct, IComparable<T>, IComparable
            => new Interval<T>(null, end, false, true);
    }
}
