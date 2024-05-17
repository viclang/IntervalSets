using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace IntervalSet.Types.Bounded;
public static partial class BoundedIntervalParse
{
    private const string NotFoundMessage = "Interval not found in string. Please provide an interval string in correct format.";

    [GeneratedRegex(@"^(\(|\[)\s*([^(),[\]\s]+)\s*,\s*([^(),[\]\s]+)\s*(\)|\])$", RegexOptions.ExplicitCapture)]
    private static partial Regex IntervalRegex();

    internal static void ValidateAndThrow(string s)
    {
        if (!IntervalRegex().IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }
    }

    internal static void ValidateAndThrow(ReadOnlySpan<char> s)
    {
        if (!IntervalRegex().IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }
    }

    internal static void ValidateAndThrow(string s, Bound startBound, Bound endBound)
    {
        if (!IntervalRegex().IsMatch(s)
            || (startBound.IsClosed() && s[0] == '[')
            || (endBound.IsClosed() && s[^1] == ']'))
        {
            throw new FormatException(NotFoundMessage);
        }
    }

    internal static void ValidateAndThrow(ReadOnlySpan<char> s, Bound startBound, Bound endBound)
    {
        if (!IntervalRegex().IsMatch(s)
            || (startBound.IsClosed() && s[0] == '(')
            || (endBound.IsClosed() && s[^1] == ')'))
        {
            throw new FormatException(NotFoundMessage);
        }
    }
    internal static bool IsMatch(string s, Bound startBound, Bound endBound)
    {
        return IntervalRegex().IsMatch(s)
            || (startBound.IsClosed() && s[0] == '[')
            || (endBound.IsClosed() && s[^1] == ']');
    }

    internal static bool IsMatch(ReadOnlySpan<char> s, Bound startBound, Bound endBound)
    {
        return IntervalRegex().IsMatch(s)
            || (startBound.IsClosed() && s[0] == '[')
            || (endBound.IsClosed() && s[^1] == ']');
    }

    internal static Interval<T> Parse<T>(string s, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        ValidateAndThrow(s);
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];
        var intervalType = (s[0], s[^1]).ParseBounds();

        return new Interval<T>(
            T.Parse(startValue, provider),
            T.Parse(endValue, provider),
            intervalType);
    }

    internal static Interval<T> Parse<T>(ReadOnlySpan<char> s, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        ValidateAndThrow(s);
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];
        var intervalType = (s[0], s[^1]).ParseBounds();

        return new Interval<T>(
            T.Parse(startValue, provider),
            T.Parse(endValue, provider),
            intervalType);
    }

    internal static bool TryParse<T>(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T> result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        result = null;
        if (s is null || !IntervalRegex().IsMatch(s))
        {
            return false;
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];
        var intervalType = (s[0], s[^1]).ParseBounds();

        if (T.TryParse(startValue, provider, out var start) && T.TryParse(endValue, provider, out var end))
        {
            result = new Interval<T>(start, end, intervalType);
            return true;
        }
        return false;
    }

    internal static bool TryParse<T>(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        result = null;
        if (!IntervalRegex().IsMatch(s))
        {
            return false;
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];
        var intervalType = (s[0], s[^1]).ParseBounds();

        if (T.TryParse(startValue, provider, out var start) && T.TryParse(endValue, provider, out var end))
        {
            result = new Interval<T>(start, end, intervalType);
            return true;
        }
        return false;
    }

    internal static Interval<T, L, R> Parse<T, L, R>(ReadOnlySpan<char> s, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        ValidateAndThrow(s, L.Bound, R.Bound);
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        return new Interval<T, L, R>(
            T.Parse(startValue, provider),
            T.Parse(endValue, provider));
    }

    internal static Interval<T, L, R> Parse<T, L, R>(string s, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        ValidateAndThrow(s, L.Bound, R.Bound);
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        return new Interval<T, L, R>(
            T.Parse(startValue, provider),
            T.Parse(endValue, provider));
    }

    internal static bool TryParse<T, L, R>(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T, L, R> result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        result = null;
        if (!IsMatch(s, L.Bound, R.Bound))
        {
            return false;
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        if (T.TryParse(startValue, provider, out var start) && T.TryParse(endValue, provider, out var end))
        {
            result = new Interval<T, L, R>(start, end);
            return true;
        }
        return false;
    }

    internal static bool TryParse<T, L, R>(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T, L, R> result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        result = null;
        if (s is null || !IsMatch(s, L.Bound, R.Bound))
        {
            return false;
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        if (T.TryParse(startValue, provider, out var start) && T.TryParse(endValue, provider, out var end))
        {
            result = new Interval<T, L, R>(start, end);
            return true;
        }
        return false;
    }
}
