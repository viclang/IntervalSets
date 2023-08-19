using IntervalRecords.Extensions.PeriodBuilder.Dutch;
using Unbounded;

namespace IntervalRecords.Extensions;
public static partial class IntervalFactory
{
    public static Interval<T> Create<T>(Unbounded<T> start, Unbounded<T> end, bool startInclusive, bool endInclusive)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return (startInclusive, endInclusive) switch
        {
            (true, true) => new ClosedInterval<T>(start, end),
            (false, true) => new OpenClosedInterval<T>(start, end),
            (true, false) => new ClosedOpenInterval<T>(start, end),
            (false, false) => new OpenInterval<T>(start, end)
        };
    }

    public static Interval<T> Create<T>(Unbounded<T> start, Unbounded<T> end, IntervalType intervalType)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return intervalType switch
        {
            IntervalType.Closed => new ClosedInterval<T>(start, end),
            IntervalType.ClosedOpen => new ClosedOpenInterval<T>(start, end),
            IntervalType.OpenClosed => new OpenClosedInterval<T>(start, end),
            IntervalType.Open => new OpenInterval<T>(start, end),
            _ => throw new NotImplementedException(),
        };
    }
}
