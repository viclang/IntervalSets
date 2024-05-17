using System.Text.RegularExpressions;

namespace IntervalSet.Types;
internal static partial class IntervalRegex
{
    [GeneratedRegex(@"^(\(|\[)\s*([^(),[\]\s]+)\s*,\s*([^(),[\]\s]+)\s*(\)|\])$", RegexOptions.ExplicitCapture)]
    internal static partial Regex BoundedIntervalRegex();

    [GeneratedRegex(@"^(\)|\])\s*([^(),[\]\s]+)\s*,\s*([^(),[\]\s]+)\s*(\(|\[)$", RegexOptions.ExplicitCapture)]
    internal static partial Regex ComplementIntervalRegex();

    [GeneratedRegex(@"^(\(|\[)([^(),[\]]*),([^(),[\]]*)(\)|\])$", RegexOptions.ExplicitCapture)]
    internal static partial Regex NullableIntervalRegex();

    internal const string NotFoundMessage = "Interval not found in string. Please provide an interval string in correct format.";

    internal static (string start, string end) MatchBoundedInterval(string s)
    {
        return MatchInterval(s, BoundedIntervalRegex());
    }

    internal static (string start, string end) MatchComplementInterval(string s)
    {
        return MatchInterval(s, ComplementIntervalRegex());
    }

    private static int MatchInterval(string s, Regex regex)
    {
        if (!regex.IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }
        int commaIndex = s.IndexOf(',');
        return commaIndex;
    }

    internal static (ReadOnlySpan<char> start, ReadOnlySpan<char> end) MatchBoundedInterval(ReadOnlySpan<char> s)
    {
        return MatchInterval(s, BoundedIntervalRegex());
    }

    internal static (ReadOnlySpan<char> start, ReadOnlySpan<char> end) MatchComplementInterval(ReadOnlySpan<char> s)
    {
        return MatchInterval(s, ComplementIntervalRegex());
    }

    private static int MatchInterval(ReadOnlySpan<char> s, Regex regex)
    {
        if (!regex.IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        return commaIndex;
    }

    internal static Interval<T> ParseBounded<T>(string s, Func<string, T> parse)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (!BoundedIntervalRegex().IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];
        var intervalType = ParseBounds(s[0], s[^1]);
        return new Interval<T>(parse(startValue), parse(endValue), intervalType);
    }

    internal static ComplementInterval<T> ParseComplement<T>(string s, Func<string, T> parse)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var (start, end) = MatchInterval(s, ComplementIntervalRegex());
        var intervalType = ParseComplementBounds(s[0], s[^1]);
        return new ComplementInterval<T>(parse(start), parse(end), intervalType);
    }

    internal static UnboundedInterval<T> ParseUnbounded<T>(string s)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var (start, end) = MatchInterval(s, NullableIntervalRegex());
        if (!IsUnbounded(start) && !IsUnbounded(end))
        {
            throw new FormatException(NotFoundMessage);
        }
        return new UnboundedInterval<T>();
    }

    internal static LeftBoundedInterval<T> ParseLeftBounded<T>(string s, Func<string, T> parse)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var (start, end) = MatchInterval(s, NullableIntervalRegex());
        if (IsUnbounded(start) || !IsUnbounded(end))
        {
            throw new FormatException(NotFoundMessage);
        }
        return new LeftBoundedInterval<T>(parse(start), s[0] == '[' ? Bound.Closed : Bound.Open);
    }

    public static RightBoundedInterval<T> ParseRightBounded<T>(string s, Func<string, T> parse)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var (start, end) = MatchInterval(s, NullableIntervalRegex());
        if (!IsUnbounded(start) || IsUnbounded(end))
        {
            throw new FormatException(NotFoundMessage);
        }
        return new RightBoundedInterval<T>(parse(end), s[^1] == ']' ? Bound.Closed : Bound.Open);
    }

    internal static bool TryParseBounded<T>(string s, IFormatProvider? provider, out Interval<T> result)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        result = default!;
        if (!BoundedIntervalRegex().IsMatch(s))
        {
            return false;
        }

        var (start, end) = MatchInterval(s, BoundedIntervalRegex());
        var intervalType = ParseBounds(s[0], s[^1]);

        if (T.TryParse(start, provider, out var startParsed) && T.TryParse(end, provider, out var endParsed))
        {
            result = new Interval<T>(startParsed, endParsed, intervalType);
            return true;
        }
        return false;
    }

    internal static bool TryParseComplement<T>(string s, IFormatProvider? provider, out ComplementInterval<T> result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        result = default!;
        if (!ComplementIntervalRegex().IsMatch(s))
        {
            return false;
        }

        var (start, end) = MatchInterval(s, ComplementIntervalRegex());
        var intervalType = ParseComplementBounds(s[0], s[^1]);

        if (T.TryParse(start, provider, out var startParsed) && T.TryParse(end, provider, out var endParsed))
        {
            result = new ComplementInterval<T>(startParsed, endParsed, intervalType);
            return true;
        }
        return false;
    }

    internal static bool TryParseUnbounded<T>(string s, out UnboundedInterval<T> result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        result = default!;
        if (!NullableIntervalRegex().IsMatch(s))
        {
            return false;
        }

        var (start, end) = MatchInterval(s, NullableIntervalRegex());
        if (!IsUnbounded(start) && !IsUnbounded(end))
        {
            return false;
        }

        result = new UnboundedInterval<T>();
        return true;
    }

    internal static bool TryParseLeftBounded<T>(string s, IFormatProvider? provider, out LeftBoundedInterval<T> result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        result = default!;
        if (!NullableIntervalRegex().IsMatch(s))
        {
            return false;
        }

        var (start, end) = MatchInterval(s, NullableIntervalRegex());
        if (IsUnbounded(start) || !IsUnbounded(end))
        {
            return false;
        }

        if (T.TryParse(start, provider, out var startParsed))
        {
            result = new LeftBoundedInterval<T>(startParsed, s[0] == '[' ? Bound.Closed : Bound.Open);
            return true;
        }
        return false;
    }

    internal static bool TryParseRightBounded<T>(string s, IFormatProvider? provider, out RightBoundedInterval<T> result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        result = default!;
        if (!NullableIntervalRegex().IsMatch(s))
        {
            return false;
        }

        var (start, end) = MatchInterval(s, NullableIntervalRegex());
        if (!IsUnbounded(start) || IsUnbounded(end))
        {
            return false;
        }

        if (T.TryParse(end, provider, out var endParsed))
        {
            result = new RightBoundedInterval<T>(endParsed, s[^1] == ']' ? Bound.Closed : Bound.Open);
            return true;
        }
        return false;
    }

    private static bool IsUnbounded(string value)
    {
        return string.IsNullOrEmpty(value)
            || value.Contains('∞')
            || value.Contains("infinity", StringComparison.OrdinalIgnoreCase);
    }

    internal static IntervalType ParseBounds(this (char left, char right) bounds)
    {
        return bounds switch
        {
            ('(', ')') => IntervalType.Open,
            ('(', ']') => IntervalType.OpenClosed,
            ('[', ')') => IntervalType.ClosedOpen,
            ('[', ']') => IntervalType.Closed,
            _ => throw new FormatException(NotFoundMessage)
        };
    }

    internal static IntervalType ParseComplementBounds(this (char left, char right) bounds)
    {
        return bounds switch
        {
            (')', '(') => IntervalType.Open,
            (')', '[') => IntervalType.OpenClosed,
            (']', '(') => IntervalType.ClosedOpen,
            (']', '[') => IntervalType.Closed,
            _ => throw new FormatException(NotFoundMessage)
        };
    }
}
