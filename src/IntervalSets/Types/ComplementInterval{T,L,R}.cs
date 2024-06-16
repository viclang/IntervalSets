namespace IntervalSets.Types;
public class ComplementInterval<T, L, R> : ComplementInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{

    public override Bound StartBound => L.Bound;

    public override Bound EndBound => R.Bound;

    public override IntervalType IntervalType => IntervalTypeFactory.Create(L.Bound, R.Bound);

    public ComplementInterval(T start, T end) : base(start, end)
    {
    }
}