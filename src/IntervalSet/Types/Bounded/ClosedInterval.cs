using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;
public sealed class ClosedInterval<T>(T Start, T End) : Interval<T, Closed, Closed>(Start, End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public override bool IsEmpty => End.CompareTo(Start) < 0;

    public override string ToString() => $"[{Start}, {End}]";

    public static new ClosedInterval<T> Parse(string s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, Closed, Closed>(s, provider);
        return new(start, end);
    }

    public static new ClosedInterval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, Closed, Closed>(s, provider);
        return new(start, end);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out ClosedInterval<T> result)
    {
        result = null;
        if (IntervalParse.TryParse<T, Closed, Closed>(s, provider, out var tupleResult))
        {
            result = new(tupleResult.start, tupleResult.end);
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
        if (IntervalParse.TryParse<T, Closed, Closed>(s, provider, out var tupleResult))
        {
            result = new(tupleResult.start, tupleResult.end);
            return true;
        }
        return false;
    }
}
