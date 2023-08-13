using Unbounded;

namespace IntervalRecords.Tests.TestData
{
    public static class OverlapFactory
    {
        public static Interval<int> Before(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.Start.Add(offset).Substract(value.Length()),
                End = value.Start.Substract(offset)
            };
        }

        public static Interval<int> Meets(Interval<int> value)
        {
            return value with
            {
                Start = value.Start.Substract(value.Length()),
                End = value.Start
            };
        }

        public static Interval<int> Overlaps(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.Start.Substract(offset),
                End = value.Start.Add(offset)
            };
        }

        public static Interval<int> Starts(Interval<int> value, int offset)
        {
            return value with
            {
                End = value.End.Substract(offset)
            };
        }

        public static Interval<int> RightboundedStarts(Interval<int> value, int offset)
        {
            return value with
            {
                Start = Unbounded<int>.NegativeInfinity,
                End = value.End.Substract(offset)
            };
        }

        public static Interval<int> ContainedBy(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.Start.Add(offset),
                End = value.End.Substract(offset)
            };
        }

        public static Interval<int> Finishes(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.Start.Add(offset)
            };
        }

        public static Interval<int> LeftboundedFinishes(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.Start.Add(offset),
                End = Unbounded<int>.PositiveInfinity
            };
        }

        public static Interval<int> LeftBoundedEqual(Interval<int> value)
        {
            return value with
            {
                End = Unbounded<int>.PositiveInfinity
            };
        }

        public static Interval<int> RightBoundedEqual(Interval<int> value)
        {
            return value with
            {
                Start = Unbounded<int>.NegativeInfinity
            };
        }
        public static Interval<int> UnBoundedEqual(Interval<int> value)
        {
            return value with
                {
                    Start = Unbounded<int>.NegativeInfinity,
                    End = Unbounded<int>.PositiveInfinity
                };
        }

        public static Interval<int> FinishedBy(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.Start.Substract(offset)
            };
        }


        public static Interval<int> LeftBoundedFinishedBy(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.Start.Substract(offset),
                End = Unbounded<int>.PositiveInfinity
            };
        }

        public static Interval<int> Contains(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.Start.Substract(offset),
                End = value.End.Add(offset)
            };
        }

        public static Interval<int> StartedBy(Interval<int> value, int offset)
        {
            return value with
            {
                End = value.End.Add(offset)
            };
        }


        public static Interval<int> RightboundedStartedBy(Interval<int> value, int offset)
        {
            return value with
            {
                Start = Unbounded<int>.NegativeInfinity,
                End = value.End.Add(offset)
            };
        }

        public static Interval<int> OverlappedBy(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.End.Substract(offset),
                End = value.End.Add(offset)
            };
        }

        public static Interval<int> MetBy(Interval<int> value)
        {
            return value with
            {
                Start = value.End,
                End = value.End.Add(value.Length())
            };
        }

        public static Interval<int> After(Interval<int> value, int offset)
        {
            return value with
            {
                Start = value.End.Add(offset),
                End = value.End.Add(offset).Add(value.Length())
            };
        }
    }
}
