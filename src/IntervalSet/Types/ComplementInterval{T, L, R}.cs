namespace IntervalSet.Types;
public class ComplementInterval<T, L, R> : IInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{
    public T Start { get; }

    public T End { get; }

    public Bound StartBound => L.Bound;

    public Bound EndBound => R.Bound;

    public IntervalType IntervalType => IntervalTypeFactory.Create(L.Bound, R.Bound);

    public bool IsEmpty => End.CompareTo(Start) is int comparison
        && comparison < 0 || comparison == 0 && L.Bound.IsOpen() && L.Bound.IsOpen();

    public ComplementInterval(T start, T end)
    {
        Start = start;
        End = end;
    }

    public static implicit operator ComplementInterval<T>(ComplementInterval<T, L, R> complementInterval)
        => new(complementInterval.Start, complementInterval.End, L.Bound, R.Bound);

    public override string ToString()
        => $"{(L.Bound.IsClosed() ? ']' : ')')}{Start}, {End}{(R.Bound.IsClosed() ? '[' : '(')}";
}