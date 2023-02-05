using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public enum IntervalType : byte
    {
        Closed = 0,
        ClosedOpen = 1,
        OpenClosed = 2,
        Open = 3,
    }

    public static class IntervalTypeExtensions
    {
        [Pure]
        public static (bool, bool) ToTuple(this IntervalType intervalType) => intervalType switch
        {
            IntervalType.Closed => (true, true),
            IntervalType.ClosedOpen => (true, false),
            IntervalType.OpenClosed => (false, true),
            IntervalType.Open => (false, false),
            _ => throw new NotImplementedException()
        };
    }

    public static partial class Interval
    {
        [Pure]
        public static IntervalType GetIntervalType<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (value.StartInclusive, value.EndInclusive) switch
            {
                (true, true) => IntervalType.Closed,
                (true, false) => IntervalType.ClosedOpen,
                (false, true) => IntervalType.OpenClosed,
                (false, false) => IntervalType.Open,
            };

        [Pure]
        public static bool IsHalfOpen<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.StartInclusive && !value.EndInclusive || !value.StartInclusive && value.EndInclusive;

    }
}
