using System;
using System.Collections.Generic;
using Xunit;

namespace IntervalRecord.Tests.DataSets
{
    public record struct IntervalDataSet<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public Interval<T> Reference { get; private set; } = default;
        public Interval<T> Before { get; private set; } = default;
        public Interval<T> After { get; private set; } = default;
        public Interval<T> ContainedBy { get; private set; } = default;
        public Interval<T> Meets { get; private set; } = default;
        public Interval<T> OverlappedBy { get; private set; } = default;
        public Interval<T> StartedBy { get; private set; } = default;
        public Interval<T> Contains { get; private set; } = default;
        public Interval<T> Finishes { get; private set; } = default;
        public Interval<T> Equal { get; private set; } = default;
        public Interval<T> FinishedBy { get; private set; } = default;
        public Interval<T> Starts { get; private set; } = default;
        public Interval<T> Overlaps { get; private set; } = default;
        public Interval<T> MetBy { get; private set; } = default;

        /// <summary>
        /// Creates a DataSet with 13 different states <see cref="OverlappingState"/> relative to the reference interval.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="boundaryType"></param>
        /// <param name="offset"></param>
        public IntervalDataSet(T start, T end, BoundaryType boundaryType, int offset)
        {
            var (startInclusinve, endInclusive) = boundaryType.ToTuple();
            Reference = new Interval<T>(start, end, startInclusinve, endInclusive);
            var offsetCreator = OffsetCreator.Create(Reference, offset);
            Init(offsetCreator);
        }

        public IntervalDataSet(T start, T end, BoundaryType boundaryType, TimeSpan offset)
        {
            var (startInclusinve, endInclusive) = boundaryType.ToTuple();
            Reference = new Interval<T>(start, end, startInclusinve, endInclusive);
            var offsetCreator = OffsetCreator.Create(Reference, offset);
            Init(offsetCreator);
        }

        private void Init(IOffsetCreator<T> offsetCreator)
        {
            After = offsetCreator.After;
            ContainedBy = offsetCreator.ContainedBy;
            Before = offsetCreator.Before;
            Meets = Reference with { Start = Before.Start, End = Reference.Start };
            OverlappedBy = Reference with { Start = ContainedBy.End, End = After.End };
            StartedBy = Reference with { End = After.Start };
            Contains = Reference with { Start = Before.Start, End = After.End };
            Finishes = Reference with { Start = ContainedBy.Start };
            Equal = Reference with { };
            FinishedBy = Reference with { Start = Before.Start };
            Starts = Reference with { End = ContainedBy.End };
            Overlaps = Reference with { Start = Before.Start, End = ContainedBy.Start };
            MetBy = Reference with { Start = Reference.End, End = After.End };
        }

        public IEnumerable<Interval<T>> ToList()
        {
            return new List<Interval<T>>
            {
                Before,
                Meets,
                Overlaps,
                Starts,
                ContainedBy,
                Finishes,
                Equal,
                FinishedBy,
                Contains,
                StartedBy,
                OverlappedBy,
                MetBy,
                After
            };
        }

        public IntervalDataSet<T> CopyWith(BoundaryType boundaryType)
        {
            var (startInclusive, endInclusive) = Interval.ToTuple(boundaryType);
            return this with
            {
                Reference = Reference with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                Before = Before with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                Meets = Meets with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                OverlappedBy = OverlappedBy with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                StartedBy = StartedBy with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                Contains = Contains with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                Finishes = Finishes with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                Equal = Equal with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                FinishedBy = FinishedBy with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                ContainedBy = ContainedBy with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                Starts = Starts with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                Overlaps = Overlaps with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                MetBy = MetBy with { StartInclusive = startInclusive, EndInclusive = endInclusive },
                After = After with { StartInclusive = startInclusive, EndInclusive = endInclusive },
            };
        }

        public TheoryData<Interval<T>, Interval<T>, OverlappingState> GetOverlappingState => new TheoryData<Interval<T>, Interval<T>, OverlappingState>
        {
            { Before, Reference, OverlappingState.Before },
            { Meets, Reference, Reference.GetBoundaryType() == BoundaryType.Closed ? OverlappingState.Meets : OverlappingState.Before },
            { Overlaps, Reference, OverlappingState.Overlaps },
            { Starts, Reference, OverlappingState.Starts },
            { ContainedBy, Reference, OverlappingState.ContainedBy },
            { Finishes, Reference, OverlappingState.Finishes },
            { Equal, Reference, OverlappingState.Equal },
            { FinishedBy, Reference, OverlappingState.FinishedBy },
            { Contains, Reference, OverlappingState.Contains },
            { StartedBy, Reference, OverlappingState.StartedBy },
            { OverlappedBy, Reference, OverlappingState.OverlappedBy },
            { MetBy, Reference, Reference.GetBoundaryType() == BoundaryType.Closed ? OverlappingState.MetBy : OverlappingState.After },
            { After, Reference, OverlappingState.After },
            { Before with { Start = null }, Reference with { End = null }, OverlappingState.Before },
            { Meets with { Start = null }, Reference with { End = null }, Reference.GetBoundaryType() == BoundaryType.Closed ? OverlappingState.Meets : OverlappingState.Before },
            { After with { Start = null }, Reference with { End = null }, OverlappingState.Overlaps },
            { Before with { Start = null }, Reference with { Start = null }, OverlappingState.Starts },
            { Equal with { Start = null }, Reference with { Start = null, End = null }, OverlappingState.Starts },
            { Equal, Reference with { Start = null, End = null }, OverlappingState.ContainedBy },
            { After with { End = null }, Reference with { End = null }, OverlappingState.Finishes },
            { Equal with { End = null }, Reference with { Start = null, End = null }, OverlappingState.Finishes },
            { Equal with { Start = null, End = null }, Reference  with { Start = null, End = null }, OverlappingState.Equal },
            { Before with { End = null }, Reference with { End = null }, OverlappingState.FinishedBy },
            { Equal with { Start = null, End = null }, Reference, OverlappingState.Contains },
            { After with { Start = null }, Reference with { Start = null }, OverlappingState.StartedBy },
            { Before with { End = null }, Reference with { Start = null }, OverlappingState.OverlappedBy },
            { MetBy with { End = null }, Reference with { Start = null }, Reference.GetBoundaryType() == BoundaryType.Closed ? OverlappingState.MetBy : OverlappingState.After },
            { After with { End = null }, Reference with { Start = null }, OverlappingState.After },
        };

        public TheoryData<Interval<T>, Interval<T>, bool> OverlapsWith => new TheoryData<Interval<T>, Interval<T>, bool>
        {
            { Before, Reference, false },
            { Meets, Reference, Reference.GetBoundaryType() == BoundaryType.Closed },
            { Overlaps, Reference, true },
            { Starts, Reference, true },
            { ContainedBy, Reference, true },
            { Finishes, Reference, true },
            { Equal, Reference, true },
            { FinishedBy, Reference, true },
            { Contains, Reference, true },
            { StartedBy, Reference, true },
            { OverlappedBy, Reference, true },
            { MetBy, Reference, Reference.GetBoundaryType() == BoundaryType.Closed },
            { After, Reference, false },
            { Before with { Start = null }, Reference with { End = null }, false },
            { Meets with { Start = null }, Reference with { End = null }, Reference.GetBoundaryType() == BoundaryType.Closed },
            { After with { Start = null }, Reference with { End = null }, true },
            { Before with { Start = null }, Reference with { Start = null }, true },
            { Equal with { Start = null }, Reference with { Start = null, End = null }, true },
            { Equal, Reference with { Start = null, End = null }, true },
            { After with { End = null }, Reference with { End = null }, true },
            { Equal with { End = null }, Reference with { Start = null, End = null }, true },
            { Equal with { Start = null, End = null }, Reference  with { Start = null, End = null }, true },
            { Before with { End = null }, Reference with { End = null }, true },
            { Equal with { Start = null, End = null }, Reference, true },
            { After with { Start = null }, Reference with { Start = null }, true },
            { Before with { End = null }, Reference with { Start = null }, true },
            { MetBy with { End = null }, Reference with { Start = null }, Reference.GetBoundaryType() == BoundaryType.Closed },
            { After with { End = null }, Reference with { Start = null }, false },
        };
    }
}
