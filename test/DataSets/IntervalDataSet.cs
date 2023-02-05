using IntervalRecord.Extensions;
using System;
using Xunit;

namespace IntervalRecord.Tests.DataSets
{
    public readonly struct IntervalDataSet<T, TOffset>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public Interval<T> Reference { get; init; }
        public Interval<T> Before { get; init; }
        public Interval<T> Contains { get; init; }
        public Interval<T> After { get; init; }
        public Interval<T> Meets { get; init; }
        public Interval<T> Overlaps { get; init; }
        public Interval<T> Starts { get; init; }
        public Interval<T> ContainedBy { get; init; }
        public Interval<T> Finishes { get; init; }
        public Interval<T> Equal { get; init; }
        public Interval<T> FinishedBy { get; init; }
        public Interval<T> StartedBy { get; init; }
        public Interval<T> OverlappedBy { get; init; }
        public Interval<T> MetBy { get; init; }

        public IntervalDataSet(Interval<T> reference, TOffset offset)
        {
            Reference = reference;
            var dataSetCreator = OffsetCreator.Create(Reference, offset);
            Before = dataSetCreator.Before;
            Contains = dataSetCreator.Contains;
            After = dataSetCreator.After;
            Meets = Reference with { Start = Before.Start, End = Reference.Start };
            Overlaps = Reference with { Start = Contains.End, End = After.End };
            Starts = Reference with { End = After.Start };
            ContainedBy = Reference with { Start = Before.Start, End = After.End };
            Finishes = Reference with { Start = Contains.Start };
            Equal = Reference with { };
            FinishedBy = Reference with { Start = Before.Start };
            StartedBy = Reference with { End = Contains.End };
            OverlappedBy = Reference with { Start = Before.Start, End = Contains.Start };
            MetBy = Reference with { Start = Reference.End, End = After.End };
        }

        public TheoryData<Interval<T>, Interval<T>, bool> OverlapsWith => new TheoryData<Interval<T>, Interval<T>, bool>
        {
            { Reference, Before, false },
            { Reference, Meets, Reference.GetIntervalType() == BoundaryType.Closed },
            { Reference, Overlaps, true },
            { Reference, Starts, true },
            { Reference, ContainedBy, true },
            { Reference, Finishes, true },
            { Reference, Equal, true },
            { Reference, FinishedBy, true },
            { Reference, Contains, true },
            { Reference, StartedBy, true },
            { Reference, OverlappedBy, true },
            { Reference, MetBy, Reference.GetIntervalType() == BoundaryType.Closed },
            { Reference, After, false },
            { Reference, Before with { End = null, EndInclusive = false }, true },
            { Reference, Before with { Start = null, StartInclusive = false }, false },
            { Reference, Contains with { Start = null, StartInclusive = false }, true },
            { Reference, Contains with { End = null, EndInclusive = false }, true },
            { Reference, After with { Start = null, StartInclusive = false }, true },
            { Reference, After with { End = null, EndInclusive = false }, false },
            { Reference, Equal with { Start = null, End = null, StartInclusive = false, EndInclusive = false }, true },
        };

        public TheoryData<Interval<T>, Interval<T>, bool> IsConnected => new TheoryData<Interval<T>, Interval<T>, bool>
        {
            { Reference, Before, false },
            { Reference, Meets, Reference.GetIntervalType() == BoundaryType.Closed },
            { Reference, Overlaps, true },
            { Reference, Starts, true },
            { Reference, ContainedBy, true },
            { Reference, Finishes, true },
            { Reference, Equal, true },
            { Reference, FinishedBy, true },
            { Reference, Contains, true },
            { Reference, StartedBy, true },
            { Reference, OverlappedBy, true },
            { Reference, MetBy, Reference.GetIntervalType() == BoundaryType.Closed },
            { Reference, After, false },
            { Reference, Before with { End = null, EndInclusive = false }, false },
            { Reference, Before with { Start = null, StartInclusive = false }, false },
            { Reference, Contains with { Start = null, StartInclusive = false }, false },
            { Reference, Contains with { End = null, EndInclusive = false }, false },
            { Reference, After with { Start = null, StartInclusive = false }, false },
            { Reference, After with { End = null, EndInclusive = false }, false },
            { Reference, Equal with { Start = null, End = null, StartInclusive = false, EndInclusive = false }, false },
        };
    }

    public static class IntervalDataSet
    {
        public static IntervalDataSet<T, TOffset> Open<T, TOffset>(T start, T end, TOffset offset)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new IntervalDataSet<T, TOffset>(Interval.Open(start, end), offset);
        }

        public static IntervalDataSet<T, TOffset> Closed<T, TOffset>(T start, T end, TOffset offset)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new IntervalDataSet<T, TOffset>(Interval.Closed(start, end), offset);
        }

        public static IntervalDataSet<T, TOffset> OpenClosed<T, TOffset>(T start, T end, TOffset offset)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new IntervalDataSet<T, TOffset>(Interval.OpenClosed(start, end), offset);
        }

        public static IntervalDataSet<T, TOffset> ClosedOpen<T, TOffset>(T start, T end, TOffset offset)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new IntervalDataSet<T, TOffset>(Interval.ClosedOpen(start, end), offset);
        }
    }
}
