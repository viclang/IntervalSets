using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace IntervalSet.Types;
public static partial class IntervalParse
{
    private const string NotFoundMessage = "Interval not found in string. Please provide an interval string in correct format.";

    [GeneratedRegex(@"^(\(|\[)\s*([^(),[\]]*),([^(),[\]]*)\s*(\)|\])$", RegexOptions.ExplicitCapture)]
    private static partial Regex IntervalRegex();

    internal static Interval<T> Parse<T>(string s, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (!IntervalRegex().IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        T start;
        T end;
        Bound startBound;
        Bound endBound;
        if (IsUnbounded(startValue))
        {
            startBound = Bound.Unbounded;
            start = default!;
        }
        else
        {
            startBound = s[0] == '[' ? Bound.Closed : Bound.Open;
            start = T.Parse(startValue, provider);
        }

        if (IsUnbounded(endValue))
        {
            endBound = Bound.Unbounded;
            end = default!;
        }
        else
        {
            endBound = s[^1] == ']' ? Bound.Closed : Bound.Open;
            end = T.Parse(endValue, provider);
        }
        return new Interval<T>(start, end, startBound, endBound);
    }

    internal static Interval<T> Parse<T>(ReadOnlySpan<char> s, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (!IntervalRegex().IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        T start;
        T end;
        Bound startBound;
        Bound endBound;
        if (IsUnbounded(startValue))
        {
            startBound = Bound.Unbounded;
            start = default!;
        }
        else
        {
            startBound = s[0] == '[' ? Bound.Closed : Bound.Open;
            start = T.Parse(startValue, provider);
        }

        if (IsUnbounded(endValue))
        {
            endBound = Bound.Unbounded;
            end = default!;
        }
        else
        {
            endBound = s[^1] == ']' ? Bound.Closed : Bound.Open;
            end = T.Parse(endValue, provider);
        }
        return new Interval<T>(start, end, startBound, endBound);
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

        T start;
        T end;
        Bound startBound;
        Bound endBound;
        if (IsUnbounded(startValue))
        {
            startBound = Bound.Unbounded;
            start = default!;
        }
        else
        {
            startBound = s[0] == '[' ? Bound.Closed : Bound.Open;
            if (!T.TryParse(startValue, provider, out start!))
            {
                return false;
            }
        }

        if (IsUnbounded(endValue))
        {
            endBound = Bound.Unbounded;
            end = default!;
        }
        else
        {
            endBound = s[^1] == ']' ? Bound.Closed : Bound.Open;
            if (!T.TryParse(endValue, provider, out end!))
            {
                return false;
            }
        }
        result = new(start, end, startBound, endBound);
        return true;
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

        T start;
        T end;
        Bound startBound;
        Bound endBound;
        if (IsUnbounded(startValue))
        {
            startBound = Bound.Unbounded;
            start = default!;
        }
        else
        {
            startBound = s[0] == '[' ? Bound.Closed : Bound.Open;
            if (!T.TryParse(startValue, provider, out start!))
            {
                return false;
            }
        }

        if (IsUnbounded(endValue))
        {
            endBound = Bound.Unbounded;
            end = default!;
        }
        else
        {
            endBound = s[^1] == ']' ? Bound.Closed : Bound.Open;
            if (!T.TryParse(endValue, provider, out end!))
            {
                return false;
            }
        }
        result = new Interval<T>(start, end, startBound, endBound);
        return true;
    }

    internal static (T start, T end) Parse<T, L, R>(ReadOnlySpan<char> s, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        if (!IntervalRegex().IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }

        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        if (L.Bound.IsUnbounded() && !IsUnbounded(startValue)
            || R.Bound.IsUnbounded() && !IsUnbounded(endValue))
        {
            throw new FormatException(NotFoundMessage);
        }

        return (T.Parse(startValue, provider), T.Parse(endValue, provider));
    }

    internal static (T start, T end) Parse<T, L, R>(string s, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {

        if (!IntervalRegex().IsMatch(s))
        {
            throw new FormatException(NotFoundMessage);
        }

        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        if (!ValidateBounds<L, R>(s) || !ValidateValues<L, R>(startValue, endValue))
        {
            throw new FormatException(NotFoundMessage);
        }
        return (T.Parse(startValue, provider), T.Parse(endValue, provider));
    }

    internal static bool TryParse<T, L, R>(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out (T start, T end) result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        result = (default!, default!);
        if (!IntervalRegex().IsMatch(s))
        {
            return false;
        }

        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        if (!ValidateBounds<L, R>(s) || !ValidateValues<L, R>(startValue, endValue))
        {
            return false;
        }
        if (T.TryParse(startValue, provider, out var start) && T.TryParse(endValue, provider, out var end))
        {
            result = (start, end);
            return true;
        }
        return false;
    }

    private static bool ValidateValues<L, R>(ReadOnlySpan<char> startValue, ReadOnlySpan<char> endValue)
        where L : struct, IBound
        where R : struct, IBound
    {
        return (L.Bound, R.Bound) switch
        {
            (Bound.Unbounded, Bound.Unbounded) => IsUnbounded(startValue) && IsUnbounded(endValue),
            (Bound.Unbounded, _) => IsUnbounded(startValue),
            (_, Bound.Unbounded) => IsUnbounded(endValue),
            _ => !IsUnbounded(startValue) && !IsUnbounded(endValue)
        };
    }

    internal static bool TryParse<T, L, R>(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out (T start, T end) result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        result = (default!, default!);
        if (s is null || !IntervalRegex().IsMatch(s))
        {
            return false;
        }
        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        if (!ValidateBounds<L, R>(s) || !ValidateValues<L, R>(startValue, endValue))
        {
            return false;
        }
        if (T.TryParse(startValue, provider, out var start) && T.TryParse(endValue, provider, out var end))
        {
            result = (start, end);
            return true;
        }
        return false;
    }

    private static bool ValidateBounds<L, R>(string s)
        where L : struct, IBound
        where R : struct, IBound
        => (L.Bound, R.Bound) switch
        {
            (Bound.Open or Bound.Unbounded, Bound.Open or Bound.Unbounded) => s[0] == '(' && s[^1] == ')',
            (Bound.Open or Bound.Unbounded, Bound.Closed) => s[0] == '(' && s[^1] == ']',
            (Bound.Closed, Bound.Open or Bound.Unbounded) => s[0] == '[' && s[^1] == ')',
            (Bound.Closed, Bound.Closed) => s[0] == '[' && s[^1] == ']',
            _ => false
        };

    private static bool ValidateBounds<L, R>(ReadOnlySpan<char> s)
        where L : struct, IBound
        where R : struct, IBound
        => (L.Bound, R.Bound) switch
        {
            (Bound.Open or Bound.Unbounded, Bound.Open or Bound.Unbounded) => s[0] == '(' && s[^1] == ')',
            (Bound.Open or Bound.Unbounded, Bound.Closed) => s[0] == '(' && s[^1] == ']',
            (Bound.Closed, Bound.Open or Bound.Unbounded) => s[0] == '[' && s[^1] == ')',
            (Bound.Closed, Bound.Closed) => s[0] == '[' && s[^1] == ']',
            _ => false
        };



    private static bool IsUnbounded(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            || value.Contains('∞')
            || value.Contains("infinity", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsUnbounded(ReadOnlySpan<char> value)
    {
        return value.IsWhiteSpace()
            || value.Contains('∞')
            || value.Contains("infinity", StringComparison.OrdinalIgnoreCase);
    }
}
