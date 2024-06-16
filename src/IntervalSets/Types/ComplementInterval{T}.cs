using System.Text;

namespace IntervalSets.Types;
public class ComplementInterval<T> : IAbstractInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public T Start { get; init; }

    public T End { get; init; }

    public virtual IntervalType IntervalType { get; }

    public virtual Bound StartBound => IntervalType.StartBound();

    public virtual Bound EndBound => IntervalType.EndBound();

    public static ComplementInterval<T> Empty => new(default!, default!, IntervalType.Open);

    public bool IsEmpty => End.CompareTo(Start) is int comparison
        && StartBound.IsUnbounded() && EndBound.IsUnbounded()
        || comparison < 0 || comparison == 0 && StartBound.IsOpen() && EndBound.IsOpen();

    public ComplementInterval(T start, T end, Bound startBound, Bound endBound)
        : this(start, end, IntervalTypeFactory.Create(startBound, endBound))
    {
    }

    public ComplementInterval(T start, T end, IntervalType intervalType)
    {
        Start = start;
        End = end;
        IntervalType = intervalType;
    }

    internal ComplementInterval(T start, T end)
    {
        Start = start;
        End = end;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(StartBound.IsClosed() ? ']' : ')');
        sb.Append(StartBound.IsUnbounded() ? "-Infinity" : Start);
        sb.Append(", ");
        sb.Append(EndBound.IsUnbounded() ? "Infinity" : End);
        sb.Append(EndBound.IsClosed() ? '[' : '(');
        return sb.ToString();
    }
}