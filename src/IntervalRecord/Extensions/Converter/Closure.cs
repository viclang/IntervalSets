namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> Closure<T>(this Interval<T> value, int step, Func<T, int, T> addStep)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => ToClosed(value, x => addStep(x, step), x => addStep(x, -step));
        public static Interval<T> Closure<T>(this Interval<T> value, double step, Func<T, double, T> addStep)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => ToClosed(value, x => addStep(x, step), x => addStep(x, -step));
        public static Interval<T> Closure<T>(this Interval<T> value, TimeSpan step, Func<T, TimeSpan, T> addStep)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => ToClosed(value, x => addStep(x, step), x => addStep(x, -step));
    }
}
