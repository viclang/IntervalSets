using IntervalSet.Bounds;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace IntervalSet.Types;
public static class IntervalParse
{

    internal static readonly Regex Regex = new(@"(?:\(|\[)(?:[^()[\],]*,[^,()[\]]*)(?:\)|\])", RegexOptions.Compiled);

    internal const string NotFoundMessage = "Interval not found in string. Please provide an interval string in correct format";

    public static IAbstractInterval<T> Parse<T>(ReadOnlySpan<char> s, IFormatProvider? provider = null)
        where T : notnull, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (!ValidateAndExtractEndpoints(s, out var startValue, out var endValue))
        {
            throw new FormatException(NotFoundMessage);
        }
        var start = ParseEndpoint<T>(startValue, provider);
        var end = ParseEndpoint<T>(endValue, provider);

        return CreateAbstractInterval(start, end, s[0] == '[', s[^1] == ']');
    }

    public static bool TryParse<T>(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out IAbstractInterval<T> result)
        where T : notnull, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (!ValidateAndExtractEndpoints(s, out var startValue, out var endValue))
        {
            result = default;
            return false;
        }
        if (TryParseEndpoint<T>(startValue, provider, out var start)
            && TryParseEndpoint<T>(endValue, provider, out var end))
        {
            result = CreateAbstractInterval(start, end, s[0] == '[', s[^1] == ']');
            return true;
        }
        result = default;
        return false;
    }

    public static List<IAbstractInterval<T>> ParseAll<T>(ReadOnlySpan<char> s, IFormatProvider? provider = null)
        where T : notnull, IEquatable<T>, IComparable<T>, ISpanParsable<T>

    {
        var enumerator = Regex.EnumerateMatches(s);
        var result = new List<IAbstractInterval<T>>();
        while (enumerator.MoveNext())
        {
            var match = enumerator.Current;
            var matchedValue = s.Slice(match.Index, match.Length);

            var commaIndex = matchedValue.IndexOf(',');
            var startString = commaIndex > 1 ? matchedValue[1..commaIndex] : ReadOnlySpan<char>.Empty;
            var endString = commaIndex < matchedValue.Length - 2 ? matchedValue[(commaIndex + 1)..^1] : ReadOnlySpan<char>.Empty;
            if (TryParseEndpoint<T>(startString, provider, out var start)
            && TryParseEndpoint<T>(endString, provider, out var end))
            {
                result.Add(CreateAbstractInterval(
                    start,
                    end,
                    matchedValue[0] == '[',
                    matchedValue[^1] == ']'));
            }
        }
        return result;
    }

    private static IAbstractInterval<T> CreateAbstractInterval<T>(T? start, T? end, bool startInclusive, bool endInclusive)
        where T : notnull, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        return (start is null, end is null, startInclusive, endInclusive) switch
        {
            (true, true, _, _) => new UnboundedInterval<T>(),
            (false, true, true, _) => new LeftBoundedInterval<T, Closed>(start!),
            (false, true, false, _) => new LeftBoundedInterval<T, Open>(start!),
            (true, false, _, true) => new RightBoundedInterval<T, Closed>(end!),
            (true, false, _, false) => new RightBoundedInterval<T, Open>(end!),
            (false, false, true, true) => new Interval<T, Closed, Closed>(start!, end!),
            (false, false, true, false) => new Interval<T, Closed, Open>(start!, end!),
            (false, false, false, true) => new Interval<T, Open, Closed>(start!, end!),
            (false, false, false, false) => new Interval<T, Open, Open>(start!, end!),
        };
    }

    private static IBoundedInterval<T> CreateBoundedInterval<T>(T? start, T? end, bool startInclusive, bool endInclusive)
        where T : notnull, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        return (start is null, end is null, startInclusive, endInclusive) switch
        {
            (false, false, true, true) => new Interval<T, Closed, Closed>(start!, end!),
            (false, false, true, false) => new Interval<T, Closed, Open>(start!, end!),
            (false, false, false, true) => new Interval<T, Open, Closed>(start!, end!),
            (false, false, false, false) => new Interval<T, Open, Open>(start!, end!),
            _ => throw new InvalidOperationException()
        };
    }

    private static ILeftBoundedInterval<T> CreateLeftBoundedInterval<T>(T? start, T? end, bool startInclusive, bool endInclusive)
        where T : notnull, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        return (start is null, end is null, startInclusive, endInclusive) switch
        {
            (false, true, true, _) => new LeftBoundedInterval<T, Closed>(start!),
            (false, true, false, _) => new LeftBoundedInterval<T, Open>(start!),
            _ => throw new InvalidOperationException()
        };
    }

    private static IRightBoundedInterval<T> CreateRightBoundedInterval<T>(T? start, T? end, bool startInclusive, bool endInclusive)
        where T : notnull, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        return (start is null, end is null, startInclusive, endInclusive) switch
        {
            (false, true, true, _) => new RightBoundedInterval<T, Closed>(start!),
            (false, true, false, _) => new RightBoundedInterval<T, Open>(start!),
            _ => throw new InvalidOperationException()
        };
    }

    private static bool ValidateAndExtractEndpoints(ReadOnlySpan<char> s, out ReadOnlySpan<char> startString, out ReadOnlySpan<char> endString)
    {
        var commaIndex = s.IndexOf(',');
        if (commaIndex < 1 || !"[(".Contains(s[0]) || !"])".Contains(s[^1]))
        {
            startString = ReadOnlySpan<char>.Empty;
            endString = ReadOnlySpan<char>.Empty;
            return false;
        }
        startString = commaIndex > 1 ? s[1..commaIndex] : ReadOnlySpan<char>.Empty;
        endString = commaIndex < s.Length - 2 ? s[(commaIndex + 1)..^1] : ReadOnlySpan<char>.Empty;
        return true;
    }

    private static T? ParseEndpoint<T>(ReadOnlySpan<char> value, IFormatProvider? provider)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (value.IsEmpty || value.Contains('∞') || value.Contains("infinity", StringComparison.OrdinalIgnoreCase))
        {
            return default;
        }
        return T.Parse(value, provider);
    }

    private static bool TryParseEndpoint<T>(ReadOnlySpan<char> value, IFormatProvider? provider, out T? result)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (value.IsEmpty || value.Contains('∞') || value.Contains("infinity", StringComparison.OrdinalIgnoreCase))
        {
            result = default;
            return true;
        }
        var success = T.TryParse(value, provider, out var endpoint);
        result = endpoint;
        return success;
    }
}
