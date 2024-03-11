using IntervalRecords.Experiment.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace IntervalRecords.Experiment;

public static class Interval
{
    internal static readonly Regex Regex = new(@"(?:\[|\()(?:[^[\](),]*,[^,()[\]]*)(?:\)|\])", RegexOptions.Compiled);

    internal const string NotFoundMessage = "Interval not found in string. Please provide an interval string in correct format";

    public static Interval<T> Singleton<T>(T value) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T>(value, value, true, true);

    public static Interval<T> Closed<T>(T start, T end) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T>(start, end, true, true);

    public static Interval<T> OpenClosed<T>(T? start, T end) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T>(start, end, false, true);

    public static Interval<T> ClosedOpen<T>(T start, T? end) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T>(start, end, true, false);

    public static Interval<T> Open<T>(T? start, T? end) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T>(start, end, false, false);

    public static int Compare<T>(
        this Interval<T> left,
        Interval<T> right,
        IntervalComparison comparisonType = IntervalComparison.Interval) where T : struct, IComparable<T>, ISpanParsable<T>
        => comparisonType switch
        {
            IntervalComparison.Interval => left.CompareTo(right),
            IntervalComparison.Start => left.CompareStart(right),
            IntervalComparison.End => left.CompareEnd(right),
            IntervalComparison.StartToEnd => left.CompareStartToEnd(right),
            IntervalComparison.EndToStart => left.CompareEndToStart(right),
            _ => throw new NotSupportedException()
        };
}

public record class Interval<T>
    : IComparisonOperators<Interval<T>, Interval<T>, bool>,
      IComparable<Interval<T>>,
      IParsable<Interval<T>>,
      ISpanParsable<Interval<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    private readonly T? _start;

    private readonly T? _end;

    private readonly bool _startInclusive;

    private readonly bool _endInclusive;

    public T? Start
    {
        get => _start;
        init => _start = value;
    }

    public T? End
    {
        get => _end;
        init => _end = value;
    }

    public bool StartInclusive
    {
        get => _start.HasValue ? _startInclusive : false;
        init => _startInclusive = value && _start.HasValue;
    }

    public bool EndInclusive
    {
        get => _end.HasValue ? _endInclusive : false;
        init => _endInclusive = value && _end.HasValue;
    }

    public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
    {
        if (!IsValid)
        {
            throw new ArgumentException($"left value {start} is greater than right value {end}.");
        }
        if (IsEmpty)
        {
            _start = default(T);
            _end = default(T);
            _startInclusive = false;
            _endInclusive = false;
        }
        _start = start;
        _end = end;
        _startInclusive = startInclusive && _start.HasValue;
        _endInclusive = endInclusive && _end.HasValue;
    }

    public static readonly Interval<T> Empty = new(default(T), default(T), false, false);

    public static readonly Interval<T> Unbounded = new(null, null, false, false);

    public bool IsValid => Start is null || End is null || Start.Value.CompareTo(End.Value) <= 0;

    public bool IsEmpty => !IsValid || Start is not null && Start.Equals(End) && !StartInclusive && !EndInclusive;

    public bool IsSingleton => Start is not null && Start.Equals(End) && StartInclusive && EndInclusive;

    /// <summary>
    /// Returns a boolean value indicating if the current interval overlaps with the other interval.
    /// </summary>
    /// <param name="other">The interval to check for overlapping with the current interval.</param>
    /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
    public bool Overlaps(Interval<T> other)
    {
        return this.CompareStartToEnd(other) <= 0 && other.CompareStartToEnd(this) <= 0;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval contains the specified value.
    /// </summary>
    /// <param name="value">The value to check if it is contained by the current interval</param>
    /// <returns></returns>
    public bool Contains(T value)
    {
        var startComparison = Start.HasValue ? Start.Value.CompareTo(value) : -1;
        var endComparison = End.HasValue ? value.CompareTo(End.Value) : -1;

        return startComparison < 0 && endComparison < 0
            || startComparison == 0 && StartInclusive
            || endComparison == 0 && EndInclusive;
    }

    public bool IsDisjoint(Interval<T> other)
    {
        var startComparison = Start is null || other.End is null ? -1 : Start.Value.CompareTo(other.End.Value);
        var endComparison = other.Start is null || End is null ? -1 : other.Start.Value.CompareTo(End.Value);

        return startComparison > 0 || endComparison > 0
            || startComparison == 0 && !StartInclusive && !other.EndInclusive
            || endComparison == 0 && !EndInclusive && !other.StartInclusive;
    }

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
        int compareEnd = left.CompareEnd(right);
        return compareEnd == 1 || compareEnd == 0 && left.CompareStart(right) == -1;
    }

    public static bool operator <(Interval<T> left, Interval<T> right)
    {
        int compareEnd = left.CompareEnd(right);
        return compareEnd == -1 || compareEnd == 0 && left.CompareStart(right) == 1;
    }

    public static bool operator >=(Interval<T> left, Interval<T> right) => left == right || left > right;

    public static bool operator <=(Interval<T> left, Interval<T> right) => left == right || left < right;

    public void Deconstruct(out T? start, out T? end, out bool startInclusive, out bool endInclusive)
        => (start, end, startInclusive, endInclusive) = (Start, End, StartInclusive, EndInclusive);

    public static Interval<T> Parse(string s, IFormatProvider? provider = null)
    {
        return Parse(s.AsSpan(), provider);
    }

    public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        if (!IntervalParser.ValidateAndExtractEndpoints(s, out var startValue, out var endValue))
        {
            throw new FormatException(Interval.NotFoundMessage);
        }
        return new Interval<T>(
            IntervalParser.ParseEndpoint<T>(startValue, provider),
            IntervalParser.ParseEndpoint<T>(endValue, provider),
            s[0] == '[',
            s[^1] == ']');
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return TryParse(s.AsSpan(), provider, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        if (!IntervalParser.ValidateAndExtractEndpoints(s, out var startValue, out var endValue))
        {
            result = default;
            return false;
        }
        if (IntervalParser.TryParseEndpoint<T>(startValue, provider, out var start) && IntervalParser.TryParseEndpoint<T>(endValue, provider, out var end))
        {
            result = new Interval<T>(start, end, s[0] == '[', s[^1] == ']');
            return true;
        }
        result = default;
        return false;
    }

    public static List<Interval<T>> ParseAll(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        var enumerator = Interval.Regex.EnumerateMatches(s);
        var result = new List<Interval<T>>();
        while (enumerator.MoveNext())
        {
            var match = enumerator.Current;
            var matchedValue = s.Slice(match.Index, match.Length);

            var commaIndex = matchedValue.IndexOf(',');
            var startString = commaIndex > 1 ? matchedValue[1..commaIndex] : ReadOnlySpan<char>.Empty;
            var endString = commaIndex < matchedValue.Length - 2 ? matchedValue[(commaIndex + 1)..^1] : ReadOnlySpan<char>.Empty;
            if (IntervalParser.TryParseEndpoint<T>(startString, provider, out var start)
            && IntervalParser.TryParseEndpoint<T>(endString, provider, out var end))
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

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End, StartInclusive, EndInclusive);
    }

    public override string ToString()
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
