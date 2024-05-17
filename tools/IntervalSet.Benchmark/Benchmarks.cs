using BenchmarkDotNet.Attributes;
using IntervalSet.Types;
using System.Text.RegularExpressions;

namespace IntervalSet.Benchmark;

[MemoryDiagnoser]
public partial class Benchmarks
{
    private const string interval = "[1,2]";

    [GeneratedRegex(@"^(\(|\[)\s*([^()[\],\s]+)\s*,\s*([^()[\],\s]+)\s*(\)|\])$")]
    private static partial Regex IntervalRegexWithCaptureGroups();

    [GeneratedRegex(@"^(\(|\[)\s*([^()[\],\s]+)\s*,\s*([^()[\],\s]+)\s*(\)|\])$", RegexOptions.ExplicitCapture)]
    private static partial Regex IntervalRegexWithoutCaptureGroups();

    public Interval<int> ParseWithCaptureGroups(string s, IFormatProvider? provider = default)
    {
        var match = IntervalRegexWithCaptureGroups().Match(s);
        if (!match.Success)
        {
            throw new FormatException();
        }
        var startBound = match.Groups[1].Value[0];
        var startValue = match.Groups[2].Value;
        var endValue = match.Groups[3].Value;
        var endBound = match.Groups[4].Value[0];

        var start = int.Parse(startValue, provider);
        var end = int.Parse(endValue, provider);
        var intervalType = ParseBounds(startBound, endBound);
        return new Interval<int>(start, end, intervalType);
    }

    public Interval<int> ParseWithoutCaptureGroups(string s, IFormatProvider? provider = default)
    {
        if (!IntervalRegexWithoutCaptureGroups().IsMatch(s))
        {
            throw new FormatException();
        }

        int commaIndex = s.IndexOf(',');
        var startValue = s[1..commaIndex];
        var endValue = s[(commaIndex + 1)..^1];

        var start = int.Parse(startValue, provider);
        var end = int.Parse(endValue, provider);
        var intervalType = ParseBounds(s[0], s[^1]);
        return new Interval<int>(start, end, intervalType);
    }


    public Interval<int> ParseWithoutRegex(string s, IFormatProvider? provider = default)
    {
        if (s.Length < 5)
        {
            throw new FormatException();
        }

        var intervalType = ParseBounds(s[0], s[^1]);

        int commaIndex = s.IndexOf(',');
        if (commaIndex < 2 || commaIndex == s.Length - 2)
        {
            throw new FormatException();
        }

        // Extract values
        string startValue = s[1..commaIndex].Trim();
        string endValue = s[(commaIndex + 1)..^1].Trim();

        // Parse values
        int start = int.Parse(startValue, provider);
        int end = int.Parse(endValue, provider);

        return new Interval<int>(start, end, intervalType);
    }

    private static IntervalType ParseBounds(char leftBound, char rightBound)
    {
        return (leftBound, rightBound) switch
        {
            ('(', ')') => IntervalType.Open,
            ('(', ']') => IntervalType.OpenClosed,
            ('[', ')') => IntervalType.ClosedOpen,
            ('[', ']') => IntervalType.Closed,
            _ => throw new FormatException()
        };
    }

    [Benchmark]
    public Interval<int> ParseWithoutCaptureGroups() => ParseWithoutCaptureGroups(interval);

    [Benchmark]
    public Interval<int> ParseWithCaptureGroups() => ParseWithCaptureGroups(interval);

    [Benchmark]
    public Interval<int> ParseWithoutRegex() => ParseWithoutRegex(interval);
}
