using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;
public record LeftBoundedInterval<T>(T Start, Bound StartBound) : ILeftBoundedInterval<T>, ISpanParsable<LeftBoundedInterval<T>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public bool IsEmpty => false;

    public Bound EndBound => Bound.Open;

    public static LeftBoundedInterval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return IntervalRegex.ParseLeftBounded<T>(s, provider);
    }

    public static LeftBoundedInterval<T> Parse(string s, IFormatProvider? provider)
    {
        return IntervalRegex.ParseLeftBounded<T>(s, provider);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out LeftBoundedInterval<T> result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out LeftBoundedInterval<T> result)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IAbstractInterval<T>? other)
    {
        if (other is ILeftBoundedInterval<T> otherLeftBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherLeftBounded.Start, otherLeftBounded.Start)
            && otherLeftBounded.StartBound == otherLeftBounded.StartBound;
        }
        return false;
    }

    public override string ToString() => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, Infinity)";
}

public record LeftBoundedInterval<T, L>(T Start) : TypedLeftBoundedInterval<T, L>(Start)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
{
    public override bool IsEmpty => false;

    public static implicit operator LeftBoundedInterval<T>(LeftBoundedInterval<T, L> leftBoundedInterval)
        => new(leftBoundedInterval.Start, L.Bound);

    public override string ToString() => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, Infinity)";
}
