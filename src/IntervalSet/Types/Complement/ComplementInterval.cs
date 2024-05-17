using IntervalSet.Types.Bounded;
using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;
public record ComplementInterval<T> : IComplementInterval<T>, ISpanParsable<ComplementInterval<T>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    private readonly IntervalType _intervalType;

    public T Start { get; init; }

    public T End { get; init; }

    public Bound StartBound
    {
        get => _intervalType.StartBound();
        init => _intervalType = IntervalTypeFactory.Create(value, EndBound);
    }

    public Bound EndBound
    {
        get => _intervalType.EndBound();
        init => _intervalType = IntervalTypeFactory.Create(StartBound, value);
    }

    public static ComplementInterval<T> Empty => new(default!, default!, IntervalType.Open);

    public bool IsEmpty => End.CompareTo(Start) is int comparison
        && comparison < 0 || comparison == 0 && StartBound.IsOpen() && EndBound.IsOpen();

    public ComplementInterval(T start, T end, Bound startBound, Bound endBound)
        : this(start, end, IntervalTypeFactory.Create(startBound, endBound))
    {
    }

    public ComplementInterval(T start, T end, IntervalType intervalType)
    {
        Start = start;
        End = end;
        _intervalType = intervalType;
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

    public static ComplementInterval<T> Parse(string s, IFormatProvider? provider)
    {
        return ComplementIntervalParse.Parse<T>(s, provider);
    }

    public static ComplementInterval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return ComplementIntervalParse.Parse<T>(s, provider);
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out ComplementInterval<T> result)
    {
        return ComplementIntervalParse.TryParse(s, provider, out result);
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out ComplementInterval<T> result)
    {
        return ComplementIntervalParse.TryParse(s, provider, out result);
    }
}

public record ComplementInterval<T, L, R>(T Start, T End)
    : TypedComplementInterval<T, L, R>(Start, End), ISpanParsable<ComplementInterval<T, L, R>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{
    public static implicit operator ComplementInterval<T>(ComplementInterval<T, L, R> complementInterval)
        => new(complementInterval.Start, complementInterval.End, L.Bound, R.Bound);

    public override string ToString()
        => $"{(StartBound.IsClosed() ? ']' : ')')}{Start}, {End}{(EndBound.IsClosed() ? '[' : '(')}";

    public static ComplementInterval<T, L, R> Parse(string s, IFormatProvider? provider)
    {
        return ComplementIntervalParse.Parse<T, L, R>(s, provider);
    }

    public static ComplementInterval<T, L, R> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return ComplementIntervalParse.Parse<T, L, R>(s, provider);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ComplementInterval<T, L, R> result)
    {
        return ComplementIntervalParse.TryParse(s, provider, out result);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out ComplementInterval<T, L, R> result)
    {
        return ComplementIntervalParse.TryParse(s, provider, out result);
    }
}