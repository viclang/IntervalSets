using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace IntervalSets.Types;
public sealed class EmptyInterval<T, L, R> : Interval<T, L, R>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{
    public override bool IsEmpty => true;

    public override bool IsSingleton => false;

    [Obsolete("Not supported for EmptyInterval.", true)]
    public static new Interval<T> CreateOrEmpty(T _, T __, Bound ___, Bound ____) => throw new NotSupportedException();

    [Obsolete("Not supported for EmptyInterval.", true)]
    public static new Interval<T> CreateOrEmpty(T _, T __, IntervalType ___) => throw new NotSupportedException();

    public EmptyInterval() : base(default!, default!)
    {
    }

    public static new EmptyInterval<T, L, R> Parse(string s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, L, R>(s, provider);
        if (!new Interval<T, L, R>(start, end).IsEmpty)
        {
            throw new ArgumentException("The provided interval is not empty.");
        }
        return new EmptyInterval<T, L, R>();
    }

    public static new EmptyInterval<T, L, R> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        var (start, end) = IntervalParse.Parse<T, L, R>(s, provider);
        if (!new Interval<T, L, R>(start, end).IsEmpty)
        {
            throw new ArgumentException("The provided interval is not empty.");
        }
        return new EmptyInterval<T, L, R>();
    }

    public static bool TryParse(
               [NotNullWhen(true)] string? s,
                      IFormatProvider? provider,
                             [MaybeNullWhen(false)] out EmptyInterval<T, L, R> result)
    {
        if (IntervalParse.TryParse<T, L, R>(s, provider, out var endpoints))
        {
            if (!new Interval<T, L, R>(endpoints.start, endpoints.end).IsEmpty)
            {
                result = null;
                return false;
            }
            result = new EmptyInterval<T, L, R>();
            return true;
        }
        result = null;
        return false;
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out EmptyInterval<T, L, R> result)
    {
        if (IntervalParse.TryParse<T, L, R>(s, provider, out var endpoints))
        {
            if (!new Interval<T, L, R>(endpoints.start, endpoints.end).IsEmpty)
            {
                result = null;
                return false;
            }
            result = new EmptyInterval<T, L, R>();
            return true;
        }
        result = null;
        return false;
    }
}

public sealed class EmptyInterval<T> : Interval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public override bool IsEmpty => true;

    public override bool IsSingleton => false;

    public static new Interval<T> CreateOrEmpty(T _, T __, Bound startBound, Bound endBound)
        => new EmptyInterval<T>(startBound, endBound);

    public static new Interval<T> CreateOrEmpty(T _, T __, IntervalType intervalType)
        => new EmptyInterval<T>(intervalType);

    public EmptyInterval(Bound startBound, Bound endBound)
        : base(default!, default!, startBound, endBound)
    {
    }

    public EmptyInterval(IntervalType intervalType)
        : base(default!, default!, intervalType)
    {
    }

    public static new EmptyInterval<T> Parse(string s, IFormatProvider? provider = null)
    {
        var result = IntervalParse.Parse<T>(s, provider);
        if (!result.IsEmpty)
        {
            throw new ArgumentException("The provided interval is not empty.");
        }
        return new EmptyInterval<T>(result.IntervalType);
    }

    public static new EmptyInterval<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        var result = IntervalParse.Parse<T>(s, provider);
        if (!result.IsEmpty)
        {
            throw new ArgumentException("The provided interval is not empty.");
        }
        return new EmptyInterval<T>(result.IntervalType);
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out EmptyInterval<T> result)
    {
        if (IntervalParse.TryParse<T>(s, provider, out var interval))
        {
            if (!interval.IsEmpty)
            {
                result = null;
                return false;
            }
            result = new EmptyInterval<T>(interval.IntervalType);
            return true;
        }
        result = null;
        return false;
    }

    public static bool TryParse(
        ReadOnlySpan<char> s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out EmptyInterval<T> result)
    {
        if (IntervalParse.TryParse<T>(s, provider, out var interval))
        {
            if (!interval.IsEmpty)
            {
                result = null;
                return false;
            }
            result = new EmptyInterval<T>(interval.IntervalType);
            return true;
        }
        result = null;
        return false;
    }
}
