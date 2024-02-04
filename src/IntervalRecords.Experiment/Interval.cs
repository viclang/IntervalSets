using IntervalRecords.Endpoints;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace IntervalRecords;
public record class Interval<T>
    : IParsable<Interval<T>>, ISpanParsable<Interval<T>>
    where T : struct, IComparable<T>, IParsable<T>, ISpanParsable<T>
{
    private readonly Endpoint<T> _start;

    private readonly Endpoint<T> _end;
    /// <summary>
    /// Represents the start value of the interval.
    /// </summary>
    public Endpoint<T> Start
    {
        get => _start;
        init
        {
            _start = -value;
        }
    }

    /// <summary>
    /// Represents the end value of the interval.
    /// </summary>
    public Endpoint<T> End
    {
        get => _end;
        init
        {
            _end = +value;
        }
    }

    public static readonly Interval<T> Empty = new(
        new Endpoint<T>(default(T), false, BoundaryType.Lower),
        new Endpoint<T>(default(T), false, BoundaryType.Upper));

    public static readonly Interval<T> Unbounded = new(Endpoint<T>.NegativeInfinity, Endpoint<T>.PositiveInfinity);

    public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
    {
        _start = new Endpoint<T>(start, startInclusive, BoundaryType.Lower);
        _end = new Endpoint<T>(end, endInclusive, BoundaryType.Upper);
    }

    public Interval(Endpoint<T> start, Endpoint<T> end)
    {
        _start = -start;
        _end = +end;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval contains the specified value.
    /// </summary>
    /// <param name="value">The value to check if it is contained by the current interval</param>
    /// <returns></returns>
    public bool Contains(T value)
    {
        var singleton = Singleton(value);
        return Start <= singleton.Start && singleton.End <= End;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval overlaps with the other interval.
    /// </summary>
    /// <param name="other">The interval to check for overlapping with the current interval.</param>
    /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
    public bool Overlaps(Interval<T> other)
    {
        return Start <= other.End && other.Start <= End;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval is connected to the other interval.
    /// </summary>
    /// <param name="other">The interval to check if it is connected to current interval.</param>
    /// <returns>True if the current interval and the other interval are connected, False otherwise.</returns>
    public bool IsConnected(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || Start == other.End && (Start.Inclusive || other.End.Inclusive)
            || End == other.Start && (End.Inclusive || other.Start.Inclusive);
    }

    public static Interval<T> Singleton(T value) => new Interval<T>(value, value, true, true);


    private static readonly Regex _intervalRegex = new(@"(?:\[|\()(?:[^[\](),]*,[^,()[\]]*)(?:\)|\])");
    private const string _notFoundMessage = "Interval not found in string. Please provide an interval string in correct format";

    public static Interval<T> Parse(string s, IFormatProvider? provider)
    {
        var match = _intervalRegex.Match(s);
        if (!match.Success)
        {
            throw new ArgumentException(_notFoundMessage);
        }
        var parts = s.Split(',');
        return new Interval<T>(
            ParseStart(parts[0], provider),
            ParseEnd(parts[1], provider));
    }

    public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        return Parse(s.ToString(), provider);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        if (s == null)
        {
            result = default;
            return false;
        }
        var match = _intervalRegex.Match(s);
        if (!match.Success)
        {
            result = default;
            return false;
        }
        var parts = s.Split(',');
        if (TryParseStart(parts[0], provider, out var start)
            && TryParseEnd(parts[1], provider, out var end))
        {
            result = new Interval<T>(start, end);
            return true;
        }
        result = default;
        return false;
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<Interval<T>> ParseAll(string value, IFormatProvider? provider = null)
    {
        var matches = _intervalRegex.Matches(value);
        return matches.Select(match => Parse(match.Value, provider));
    }

    private static Endpoint<T> ParseStart(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if (s.Equals("NegativeInfinity", StringComparison.OrdinalIgnoreCase)
            || s.Equals("-Infinity", StringComparison.OrdinalIgnoreCase)
            || s.Equals("-∞", StringComparison.CurrentCulture))
        {
            return Endpoint<T>.NegativeInfinity;
        }
        return new Endpoint<T>(T.Parse(s[1..], provider), s[0] == '[', BoundaryType.Lower);
    }

    private static bool TryParseStart(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Endpoint<T> result)
    {
        if (s.Equals("NegativeInfinity", StringComparison.OrdinalIgnoreCase)
            || s.Equals("-Infinity", StringComparison.OrdinalIgnoreCase)
            || s.Equals("-∞", StringComparison.CurrentCulture))
        {
            result = Endpoint<T>.NegativeInfinity;
            return true;
        }
        if (T.TryParse(s[1..], provider, out var point))
        {
            result = new Endpoint<T>(point, s[0] == '[', BoundaryType.Lower);
            return true;
        }
        result = default;
        return false;
    }

    private static Endpoint<T> ParseEnd(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if (s.Equals("PositiveInfinity", StringComparison.OrdinalIgnoreCase)
            || s.Equals("+Infinity", StringComparison.OrdinalIgnoreCase)
            || s.Equals("+∞", StringComparison.CurrentCulture)
            || s.Equals("∞", StringComparison.CurrentCulture))
        {
            return Endpoint<T>.PositiveInfinity;
        }
        return new Endpoint<T>(T.Parse(s[..^1], provider), s[^1]  == ']', BoundaryType.Upper);
    }

    private static bool TryParseEnd(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Endpoint<T> result)
    {
        if (s.Equals("PositiveInfinity", StringComparison.OrdinalIgnoreCase)
            || s.Equals("+Infinity", StringComparison.OrdinalIgnoreCase)
            || s.Equals("+∞", StringComparison.CurrentCulture)
            || s.Equals("∞", StringComparison.CurrentCulture))
        {
            result = Endpoint<T>.PositiveInfinity;
            return true;
        }
        if (T.TryParse(s[..^1], provider, out var point))
        {
            result = new Endpoint<T>(point, s[^1] == ']', BoundaryType.Upper);
            return true;
        }
        result = default;
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {
        return $"{Start}, {End}";
    }
}
