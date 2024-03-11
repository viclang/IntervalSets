namespace IntervalRecords.Experiment.Helpers;
internal static class IntervalParser
{
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

    public static T? ParseEndpoint<T>(ReadOnlySpan<char> value, IFormatProvider? provider)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (value.IsEmpty || value.Contains("infinity", StringComparison.OrdinalIgnoreCase) || value.Contains('∞'))
        {
            return null;
        }
        return T.Parse(value, provider);
    }

    public static bool TryParseEndpoint<T>(ReadOnlySpan<char> value, IFormatProvider? provider, out T? result)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (value.IsEmpty || value.Contains("infinity", StringComparison.OrdinalIgnoreCase) || value.Contains('∞'))
        {
            result = null;
            return true;
        }
        var success = T.TryParse(value, provider, out var endpoint);
        result = endpoint;
        return success;
    }
}
