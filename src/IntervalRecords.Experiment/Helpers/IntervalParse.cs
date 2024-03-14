using IntervalRecords.Experiment.Endpoints;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace IntervalRecords.Experiment.Helpers;
internal static class IntervalParse
{

    internal static readonly Regex Regex = new(@"(?:\(|\[)(?:[^()[\],]*,[^,()[\]]*)(?:\)|\])", RegexOptions.Compiled);

    internal const string NotFoundMessage = "Interval not found in string. Please provide an interval string in correct format";

    public static Interval<T, L, R> Parse<T, L, R>(ReadOnlySpan<char> s, IFormatProvider? provider = null)
        where T : struct, IComparable<T>, ISpanParsable<T>
        where L : IBound, new()
        where R : IBound, new()
    {
        if (!ValidateAndExtractEndpoints(s, out var startValue, out var endValue))
        {
            throw new FormatException(NotFoundMessage);
        }
        return new Interval<T, L, R>(
            ParseEndpoint<T>(startValue, provider),
            ParseEndpoint<T>(endValue, provider),
            s[0] == '[',
            s[^1] == ']');
    }

    public static bool TryParse<T, L, R>(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T L, R> result)
        where T : struct, IComparable<T>, ISpanParsable<T>
        where L : IBound, new()
        where R : IBound, new()
    {
        if (!ValidateAndExtractEndpoints(s, out var startValue, out var endValue))
        {
            result = default;
            return false;
        }
        if (TryParseEndpoint<T>(startValue, provider, out var start) && TryParseEndpoint<T>(endValue, provider, out var end))
        {
            result = new Interval<T>(start, end, s[0] == '[', s[^1] == ']');
            return true;
        }
        result = default;
        return false;
    }

    public static List<Interval<T>> ParseAll<T>(ReadOnlySpan<char> s, IFormatProvider? provider = null)
        where T : struct, IComparable<T>, ISpanParsable<T>

    {
        var enumerator = Regex.EnumerateMatches(s);
        var result = new List<Interval<T>>();
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
                result.Add(new Interval<T>(
                    start,
                    end,
                    matchedValue[0] == '[',
                    matchedValue[^1] == ']'));
            }
        }
        return result;
    }


    public static bool ValidateAndExtractEndpoints(ReadOnlySpan<char> s, out ReadOnlySpan<char> startString, out ReadOnlySpan<char> endString)
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
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (value.IsEmpty || value.Contains('∞') || value.Contains("infinity", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        return T.Parse(value, provider);
    }

    private static bool TryParseEndpoint<T>(ReadOnlySpan<char> value, IFormatProvider? provider, out T? result)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (value.IsEmpty || value.Contains('∞') || value.Contains("infinity", StringComparison.OrdinalIgnoreCase))
        {
            result = null;
            return true;
        }
        var success = T.TryParse(value, provider, out var endpoint);
        result = endpoint;
        return success;
    }
}
