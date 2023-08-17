using Unbounded;

namespace IntervalRecords.Extensions.PeriodBuilder.Dutch;

public class Periode<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static PeriodeBuilder Van(T start) => PeriodeBuilder.Van(start);

    public static ClosedInterval<T> Vanaf(T start)
        => new ClosedInterval<T>(start, Unbounded<T>.PositiveInfinity);

    public static OpenInterval<T> Tot(T eind)
        => new OpenInterval<T>(Unbounded<T>.NegativeInfinity, eind);

    public static ClosedInterval<T> TotEnMet(T eind)
        => new ClosedInterval<T>(Unbounded<T>.NegativeInfinity, eind);

    public static OpenInterval<T> Tussen(T start, T eind)
        => new OpenInterval<T>(start, eind);

    public class PeriodeBuilder
    {
        private readonly T _start;

        private PeriodeBuilder(T start)
        {
            _start = start;
        }

        public static PeriodeBuilder Van(T start) => new PeriodeBuilder(start);

        public ClosedOpenInterval<T> Tot(T eind) => new ClosedOpenInterval<T>(_start, eind);

        public ClosedInterval<T> TotEnMet(T eind) => new ClosedInterval<T>(_start, eind);
    }
}

