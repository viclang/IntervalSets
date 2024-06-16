using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IntervalSets.Types;

public class Interval<T> : IAbstractInterval<T>, IComparable<Interval<T>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public T Start { get; }

    public T End { get; }

    public virtual IntervalType IntervalType { get; }

    public virtual Bound StartBound => IntervalType.StartBound();

    public virtual Bound EndBound => IntervalType.EndBound();

    public virtual bool IsEmpty => Start.CompareTo(End) == 0
        && !StartBound.IsUnbounded() && !EndBound.IsUnbounded()
        && (StartBound.IsOpen() || EndBound.IsOpen());

    public virtual bool IsSingleton => StartBound.IsClosed() && EndBound.IsClosed()
        && EqualityComparer<T>.Default.Equals(Start, End);

    public static Interval<T> Unbounded => new(default!, default!, IntervalType.Unbounded);

    public static Interval<T> Empty => new EmptyInterval<T>(IntervalType.Open);

    public static Interval<T> CreateOrEmpty(T start, T end, Bound startBound, Bound endBound)
    {
        return new Interval<T>(start, end, IntervalTypeFactory.Create(startBound, endBound));
    }

    public static Interval<T> CreateOrEmpty(T start, T end, IntervalType intervalType)
    {
        if (intervalType.IsBounded() && start.CompareTo(end) > 0)
        {
            return new EmptyInterval<T>(intervalType);
        }

        return new Interval<T>(start, end, intervalType);
    }

    public Interval(T start, T end, Bound startBound, Bound endBound)
        : this(start, end, IntervalTypeFactory.Create(startBound, endBound))
    {
    }

    public Interval(T start, T end, IntervalType intervalType)
    {
        if (intervalType.IsBounded() && end.CompareTo(start) < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(end), "The end value must be greater than or equal to the start value.");
        }

        Start = StartBound.IsUnbounded() ? default! : start;
        End = EndBound.IsUnbounded() ? default! : end;
        IntervalType = intervalType;
    }

    internal Interval(T start, T end)
    {
        Start = start;
        End = end;
    }

    public bool IsDisjoint(Interval<T> other)
    {
        if (IsEmpty || other.IsEmpty)
        {
            return true;
        }
        var startComparison = StartBound.IsUnbounded() || other.EndBound.IsUnbounded() ? -1 : Start.CompareTo(other.End);
        var endComparison = other.StartBound.IsUnbounded() || EndBound.IsUnbounded() ? -1 : other.Start.CompareTo(End);

        return startComparison > 0 || endComparison > 0
            || startComparison == 0 && !StartBound.IsClosed() && !other.EndBound.IsClosed()
            || endComparison == 0 && !EndBound.IsClosed() && !other.StartBound.IsClosed();
    }

    public int CompareTo(Interval<T>? other)
    {
        if (other is null) return 1;

        if (IsEmpty && other.IsEmpty) return 0;
        if (IsEmpty) return -1;
        if (other.IsEmpty)  return 1;

        int result = CompareBoundaries(StartBound, other.StartBound, Start, other.Start);
        if (result != 0) return result;

        return CompareBoundaries(EndBound, other.EndBound, End, other.End);
    }

    private int CompareBoundaries(Bound thisBound, Bound otherBound, T thisValue, T otherValue)
    {
        return (thisBound.IsUnbounded(), otherBound.IsUnbounded()) switch
        {
            (true, true) => 0,
            (true, false) => -1,
            (false, true) => 1,
            _ => CompareValues(thisValue, thisBound, otherValue, otherBound)
        };
    }

    private int CompareValues(T thisValue, Bound thisBound, T otherValue, Bound otherBound)
    {
        int result = thisValue.CompareTo(otherValue);
        return result != 0 ? result : thisBound.CompareTo(otherBound);
    }

    public override bool Equals(object? obj) => Equals(obj as Interval<T>);

    public bool Equals(Interval<T>? other)
    {
        if (other is null) return false;
        if (IsEmpty && other.IsEmpty) return true;
        if (IsEmpty || other.IsEmpty) return false;

        return EqualityComparer<T>.Default.Equals(Start, other.Start)
            && EqualityComparer<T>.Default.Equals(End, other.End)
            && StartBound == other.StartBound
            && EndBound == other.EndBound;
    }

    public override int GetHashCode()
    {
        if (IsEmpty)
        {
            return 0;
        }
        return HashCode.Combine(Start, End, StartBound, EndBound);
    }

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

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T> result)
    {
        return IntervalParse.TryParse(s, provider, out result);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Interval<T> result)
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