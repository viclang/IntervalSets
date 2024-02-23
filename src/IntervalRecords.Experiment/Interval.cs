using IntervalRecords.Experiment.Bounds;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;

namespace IntervalRecords.Experiment;
public record class Interval<T>
    : AbstractInterval<LowerBound<T>, UpperBound<T>>,
      IComparisonOperators<Interval<T>, Interval<T>, bool>,
      IParsable<Interval<T>>,
      ISpanParsable<Interval<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    public T? Start
    {
        get => LeftEndpoint.Point;
        init => LeftEndpoint = LeftEndpoint with { Point = value };
    }

    public bool StartInclusive
    {
        get => LeftEndpoint.Inclusive;
        init => LeftEndpoint = LeftEndpoint with { Inclusive = value };
    }

    public T? End
    {
        get => RightEndpoint.Point;
        init => RightEndpoint = RightEndpoint with { Point = value };
    }

    public bool EndInclusive
    {
        get => RightEndpoint.Inclusive;
        init => RightEndpoint = RightEndpoint with { Inclusive = value };
    }

    public Interval(T? start, T? end, bool startInclusive, bool endInclusive) : base(
        left: new LowerBound<T>(start, startInclusive),
        right: new UpperBound<T>(end, endInclusive))
    {
        if (!IsValid)
        {
            throw new ArgumentException($"left value {start} is greater than right value {end}.");
        }
        if(IsEmpty)
        {
            LeftEndpoint = new(default(T), false);
            RightEndpoint = new(default(T), false);
        }
    }

    public static readonly Interval<T> Empty = new(default(T), default(T), false, false);

    public static readonly Interval<T> Unbounded = new(null, null, false, false);

    public virtual bool IsValid => Start is null || End is null || Start.Value.CompareTo(End.Value) <= 0;

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
    /// Returns a boolean value indicating if the current interval is connected to the other interval.
    /// </summary>
    /// <param name="other">The interval to check if it is connected to current interval.</param>
    /// <returns>True if the current interval and the other interval are connected, False otherwise.</returns>
    public bool IsConnected(Interval<T> other)
    {
        return LeftEndpoint.ConnectedCompareTo(other.RightEndpoint) <= 0
            && other.LeftEndpoint.ConnectedCompareTo(RightEndpoint) <= 0;
    }

    public static bool operator >(Interval<T> left, Interval<T> right) => IsGreaterThan(left, right);

    public static bool operator <(Interval<T> left, Interval<T> right) => IsLessThan(left, right);

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
