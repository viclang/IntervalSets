using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IntervalSet.Types;

public class Interval<T> : IInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public T Start { get; }

    public T End { get; }

    public virtual IntervalType IntervalType { get; }

    public virtual Bound StartBound => IntervalType.StartBound();

    public virtual Bound EndBound => IntervalType.EndBound();

    public virtual bool IsEmpty => End.CompareTo(Start) is int comparison
        && !StartBound.IsUnbounded() && !EndBound.IsUnbounded()
        && (comparison < 0 || comparison == 0 && IntervalType == IntervalType.Open);

    public Interval(T start, T end, Bound startBound, Bound endBound)
        : this(start, end, IntervalTypeFactory.Create(startBound, endBound))
    {
    }

    public Interval(T start, T end, IntervalType intervalType)
    {
        Start = StartBound.IsUnbounded() ? default! : start;
        End = EndBound.IsUnbounded() ? default! : end;
        IntervalType = intervalType;
    }

    internal Interval(T start, T end)
    {
        Start = start;
        End = end;
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

    public static bool operator ==(Interval<T> left, Interval<T> right)
        => left.Equals(right);

    public static bool operator !=(Interval<T> left, Interval<T> right)
        => !left.Equals(right);

    public static Interval<T> Parse(string s, IFormatProvider? provider = null)
    {
        return IntervalParse.Parse<T>(s, provider);
    }

    public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider= null)
    {
        return IntervalParse.Parse<T>(s, provider);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return IntervalParse.TryParse(s, provider, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return IntervalParse.TryParse(s, provider, out result);
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