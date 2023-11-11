using Unbounded;

namespace IntervalRecords.Extensions.PeriodBuilder.English;

public class Period<T>
    where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
{
    public static PeriodBuilder From(T start) => PeriodBuilder.From(start);

    public static ClosedInterval<T> StartingFrom(T start) => new ClosedInterval<T>(start, Unbounded<T>.PositiveInfinity);

    public static OpenInterval<T> To(T end) => new OpenInterval<T>(Unbounded<T>.NegativeInfinity, end);

    public static ClosedInterval<T> UpToAndIncluding(T end) => new ClosedInterval<T>(Unbounded<T>.NegativeInfinity, end);

    public static OpenInterval<T> Between(T start, T end) => new OpenInterval<T>(start, end);

    public class PeriodBuilder
    {
        private readonly T _start;

        private PeriodBuilder(T start)
        {
            _start = start;
        }

        public static PeriodBuilder From(T start) => new PeriodBuilder(start);

        public ClosedOpenInterval<T> To(T end) => new ClosedOpenInterval<T>(_start, end);

        public ClosedInterval<T> UpToAndIncluding(T end) => new ClosedInterval<T>(_start, end);
    }
}

