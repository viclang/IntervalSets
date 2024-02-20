using IntervalRecords.Experiment.Bounds;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;

namespace IntervalRecords.Experiment;
public record class Interval<T>
    : IComparable<Interval<T>>,
      IComparisonOperators<Interval<T>, Interval<T>, bool>,
      IParsable<Interval<T>>,
      ISpanParsable<Interval<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    internal LowerBound<T> _start;
    internal UpperBound<T> _end;

    public T? Start
    {
        get => _start.Bound;
        set
        {
            _start = _start with { Bound = value };
        }
    }

    public bool StartInclusive
    {
        get => _start.Inclusive;
        set
        {
            _start = _start with { Inclusive = value };
        }
    }

    public T? End
    {
        get => _end.Bound;
        set
        {
            _end = _end with { Bound = value };
        }
    }

    public bool EndInclusive
    {
        get => _end.Inclusive;
        set
        {
            _end = _end with { Inclusive = value };
        }
    }

    public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
    {
        _start = new LowerBound<T>(start, startInclusive);
        _end = new UpperBound<T>(end, endInclusive);
    }

    public static readonly Interval<T> Empty = new(default(T), default(T), false, false);

    public static readonly Interval<T> Unbounded = new(null, null, false, false);

    public virtual bool IsValid => Start is null || End is null || Start.Value.CompareTo(End.Value) < 0;

    public virtual bool IsEmpty => !IsValid || (Start is not null && Start.Equals(End) && !StartInclusive && !EndInclusive);

    public bool IsSingleton => Start is not null && Start.Equals(End) && StartInclusive && EndInclusive;

    public static Interval<T> Singleton(T value) => new Interval<T>(value, value, true, true);

    public static Interval<T> Closed(T? start, T? end) => new Interval<T>(start, end, true, true);

    public static Interval<T> OpenClosed(T? start, T? end) => new Interval<T>(start, end, false, true);

    public static Interval<T> ClosedOpen(T? start, T? end) => new Interval<T>(start, end, true, false);

    public static Interval<T> Open(T? start, T? end) => new Interval<T>(start, end, false, false);

    /// <summary>
    /// Returns a boolean value indicating if the current interval contains the specified value.
    /// </summary>
    /// <param name="value">The value to check if it is contained by the current interval</param>
    /// <returns></returns>
    public bool Contains(T value)
    {
        return (Start is null || Start.Value.CompareTo(value) < 0) && (End is null || value.CompareTo(End.Value) < 0)
            || Start is not null && Start.Value.Equals(value) && StartInclusive
            || End is not null && value.Equals(End.Value) && EndInclusive;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval overlaps with the other interval.
    /// </summary>
    /// <param name="other">The interval to check for overlapping with the current interval.</param>
    /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
    public bool Overlaps(Interval<T> other)
    {
        return _start.CompareTo(other._end) <= 0
            && other._start.CompareTo(_end) <= 0;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval is connected to the other interval.
    /// </summary>
    /// <param name="other">The interval to check if it is connected to current interval.</param>
    /// <returns>True if the current interval and the other interval are connected, False otherwise.</returns>
    public bool IsConnected(Interval<T> other)
    {
        return _start.ConnectedCompareTo(other._end) <= 0
            && other._start.ConnectedCompareTo(_end) <= 0;
    }

    public static int Compare(Interval<T> left, Interval<T> right, IntervalComparison comparisonType) => comparisonType switch
    {
        IntervalComparison.Interval => left.CompareTo(right),
        IntervalComparison.Start => left._start.CompareTo(right._start),
        IntervalComparison.End => left._end.CompareTo(right._end),
        IntervalComparison.StartToEnd => left._start.CompareTo(right._end),
        IntervalComparison.EndToStart => left._end.CompareTo(right._start),
        _ => throw new NotImplementedException(),
    };

    public int CompareTo(Interval<T>? other)
    {
        if (other == null || this > other)
        {
            return 1;
        }
        if (this < other)
        {
            return -1;
        }
        return 0;
    }

    public static bool operator >(Interval<T> left, Interval<T> right)
    {
        int compareEnd = left._end.CompareTo(right._end);
        return compareEnd == 1 || (compareEnd == 0 && left._start.CompareTo(right._start) == 1);
    }

    public static bool operator <(Interval<T> left, Interval<T> right)
    {
        int compareEnd = left._end.CompareTo(right._end);
        return compareEnd == -1 || (compareEnd == 0 && left._start.CompareTo(right._start) == -1);
    }

    public static bool operator >=(Interval<T> left, Interval<T> right) => left == right || left > right;

    public static bool operator <=(Interval<T> left, Interval<T> right) => left == right || left < right;


    public static Interval<T> Parse(string s, IFormatProvider? provider = null)
    {
        return Parse(s.AsSpan(), provider);
    }

    public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        if (!ValidateAndExtractEndpoints(s, out var startValue, out var endValue))
        {
            throw new FormatException(IntervalConstants.IntervalNotFoundMessage);
        }
        return new Interval<T>(
            ParseEndpoint(startValue, provider),
            ParseEndpoint(endValue, provider),
            s[0] == '[',
            s[^1] == ']');
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return TryParse(s.AsSpan(), provider, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        if (!ValidateAndExtractEndpoints(s, out var startValue, out var endValue))
        {
            result = default;
            return false;
        }
        if (TryParseEndpoint(startValue, provider, out var start) && TryParseEndpoint(endValue, provider, out var end))
        {
            result = new Interval<T>(
                    start,
                    end,
                    s[0] == '[',
                    s[^1] == ']');
            return true;
        }
        result = default;
        return false;
    }

    public static List<Interval<T>> ParseAll(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        var enumerator = IntervalConstants.IntervalRegex.EnumerateMatches(s);
        var result = new List<Interval<T>>();
        while (enumerator.MoveNext())
        {
            var match = enumerator.Current;
            var matchedValue = s.Slice(match.Index, match.Length);

            var commaIndex = matchedValue.IndexOf(',');
            var startString = commaIndex > 1 ? matchedValue[1..commaIndex] : ReadOnlySpan<char>.Empty;
            var endString = commaIndex < matchedValue.Length - 2 ? matchedValue[(commaIndex + 1)..^1] : ReadOnlySpan<char>.Empty;
            if (TryParseEndpoint(startString, provider, out var start)
            && TryParseEndpoint(endString, provider, out var end))
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

    private static T? ParseEndpoint(ReadOnlySpan<char> value, IFormatProvider? provider)
    {
        if (value.IsEmpty || value.Contains("infinity", StringComparison.OrdinalIgnoreCase) || value.Contains('∞'))
        {
            return null;
        }
        return T.Parse(value, provider);
    }

    private static bool TryParseEndpoint(ReadOnlySpan<char> value, IFormatProvider? provider, out T? result)
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

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End, StartInclusive, EndInclusive);
    }

    public override string? ToString()
    {
        return new StringBuilder()
            .Append(StartInclusive ? '[' : '(')
            .Append(Start.HasValue ? Start : "-Infinity")
            .Append(", ")
            .Append(End.HasValue ? End : "Infinity")
            .Append(EndInclusive ? ']' : ')')
            .ToString();
    }
}
