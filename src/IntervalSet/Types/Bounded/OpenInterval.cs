using IntervalSet.Types.Bounded;
using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;
public sealed record OpenInterval<T>(T Start, T End) : TypedBoundedInterval<T, Open, Open>(Start, End), ISpanParsable<OpenInterval<T>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public static implicit operator OpenInterval<T>(Interval<T, Open, Open> closedInterval)
        => new(closedInterval.Start, closedInterval.End);

    public static implicit operator Interval<T, Open, Open>(OpenInterval<T> closedInterval)
        => new(closedInterval.Start, closedInterval.End);

    public static implicit operator Interval<T>(OpenInterval<T> closedInterval)
        => new(closedInterval.Start, closedInterval.End, IntervalType.Open);

    public override bool IsEmpty => End.CompareTo(Start) <= 0;

    public override string ToString() => $"({Start}, {End})";

    public static OpenInterval<T> Parse(string s, IFormatProvider? provider)
    {
        return ComplementIntervalParse.Parse<T, Open, Open>(s, provider);
    }

    public static OpenInterval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return ComplementIntervalParse.Parse<T, Open, Open>(s, provider);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out OpenInterval<T> result)
    {
        result = null;
        if(ComplementIntervalParse.TryParse<T, Open, Open>(s, provider, out var typedResult))
        {
            result = typedResult;
            return true;
        }
        return false;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out OpenInterval<T> result)
    {
        result = null;
        if (ComplementIntervalParse.TryParse<T, Open, Open>(s, provider, out var typedResult))
        {
            result = typedResult;
            return true;
        }
        return false;
    }
}
