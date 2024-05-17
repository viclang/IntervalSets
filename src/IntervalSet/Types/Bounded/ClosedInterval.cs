using IntervalSet.Types.Bounded;
using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;
public sealed record ClosedInterval<T>(T Start, T End) : TypedBoundedInterval<T, Closed, Closed>(Start, End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public override bool IsEmpty => End.CompareTo(Start) < 0;

    public static implicit operator ClosedInterval<T>(Interval<T, Closed, Closed> closedInterval)
        => new(closedInterval.Start, closedInterval.End);

    public static implicit operator Interval<T, Closed, Closed>(ClosedInterval<T> closedInterval)
        => new(closedInterval.Start, closedInterval.End);

    public static implicit operator Interval<T>(ClosedInterval<T> closedInterval)
        => new(closedInterval.Start, closedInterval.End, IntervalType.Closed);

    public override string ToString() => $"[{Start}, {End}]";

    public static ClosedInterval<T> Parse(string s, IFormatProvider? provider)
    {
        return ComplementIntervalParse.Parse<T, Closed, Closed>(s, provider);
    }

    public static ClosedInterval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return ComplementIntervalParse.Parse<T, Closed, Closed>(s, provider);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out ClosedInterval<T> result)
    {
        result = null;
        if (ComplementIntervalParse.TryParse<T, Closed, Closed>(s, provider, out var typedResult))
        {
            result = typedResult;
            return true;
        }
        return false;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out ClosedInterval<T> result)
    {
        result = null;
        if (ComplementIntervalParse.TryParse<T, Closed, Closed>(s, provider, out var typedResult))
        {
            result = typedResult;
            return true;
        }
        return false;
    }
}
