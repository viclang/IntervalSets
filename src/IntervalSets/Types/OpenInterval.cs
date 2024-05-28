using System.Diagnostics.CodeAnalysis;

namespace IntervalSets.Types;
public sealed class OpenInterval<T>(T Start, T End) : Interval<T, Open, Open>(Start, End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public override string ToString() => $"({Start}, {End})";

    public static new OpenInterval<T> Parse(string s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, Open, Open>(s, provider);
        return new(start, end);
    }

    public static new OpenInterval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, Open, Open>(s, provider);
        return new(start, end);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out OpenInterval<T> result)
    {
        result = null;
        if (IntervalParse.TryParse<T, Open, Open>(s, provider, out var tupleResult))
        {
            result = new(tupleResult.start, tupleResult.end);
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
        if (IntervalParse.TryParse<T, Open, Open>(s, provider, out var tupleResult))
        {
            result = new(tupleResult.start, tupleResult.end);
            return true;
        }
        return false;
    }
}
