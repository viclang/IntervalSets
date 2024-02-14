using IntervalRecords.Experiment;
using System.Diagnostics.CodeAnalysis;

namespace IntervalRecords;
public record class Interval<T>
    : IParsable<Interval<T>>, ISpanParsable<Interval<T>>
    where T : struct, IComparable<T>, IParsable<T>, ISpanParsable<T>
{
    /// <summary>
    /// Represents the start value of the interval.
    /// </summary>
    public T? Start { get; init; }

    /// <summary>
    /// Represents the end value of the interval.
    /// </summary>
    public T? End { get; init; }

    public bool StartInclusive { get; init; }

    public bool EndInclusive { get; init; }

    public static readonly Interval<T> Empty = new(default(T), default(T), false, false);

    public static readonly Interval<T> Unbounded = new(default, default, false, false);

    public virtual bool IsValid => Start is null || End is null || Start.Value.CompareTo(End.Value) < 0;
    public virtual bool IsEmpty => !IsValid || (Start is not null && Start.Equals(End));
    public bool IsSingleton => Start is not null && Start.Equals(End) && StartInclusive && EndInclusive;

    public bool IsBounded => State == IntervalState.Bounded;

    public bool IsLeftBounded => State == IntervalState.LeftBounded;

    public bool IsRightBounded => State == IntervalState.RightBounded;

    public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
    {
        Start = start;
        End = end;
        StartInclusive = startInclusive;
        EndInclusive = endInclusive;
    }

    public static Interval<T> Singleton(T value) => new Interval<T>(value, value, true, true);

    /// <summary>
    /// Determines the interval type.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="value">The interval to determine the type of.</param>
    /// <returns>The interval type as an IntervalType enumeration value.</returns>
    public IntervalType GetIntervalType() => (IntervalType)((StartInclusive ? 1 : 0) | (EndInclusive ? 2 : 0));


    /// <summary>
    /// Determines if the interval is half-bounded.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="source">The interval to check.</param>
    /// <returns>True if the interval is half-bounded.</returns>
    public bool IsHalfBounded(Interval<T> value)
        => value.State is IntervalState.LeftBounded or IntervalState.RightBounded;

    /// <summary>
    /// Determines the bounded state of the interval.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="source">The interval to determine the bounded state of.</param>
    /// <returns>A value indicating whether the interval is bounded, left-bounded, right-bounded, or unbounded.</returns>
    /// <exception cref="NotSupportedException">Thrown when the start or end state of the interval is not finite or infinity.</exception>
    public IntervalState State => (Start is null, End is null) switch
    {
        (false, false) => IntervalState.Bounded,
        (true, true) => IntervalState.Unbounded,
        (true, false) => IntervalState.RightBounded,
        (false, true) => IntervalState.LeftBounded,
    };

    /// <summary>
    /// Returns a boolean value indicating if the current interval contains the specified value.
    /// </summary>
    /// <param name="value">The value to check if it is contained by the current interval</param>
    /// <returns></returns>
    public bool Contains(T value)
    {
        return (Start is null || Start.Value.CompareTo(value) < 0)
            && (End is null || value.CompareTo(End.Value) < 0)
            || StartEquals(value, StartInclusive)
            || EndEquals(value, EndInclusive);
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval overlaps with the other interval.
    /// </summary>
    /// <param name="other">The interval to check for overlapping with the current interval.</param>
    /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
    public bool Overlaps(Interval<T> other)
    {
        return CompareStartToEnd(other) <= 0 && other.CompareStartToEnd(this) <= 0;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval is connected to the other interval.
    /// </summary>
    /// <param name="other">The interval to check if it is connected to current interval.</param>
    /// <returns>True if the current interval and the other interval are connected, False otherwise.</returns>
    public bool IsConnected(Interval<T> other)
    {
        return (Start is null || other.End is null || Start.Value.CompareTo(other.End.Value) < 0)
            && (End is null || other.Start is null || other.Start.Value.CompareTo(End.Value) < 0)
                || StartEquals(other.End, StartInclusive || other.EndInclusive)
                || EndEquals(other.Start, EndInclusive || other.StartInclusive);
    }

    public int CompareStart(Interval<T> other) => (Start.HasValue, other.Start.HasValue) switch
    {
        (true, true) => Start!.Value.CompareTo(other.Start!.Value),
        (false, true) => -1,
        (true, false) => 1,
        (false, false) => 0,
    };

    public int CompareEnd(Interval<T> other) => (End.HasValue, other.End.HasValue) switch
    {
        (true, true) => End!.Value.CompareTo(other.End!.Value),
        (false, true) => 1,
        (true, false) => -1,
        (false, false) => 0,
    };

    public int CompareEndToStart(Interval<T> other)
    {
        return other.CompareStartToEnd(this);
    }

    public int CompareStartToEnd(Interval<T> other)
    {
        if (Start is null || other.End is null)
        {
            return -1;
        }
        var startEndComparison = Start.Value.CompareTo(other.End.Value);

        // Start is greater than end if they are equal and at least one is exclusive.
        if (startEndComparison == 0 && (!StartInclusive || !other.EndInclusive))
        {
            return 1;
        }
        return startEndComparison;
    }

    private bool StartEquals(T? otherEnd, bool inclusiveCondition)
    {
        return Start is not null && otherEnd is not null && Start.Equals(otherEnd) && inclusiveCondition;
    }

    private bool EndEquals(T? otherStart, bool inclusiveCondition)
    {
        return End is not null && otherStart is not null && End.Equals(otherStart) && inclusiveCondition;
    }

    public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        return Parse(s.ToString(), provider);
    }

    public static Interval<T> Parse(string s, IFormatProvider? provider)
    {
        var match = IntervalConstants.IntervalRegex.Match(s);
        if (!match.Success)
        {
            throw new ArgumentException(IntervalConstants.IntervalNotFoundMessage);
        }
        var parts = s.Split(',');
        return new Interval<T>(
            ParseEndpoint(parts[0][1..].AsSpan(), provider),
            ParseEndpoint(parts[1][..^1].AsSpan(), provider),
            parts[0][0] == '[',
            parts[1][^1] == ']');
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return TryParse(s.ToString(), provider, out result);
    }


    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        result = default;
        if (s is null)
        {
            return false;
        }
        var match = IntervalConstants.IntervalRegex.Match(s);
        if (!match.Success)
        {
            return false;
        }
        var parts = s.Split(',');
        if (TryParseEndpoint(parts[0][1..].AsSpan(), provider, out var start)
            && TryParseEndpoint(parts[1][..^1].AsSpan(), provider, out var end))
        {
            result = new Interval<T>(start, end, parts[0][0] == '[', parts[1][^1] == ']');
            return true;
        }
        return false;
    }

    public static IEnumerable<Interval<T>> ParseAll(string value, IFormatProvider? provider = null)
    {
        var matches = IntervalConstants.IntervalRegex.Matches(value);
        return matches.Select(match => Parse(match.Value, provider));
    }

    private static T? ParseEndpoint(ReadOnlySpan<char> value, IFormatProvider? provider)
    {
        if (value.Contains("infinity", StringComparison.OrdinalIgnoreCase) || value.Contains('∞'))
        {
            return default(T?);
        }
        return T.Parse(value, provider);
    }

    private static bool TryParseEndpoint(ReadOnlySpan<char> value, IFormatProvider? provider, out T? result)
    {
        if (value.Contains("infinity", StringComparison.OrdinalIgnoreCase) || value.Contains('∞'))
        {
            result = null;
            return false;
        }
        var success = T.TryParse(value, provider, out var parsed);
        result = parsed;
        return success;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End, StartInclusive, EndInclusive);
    }

    public override string? ToString()
    {
        return $"{(StartInclusive ? '[' : '(')}{Start}, {End}{(EndInclusive ? ']' : ')')}";
    }
}
