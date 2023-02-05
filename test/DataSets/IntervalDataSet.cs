using System;
using Xunit;

namespace IntervalRecord.Tests.DataSets
{
    public struct IntervalDataSet<T, TOffset>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public Interval<T> Reference { get; init; }
        public Interval<T> Before { get; private set; } = new Interval<T>();
        public Interval<T> Contains { get; private set; } = new Interval<T>();
        public Interval<T> After { get; private set; } = new Interval<T>();
        public Interval<T> Meets { get; private set; } = new Interval<T>();
        public Interval<T> Overlaps { get; private set; } = new Interval<T>();
        public Interval<T> Starts { get; private set; } = new Interval<T>();
        public Interval<T> ContainedBy { get; private set; } = new Interval<T>();
        public Interval<T> Finishes { get; private set; } = new Interval<T>();
        public Interval<T> Equal { get; private set; } = new Interval<T>();
        public Interval<T> FinishedBy { get; private set; } = new Interval<T>();
        public Interval<T> StartedBy { get; private set; } = new Interval<T>();
        public Interval<T> OverlappedBy { get; private set; } = new Interval<T>();
        public Interval<T> MetBy { get; private set; } = new Interval<T>();

        public IntervalDataSet(Interval<T> reference, TOffset offset)
        {
            Reference = reference;
            Init(offset);
        }

        private void Init(TOffset offset)
        {
            var dataSetCreator = GetOffsetCreator(offset);
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

        private IOffsetCreator<T, TOffset> GetOffsetCreator(TOffset offset) => (Reference, offset) switch
        {
            (Interval<int> typedReference, int typedOffset) =>
                (IOffsetCreator<T, TOffset>)(object)new IntegerCreator(typedReference, typedOffset),
            (Interval<DateOnly> typedReference, int typedOffset) =>
                (IOffsetCreator<T, TOffset>)(object)new DateOnlyCreator(typedReference, typedOffset),
            (Interval<DateTime> typedReference, TimeSpan typedOffset) =>
                (IOffsetCreator<T, TOffset>)(object)new DateTimeCreator(typedReference, typedOffset),
            (Interval<DateTimeOffset> typedReference, TimeSpan typedOffset) =>
                (IOffsetCreator<T, TOffset>)(object)new DateTimeOffsetCreator(typedReference, typedOffset),
            (_, _) => throw new NotImplementedException()
        };

        public TheoryData<Interval<T>, Interval<T>, bool> OverlapsWith => new TheoryData<Interval<T>, Interval<T>, bool>
        {
            { Reference, Before, false },
            { Reference, Meets, Reference.IntervalType == IntervalType.Closed },
            { Reference, Overlaps, true },
            { Reference, Starts, true },
            { Reference, ContainedBy, true },
            { Reference, Finishes, true },
            { Reference, Equal, true },
            { Reference, FinishedBy, true },
            { Reference, Contains, true },
            { Reference, StartedBy, true },
            { Reference, OverlappedBy, true },
            { Reference, MetBy, Reference.IntervalType == IntervalType.Closed },
            { Reference, After, false }
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
