using IntervalSet.Bounds;
using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;

public sealed record Interval<T> : IBoundedInterval<T>, ISpanParsable<Interval<T>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    private readonly BoundPair _boundPair;

    public T Start { get; init; }

    public T End { get; init; }

    public Bound StartBound
    {
        get => _boundPair.StartBound();
        init => _boundPair = BoundPairFactory.Create(value, EndBound);
    }

    public Bound EndBound
    {
        get => _boundPair.EndBound();
        init => _boundPair = BoundPairFactory.Create(StartBound, value);
    }

    public bool IsEmpty => End.CompareTo(Start) is int comparison
        && comparison < 0 || comparison == 0 && StartBound.IsOpen() && EndBound.IsOpen();

    public Interval(T start, T end, Bound startBound, Bound endBound)
        : this(start, end, BoundPairFactory.Create(startBound, endBound))
    {
    }

    public Interval(T start, T end, BoundPair boundPair)
    {
        Start = start;
        End = end;
        _boundPair = boundPair;
    }

    public bool Contains(T other)
    {
        return Start.CompareTo(other) < 0 && other.CompareTo(End) < 0
            || Start.Equals(other) && StartBound.IsClosed()
            || End.Equals(other) && EndBound.IsClosed();
    }


    public bool Equals(IAbstractInterval<T>? other)
    {
        if (other is IBoundedInterval<T> otherRightBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherRightBounded.End, otherRightBounded.End)
            && otherRightBounded.EndBound == otherRightBounded.EndBound;
        }
        return false;
    }

    public override string ToString()
        => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, {End}{(EndBound.IsClosed() ? ']' : ')')}";

    public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return IntervalParse.Parse(s, provider);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        throw new NotImplementedException();
    }

    public static Interval<T> Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        throw new NotImplementedException();
    }
}

public sealed record Interval<T, L, R>(T Start, T End) : TypedBoundedInterval<T, L, R>(Start, End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{

    public static implicit operator Interval<T>(Interval<T, L, R> interval)
        => new(interval.Start, interval.End, L.Bound, R.Bound);

    public override string ToString()
        => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, {End}{(EndBound.IsClosed() ? ']' : ')')}";
}