using Intervals.Bounds;

namespace Intervals.Types;
public record ComplementInterval<T> : IComplementInterval<T>
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

    public static ComplementInterval<T> Empty => new(default!, default!, BoundPair.Open);

    public bool IsEmpty => End.CompareTo(Start) is int comparison
        && comparison < 0 || comparison == 0 && StartBound.IsOpen() && EndBound.IsOpen();

    public ComplementInterval(T start, T end, Bound startBound, Bound endBound)
        : this(start, end, BoundPairFactory.Create(startBound, endBound))
    {
    }

    public ComplementInterval(T start, T end, BoundPair boundPair)
    {
        Start = start;
        End = end;
        _boundPair = boundPair;
    }

    public bool Equals(IAbstractInterval<T>? other)
    {
        if (other is IComplementInterval<T> otherRightBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherRightBounded.End, otherRightBounded.End)
            && otherRightBounded.EndBound == otherRightBounded.EndBound;
        }
        return false;
    }

    public override string ToString()
        => $"{(StartBound.IsClosed() ? ']' : ')')}{Start}, {End}{(EndBound.IsClosed() ? '[' : '(')}";
}


public record ComplementInterval<T, L, R>(T Start, T End) : TypedComplementInterval<T, L, R>(Start, End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{
    public static implicit operator ComplementInterval<T>(ComplementInterval<T, L, R> complementInterval)
        => new(complementInterval.Start, complementInterval.End, L.Bound, R.Bound);

    public override string ToString()
        => $"{(StartBound.IsClosed() ? ']' : ')')}{Start}, {End}{(EndBound.IsClosed() ? '[' : '(')}";
}