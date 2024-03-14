using IntervalRecords.Experiment;
using IntervalRecords.Experiment.Endpoints;
using IntervalRecords.Experiment.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;

namespace IntervalRecords.Experiment;

public static class Interval
{
    public static Interval<T, Closed, Closed> Singleton<T>(T value)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        return new Interval<T, Closed, Closed>(value, value);
    }

    public static Interval<T, Closed, Closed> Closed<T>(T start, T end) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T, Closed, Closed>(start, end);

    public static Interval<T, Open, Closed> OpenClosed<T>(T? start, T end) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T, Open, Closed>(start, end);

    public static Interval<T, Closed, Open> ClosedOpen<T>(T start, T? end) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T, Closed, Open>(start, end);

    public static Interval<T, Open, Open> Open<T>(T? start, T? end) where T : struct, IComparable<T>, ISpanParsable<T>
        => new Interval<T, Open, Open>(start, end);

    public static int Compare<T, L, R>(
        this Interval<T, L, R> left,
        IInterval<T> right,
        IntervalComparison comparisonType = IntervalComparison.Interval)
        where T : struct, IComparable<T>, ISpanParsable<T>
        where L : IBound, new()
        where R : IBound, new()
        => comparisonType switch
        {
            IntervalComparison.Interval => left.CompareTo(right),
            IntervalComparison.Start => left.LeftEndpoint.CompareTo(right.LeftEndpoint),
            IntervalComparison.End => left.RightEndpoint.CompareTo(right.RightEndpoint),
            IntervalComparison.StartToEnd => left.LeftEndpoint.CompareTo(right.RightEndpoint),
            IntervalComparison.EndToStart => left.RightEndpoint.CompareTo(right.LeftEndpoint),
            _ => throw new NotSupportedException()
        };
}

public record class Interval<T, L, R> : IInterval<T>,
      IComparisonOperators<Interval<T, L, R>, IInterval<T>, bool>,
      IComparable<IInterval<T>>,
      ISpanParsable<Interval<T, L, R>>
    where T : struct, IComparable<T>, ISpanParsable<T>
    where L : IBound, new()
    where R : IBound, new()
{

    private readonly T _start;

    private readonly T _end;

    public T? Start
    {
        get => L.Bound is Bound.Unbounded ? null : _start;
        init => _start = value.GetValueOrDefault();
    }

    public T? End
    {
        get => R.Bound is Bound.Unbounded ? null : _end;
        init => _end = value.GetValueOrDefault();
    }

    public ILeftEndpoint<T> LeftEndpoint => new LeftEndpoint<T, L>(_start);

    public IRightEndpoint<T> RightEndpoint => new RightEndpoint<T, R>(_end);

    public Interval(T? start, T? end)
    {
        _start = start.GetValueOrDefault();
        _end = end.GetValueOrDefault();
    }

    public static readonly Interval<T, Open, Open> Empty = new(default(T), default(T));

    public static readonly Interval<T, Unbounded, Unbounded> Unbounded = new(null, null);

    public bool IsValid => Start is null || End is null || Start.Value.CompareTo(End.Value) <= 0;

    public bool IsEmpty => !IsValid
        || Start.HasValue && End.HasValue && Start.Value.CompareTo(End.Value) == 0 && LeftEndpoint.IsOpen && RightEndpoint.IsOpen;

    public bool IsSingleton =>
        Start.HasValue && End.HasValue && Start.Value.CompareTo(End.Value) == 0 && LeftEndpoint.IsClosed && RightEndpoint.IsClosed;


    /// <summary>
    /// Returns a boolean value indicating if the current interval overlaps with the other interval.
    /// </summary>
    /// <param name="other">The interval to check for overlapping with the current interval.</param>
    /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
    public bool Overlaps(IInterval<T> other)
    {
        return LeftEndpoint.CompareTo(other.RightEndpoint) <= 0
            && other.LeftEndpoint.CompareTo(this.RightEndpoint) <= 0;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval contains the specified value.
    /// </summary>
    /// <param name="other">The value to check if it is contained by the current interval</param>
    /// <returns></returns>
    public bool Contains(T other)
    {
        return LeftEndpoint.CompareTo(other) <= 0
            && RightEndpoint.CompareTo(other) >= 0;
    }

    public bool IsDisjoint(IInterval<T> other)
    {
        var startComparison = Start is null || other.End is null ? -1 : Start.Value.CompareTo(other.End.Value);
        var endComparison = other.Start is null || End is null ? -1 : other.Start.Value.CompareTo(End.Value);

        return startComparison > 0 || endComparison > 0
            || startComparison == 0 && !LeftEndpoint.IsClosed && !other.RightEndpoint.IsClosed
            || endComparison == 0 && !RightEndpoint.IsClosed && !other.LeftEndpoint.IsClosed;
    }

    public int CompareTo(IInterval<T>? other)
    {
        if (other is null || this > other)
        {
            return 1;
        }
        if (this < other)
        {
            return -1;
        }
        return 0;
    }

    public static bool operator >(Interval<T, L, R> left, IInterval<T> right)
    {
        int rightComparison = left.RightEndpoint.CompareTo(right.RightEndpoint);
        return rightComparison == 1 || rightComparison == 0 && left.LeftEndpoint.CompareTo(right.LeftEndpoint) == -1;
    }

    public static bool operator <(Interval<T, L, R> left, IInterval<T> right)
    {
        int rightComparison = left.RightEndpoint.CompareTo(right.RightEndpoint);
        return rightComparison == -1 || rightComparison == 0 && left.LeftEndpoint.CompareTo(right.LeftEndpoint) == 1;
    }

    public static bool operator >=(Interval<T, L, R> left, IInterval<T> right) => left == right || left > right;

    public static bool operator <=(Interval<T, L, R> left, IInterval<T> right) => left == right || left < right;

    public static bool operator ==(Interval<T, L, R>? left, IInterval<T>? right)
    {
        throw new NotImplementedException();
    }

    public static bool operator !=(Interval<T, L, R>? left, IInterval<T>? right)
    {
        throw new NotImplementedException();
    }

    public void Deconstruct(out T? start, out T? end, out bool startInclusive, out bool endInclusive)
        => (start, end, startInclusive, endInclusive) = (Start, End, L.Bound == Bound.Closed, R.Bound == Bound.Closed);

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End, L.Bound, R.Bound);
    }

    public static Interval<T, L, R> Parse(string s, IFormatProvider? provider = null)
        => Parse(s.AsSpan(), provider);

    public static Interval<T, L, R> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
        => IntervalParse.Parse<T, L, R>(s, provider);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T, L, R> result)
        => TryParse(s.AsSpan(), provider, out result);

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T, L, R> result)
        => IntervalParse.TryParse(s, provider, out result);

    public static List<Interval<T, L, R>> ParseAll(ReadOnlySpan<char> s, IFormatProvider? provider = null)
        => IntervalParse.ParseAll<T>(s, provider);

    public override string ToString()
    {
        return new StringBuilder()
            .Append(L.Bound is Bound.Closed ? '[' : '(')
            .Append(Start.HasValue ? Start : "-Infinity")
            .Append(", ")
            .Append(End.HasValue ? End : "Infinity")
            .Append(R.Bound is Bound.Closed ? ']' : ')')
            .ToString();
    }
}
