using IntervalSet.Types.Bounded;
using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;

public sealed record Interval<T> : IBoundedInterval<T>, ISpanParsable<Interval<T>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public T Start { get; init; }

    public T End { get; init; }

    public IntervalType IntervalType { get; private init; }

    public Bound StartBound
    {
        get => IntervalType.StartBound();
        init => IntervalType = IntervalTypeFactory.Create(value, EndBound);
    }

    public Bound EndBound
    {
        get => IntervalType.EndBound();
        init => IntervalType = IntervalTypeFactory.Create(StartBound, value);
    }

    public bool IsEmpty => End.CompareTo(Start) is int comparison
        && comparison < 0 || comparison == 0 && StartBound.IsOpen() && EndBound.IsOpen();

    public Interval(T start, T end, Bound startBound, Bound endBound)
        : this(start, end, IntervalTypeFactory.Create(startBound, endBound))
    {
    }

    public Interval(T start, T end, IntervalType intervalType)
    {
        Start = start;
        End = end;
        IntervalType = intervalType;
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

    public static Interval<T> Parse(string s, IFormatProvider? provider)
    {
        return BoundedIntervalParse.Parse<T>(s, provider);
    }

    public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return BoundedIntervalParse.Parse<T>(s, provider);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return BoundedIntervalParse.TryParse<T>(s, provider, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return BoundedIntervalParse.TryParse<T>(s, provider, out result);
    }

    public override string ToString()
        => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, {End}{(EndBound.IsClosed() ? ']' : ')')}";
}

public sealed record Interval<T, L, R>(T Start, T End) : TypedBoundedInterval<T, L, R>(Start, End), ISpanParsable<Interval<T, L, R>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{
    public static implicit operator Interval<T>(Interval<T, L, R> interval)
        => new(interval.Start, interval.End, interval.StartBound, interval.EndBound);

    public override string ToString()
        => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, {End}{(EndBound.IsClosed() ? ']' : ')')}";

    public static Interval<T, L, R> Parse(string s, IFormatProvider? provider)
    {
        return BoundedIntervalParse.Parse<T, L, R>(s, provider);
    }

    public static Interval<T, L, R> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return BoundedIntervalParse.Parse<T, L, R>(s, provider);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T, L, R> result)
    {
        return BoundedIntervalParse.TryParse(s, provider, out result);
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T, L, R> result)
    {
        return BoundedIntervalParse.TryParse(s, provider, out result);
    }
}