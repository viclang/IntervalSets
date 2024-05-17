namespace IntervalSet.Types;
public record UnboundedInterval<T>() : TypedComplementInterval<T, Open, Open>(default!, default!)
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public override bool IsEmpty => false;

    public static explicit operator ComplementInterval<T>(UnboundedInterval<T> _)
        => ComplementInterval<T>.Empty;

    public override string ToString() => "(-Infinity, Infinity)";
}
