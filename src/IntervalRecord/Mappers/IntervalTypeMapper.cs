using IntervalRecord.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static IntervalType GetIntervalType<T>(this Interval<T> interval) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (interval.Start.IsInfinite, interval.End.IsInfinite) switch
            {
                (false, false) => IntervalType.Bounded,
                (true, true) => IntervalType.Unbounded,
                (true, false) => IntervalType.LeftBounded,
                (false, true) => IntervalType.RightBounded
            };


        public static bool IsHalfBounded<T>(this Interval<T> interval) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => interval.GetIntervalType() is IntervalType.LeftBounded or IntervalType.RightBounded;

        public static bool IsEmpty(this Interval<int> interval, int step)
        {
            var length = interval.Length();
            return !length.IsInfinite
                && length.Value <= step
                && interval.GetBoundaryType() != BoundaryType.Closed;
        }

        public static bool IsEmpty(this Interval<DateOnly> interval, int step)
        {
            var length = interval.Length();
            return !length.IsInfinite
                && length.Value <= step
                && interval.GetBoundaryType() != BoundaryType.Closed;
        }

        public static bool IsEmpty(this Interval<DateTime> interval, TimeSpan step)
        {
            var length = interval.Length(step);
            return !length.IsInfinite
                && length.Value <= step
                && interval.GetBoundaryType() != BoundaryType.Closed;
        }

        public static bool IsEmpty(this Interval<DateTimeOffset> interval, TimeSpan step)
        {
            var length = interval.Length(step);
            return !length.IsInfinite
                && length.Value <= step
                && interval.GetBoundaryType() != BoundaryType.Closed;
        }
        internal static bool IsEmpty<T>(this Interval<T> interval) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => interval.GetIntervalType() == IntervalType.Bounded
                && interval.Start.Value.Equals(interval.End.Value)
                && interval.GetBoundaryType() != BoundaryType.Closed;

        public static bool IsSingleton<T>(this Interval<T> interval) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => interval.GetBoundaryType() == BoundaryType.Closed
                && interval.Start.Value.Equals(interval.End.Value);
    }
}
