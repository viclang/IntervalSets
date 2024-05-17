using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;
public record RightBoundedInterval<T>(T End, Bound EndBound) : IRightBoundedInterval<T>, ISpanParsable<RightBoundedInterval<T>>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public bool IsEmpty => false;

    public Bound StartBound => Bound.Open;

    public static RightBoundedInterval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static RightBoundedInterval<T> Parse(string s, IFormatProvider? provider)
    {
        return IntervalRegex.ParseRightBounded<T>(s, provider);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out RightBoundedInterval<T> result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out RightBoundedInterval<T> result)
    {
        throw new NotImplementedException();
    }

    public bool Equals(IAbstractInterval<T>? other)
    {
        if (other is IRightBoundedInterval<T> otherRightBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherRightBounded.End, otherRightBounded.End)
            && otherRightBounded.EndBound == otherRightBounded.EndBound;
        }
        return false;
    }

    public override string ToString() => $"(-Infinity, {End}{(EndBound.IsClosed() ? ']' : ')')}";
}

public record RightBoundedInterval<T, R>(T End) : TypedRightBoundedInterval<T, R>(End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where R : struct, IBound
{
    public override bool IsEmpty => false;

    public static implicit operator RightBoundedInterval<T>(RightBoundedInterval<T, R> rigthBoundedInterval)
        => new(rigthBoundedInterval.End, R.Bound);

    public override string ToString() => $"(-Infinity, {End}{(EndBound.IsClosed() ? ']' : ')')}";
}
