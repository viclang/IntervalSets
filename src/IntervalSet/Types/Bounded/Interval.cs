using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;

public class Interval<T> : IAbstractInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public T Start { get; }

    public T End { get; }

    public virtual IntervalType IntervalType { get; }

    public virtual Bound StartBound => IntervalType.StartBound();

    public virtual Bound EndBound => IntervalType.EndBound();

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

    internal Interval(T start, T end)
    {
        Start = start;
        End = end;
    }

    public bool Contains(T other)
    {
        return Start.CompareTo(other) < 0 && other.CompareTo(End) < 0
            || Start.Equals(other) && StartBound.IsClosed()
            || End.Equals(other) && EndBound.IsClosed();
    }

    public override bool Equals(object? obj) => Equals(obj as Interval<T>);

    public bool Equals(Interval<T>? other)
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

    public virtual bool IsEmpty => End.CompareTo(Start) is int comparison
        && comparison < 0 || comparison == 0 && StartBound.IsOpen() && EndBound.IsOpen();

    public static Interval<T> Parse(string s, IFormatProvider? provider)
    {
        return IntervalParse.Parse<T>(s, provider);
    }

    public static Interval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return IntervalParse.Parse<T>(s, provider);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return IntervalParse.TryParse<T>(s, provider, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Interval<T> result)
    {
        return IntervalParse.TryParse<T>(s, provider, out result);
    }

    public override string ToString()
        => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, {End}{(EndBound.IsClosed() ? ']' : ')')}";
}

public class Interval<T, L, R> : Interval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{
    public override Bound StartBound => L.Bound;

    public override Bound EndBound => R.Bound;

    public override IntervalType IntervalType => IntervalTypeFactory.Create(StartBound, EndBound);

    public Interval(T start, T end) : base(start, end)
    {
    }

    public static new Interval<T, L, R> Parse(string s, IFormatProvider? provider)
    {
        var (start, end) = IntervalParse.Parse<T, L, R>(s, provider);
        return new(start, end);
    }

    public static new Interval<T, L, R> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
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
        if (IntervalParse.TryParse<T, Open, Open>(s, provider, out var tupleResult))
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
        if (IntervalParse.TryParse<T, Open, Open>(s, provider, out var tupleResult))
        {
            result = new(tupleResult.start, tupleResult.end);
            return true;
        }
        return false;
    }
}