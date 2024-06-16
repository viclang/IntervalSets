using System.Diagnostics.CodeAnalysis;

namespace IntervalSets.Types;
public class Interval<T, L, R> : Interval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{
    public sealed override Bound StartBound => L.Bound;

    public sealed override Bound EndBound => R.Bound;

    public sealed override IntervalType IntervalType => IntervalTypeFactory.Create(StartBound, EndBound);

    public static new Interval<T, L, R> Empty => new EmptyInterval<T, L, R>();

    public static Interval<T, L, R> CreateOrEmpty(T start, T end)
    {
        return new Interval<T, L, R>(start, end);
    }

    public Interval(T start, T end) : base(start, end)
    {
        if (R.Bound.IsBounded() && end.CompareTo(start) < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(end), "The end value must be greater than or equal to the start value.");
        }
    }

    public static new Interval<T, L, R> Parse(string s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, L, R>(s, provider);
        return new(start, end);
    }

    public static new Interval<T, L, R> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, L, R>(s, provider);
        return new(start, end);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T, L, R> result)
    {
        result = null;
        if (IntervalParse.TryParse<T, L, R>(s, provider, out var tupleResult))
        {
            result = new(tupleResult.start, tupleResult.end);
            return true;
        }
        return false;
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T, L, R> result)
    {
        result = null;
        if (IntervalParse.TryParse<T, L, R>(s, provider, out var tupleResult))
        {
            result = new(tupleResult.start, tupleResult.end);
            return true;
        }
        return false;
    }
}
