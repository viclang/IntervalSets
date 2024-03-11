using Unbounded;

namespace IntervalRecords.Tests.TestData
{
    public static class IntervalRelationFactory<T, TOffset>
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TOffset : struct, IEquatable<TOffset>, IComparable<TOffset>, ISpanParsable<TOffset>
    {
        public static Interval<T> Before(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.Start.Add(offset).Substract(value.TLength<T, TOffset>()),
                End = value.Start.Substract(offset)
            };
        }

        public static Interval<T> Meets(Interval<T> value)
        {
            return value with
            {
                Start = value.Start.Substract(value.TLength<T, TOffset>()),
                End = value.Start
            };
        }

        public static Interval<T> Overlaps(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.Start.Substract(offset),
                End = value.Start.Add(offset)
            };
        }

        public static Interval<T> Starts(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                End = value.End.Substract(offset)
            };
        }

        public static Interval<T> RightboundedStarts(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = Unbounded<T>.NegativeInfinity,
                End = value.End.Substract(offset)
            };
        }

        public static Interval<T> ContainedBy(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.Start.Add(offset),
                End = value.End.Substract(offset)
            };
        }

        public static Interval<T> Finishes(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.Start.Add(offset)
            };
        }

        public static Interval<T> LeftboundedFinishes(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.Start.Add(offset),
                End = Unbounded<T>.PositiveInfinity
            };
        }

        public static Interval<T> LeftBoundedEqual(Interval<T> value)
        {
            return value with
            {
                End = Unbounded<T>.PositiveInfinity
            };
        }

        public static Interval<T> RightBoundedEqual(Interval<T> value)
        {
            return value with
            {
                Start = Unbounded<T>.NegativeInfinity
            };
        }
        public static Interval<T> UnBoundedEqual(Interval<T> value)
        {
            return value with
            {
                Start = Unbounded<T>.NegativeInfinity,
                End = Unbounded<T>.PositiveInfinity
            };
        }

        public static Interval<T> FinishedBy(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.Start.Substract(offset)
            };
        }


        public static Interval<T> LeftBoundedFinishedBy(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.Start.Substract(offset),
                End = Unbounded<T>.PositiveInfinity
            };
        }

        public static Interval<T> Contains(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.Start.Substract(offset),
                End = value.End.Add(offset)
            };
        }

        public static Interval<T> StartedBy(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                End = value.End.Add(offset)
            };
        }


        public static Interval<T> RightboundedStartedBy(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = Unbounded<T>.NegativeInfinity,
                End = value.End.Add(offset)
            };
        }

        public static Interval<T> OverlappedBy(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.End.Substract(offset),
                End = value.End.Add(offset)
            };
        }

        public static Interval<T> MetBy(Interval<T> value)
        {
            return value with
            {
                Start = value.End,
                End = value.End.Add(value.TLength<T, TOffset>())
            };
        }

        public static Interval<T> After(Interval<T> value, Unbounded<TOffset> offset)
        {
            return value with
            {
                Start = value.End.Add(offset),
                End = value.End.Add(offset).Add(value.TLength<T, TOffset>())
            };
        }
    }
}
