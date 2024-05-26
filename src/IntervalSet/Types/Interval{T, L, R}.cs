using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IntervalSet.Types;
public class Interval<T, L, R> : IInterval<T>
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
        && !StartBound.IsUnbounded() && !EndBound.IsUnbounded()
        && (comparison < 0 || comparison == 0 && (StartBound.IsOpen() || EndBound.IsOpen()));

    public bool IsSingleton => StartBound.IsClosed() && EndBound.IsClosed()
        && EqualityComparer<T>.Default.Equals(Start, End);

    public Interval(T start, T end)
    {
        Start = L.Bound.IsUnbounded() ? default! : start;
        End = R.Bound.IsUnbounded() ? default! : end;
    }

    public override bool Equals(object? obj) => Equals(obj as IInterval<T>);

    public bool Equals(IInterval<T>? other)
    {
        return other is not null
            && EqualityComparer<T>.Default.Equals(Start, other.Start)
            && EqualityComparer<T>.Default.Equals(End, other.End)
            && other.EndBound == other.EndBound;
    }

    public override int GetHashCode() => HashCode.Combine(Start, End, StartBound, EndBound);

    public static bool operator ==(Interval<T, L, R> left, Interval<T, L, R> right)
        => left.Equals(right);

    public static bool operator !=(Interval<T, L, R> left, Interval<T, L, R> right)
        => !left.Equals(right);

    public static implicit operator Interval<T>(Interval<T, L, R> interval)
    {
        return new(interval.Start, interval.End, L.Bound, R.Bound);
    }

    public static Interval<T, L, R> Parse(string s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, L, R>(s, provider);
        return new(start, end);
    }

    public static Interval<T, L, R> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
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

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(StartBound.IsClosed() ? '[' : '(');
        sb.Append(StartBound.IsUnbounded() ? "-Infinity" : Start);
        sb.Append(", ");
        sb.Append(EndBound.IsUnbounded() ? "Infinity" : End);
        sb.Append(EndBound.IsClosed() ? ']' : ')');
        return sb.ToString();
    }
}