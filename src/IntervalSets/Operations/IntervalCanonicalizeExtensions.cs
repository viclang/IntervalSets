using IntervalSets.Types;
using System.Numerics;

namespace IntervalSets.Operations;
public static class IntervalCanonicalizeExtensions
{
    private const string UnboundedErrorMessage = "Unable to canonicalize interval {0} to type Interval<{1}, {2}, {3}>.";

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<T, L, R> Canonicalize<T, L, R>(this Interval<T> value, T? step = null)
        where T : struct, INumber<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        if ((value.StartBound.IsUnbounded() && !L.Bound.IsUnbounded())
            || (value.EndBound.IsUnbounded() && !R.Bound.IsUnbounded()))
        {
            throw new InvalidOperationException(
                string.Format(UnboundedErrorMessage, value, typeof(T).Name, typeof(L).Name, typeof(R).Name));
        }
        if (value is Interval<T, L, R> interval)
        {
            return interval;
        }
        step ??= T.One;
        return Canonicalize<T, L, R>(value, n => n + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateTime, L, R> Canonicalize<L, R>(this Interval<DateTime> value, TimeSpan? step = null)
        where L : struct, IBounded
        where R : struct, IBounded
    {
        if ((value.StartBound.IsUnbounded() && !L.Bound.IsUnbounded())
            || (value.EndBound.IsUnbounded() && !R.Bound.IsUnbounded()))
        {
            throw new InvalidOperationException(
                string.Format(UnboundedErrorMessage, value, nameof(DateTime), typeof(L).Name, typeof(R).Name));
        }
        if (value is Interval<DateTime, L, R> interval)
        {
            return interval;
        }
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<DateTime, L, R>(value, b => b + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateTimeOffset, L, R> Canonicalize<L, R>(this Interval<DateTimeOffset> value, TimeSpan? step = null)
        where L : struct, IBound
        where R : struct, IBound
    {
        if ((value.StartBound.IsUnbounded() && !L.Bound.IsUnbounded())
            || (value.EndBound.IsUnbounded() && !R.Bound.IsUnbounded()))
        {
            throw new InvalidOperationException(
                string.Format(UnboundedErrorMessage, value, nameof(DateTimeOffset), typeof(L).Name, typeof(R).Name));
        }
        if (value is Interval<DateTimeOffset, L, R> interval)
        {
            return interval;
        }
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<DateTimeOffset, L, R>(value, b => b + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateOnly, L, R> Canonicalize<L, R>(this Interval<DateOnly> value, int step = 1)
        where L : struct, IBound
        where R : struct, IBound
    {
        if ((value.StartBound.IsUnbounded() && !L.Bound.IsUnbounded())
            || (value.EndBound.IsUnbounded() && !R.Bound.IsUnbounded()))
        {
            throw new InvalidOperationException(
                string.Format(UnboundedErrorMessage, value, nameof(DateOnly), typeof(L).Name, typeof(R).Name));
        }
        if (value is Interval<DateOnly, L, R> interval)
        {
            return interval;
        }
        return Canonicalize<DateOnly, L, R>(value, b => b.AddDays(step), b => b.AddDays(-step));
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<TimeOnly, L, R> Canonicalize<L, R>(this Interval<TimeOnly> value, TimeSpan? step = null)
        where L : struct, IBound
        where R : struct, IBound
    {
        if ((value.StartBound.IsUnbounded() && !L.Bound.IsUnbounded())
            || (value.EndBound.IsUnbounded() && !R.Bound.IsUnbounded()))
        {
            throw new InvalidOperationException(
                string.Format(UnboundedErrorMessage, value, nameof(TimeOnly), typeof(L).Name, typeof(R).Name));
        }
        if (value is Interval<TimeOnly, L, R> interval)
        {
            return interval;
        }
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<TimeOnly, L, R>(value, b => b.Add(step.Value), b => b.Add(-step.Value));
    }

    private static Interval<T, L, R> Canonicalize<T, L, R>(
        Interval<T> value,
        Func<T, T> add,
        Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        return (L.Bound, R.Bound) switch
        {
            (Bound.Closed, Bound.Closed) => ToClosed<T, L, R>(value, add, substract),
            (Bound.Closed, Bound.Open) => ToClosedOpen<T, L, R>(value, add),
            (Bound.Open, Bound.Closed) => ToOpenClosed<T, L, R>(value, substract),
            (Bound.Open, Bound.Open) => ToOpen<T, L, R>(value, substract, add),
            (Bound.Unbounded, Bound.Open) => ToOpen<T, L, R>(value, _ => _, add),
            (Bound.Unbounded, Bound.Closed) => ToClosed<T, L, R>(value, _ => _, substract),
            (Bound.Open, Bound.Unbounded) => ToOpen<T, L, R>(value, substract, _ => _),
            (Bound.Closed, Bound.Unbounded) => ToClosed<T, L, R>(value, add, _ => _),
            (Bound.Unbounded, Bound.Unbounded) => new Interval<T, L, R>(default!, default!),
            _ => throw new NotImplementedException()
        };
    }

    private static Interval<T, L, R> ToClosed<T, L, R>(
        IAbstractInterval<T> value,
        Func<T, T> add,
        Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        return new Interval<T, L, R>(
            value.StartBound.IsClosed() ? value.Start : add(value.Start),
            value.EndBound.IsClosed() ? value.End : substract(value.End));
    }

    private static Interval<T, L, R> ToClosedOpen<T, L, R>(
       IAbstractInterval<T> value,
       Func<T, T> add)
       where T : notnull, IComparable<T>, ISpanParsable<T>
       where L : struct, IBound
       where R : struct, IBound
    {
        return new Interval<T, L, R>(
            value.StartBound.IsClosed() ? value.Start : add(value.Start),
            value.EndBound.IsOpen() ? value.End : add(value.End));
    }

    private static Interval<T, L, R> ToOpenClosed<T, L, R>(IAbstractInterval<T> value, Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        return new Interval<T, L, R>(
            value.StartBound.IsOpen() ? value.Start : substract(value.Start),
            value.EndBound.IsClosed() ? value.End : substract(value.End));
    }

    private static Interval<T, L, R> ToOpen<T, L, R>(IAbstractInterval<T> value, Func<T, T> substract, Func<T, T> add)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        return new Interval<T, L, R>(
            value.StartBound.IsOpen() ? value.Start : substract(value.Start),
            value.EndBound.IsOpen() ? value.End : add(value.End));
    }
}
