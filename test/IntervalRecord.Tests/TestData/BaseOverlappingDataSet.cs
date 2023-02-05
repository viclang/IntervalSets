using IntervalRecord.Tests.TestData;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace IntervalRecord.Tests.DataSets
{
    public sealed record class IntOverlappingDataSet : BaseOverlappingDataSet<int>
    {
        private readonly int _offset;

        public IntOverlappingDataSet(int start, int end, BoundaryType boundaryType, int offset)
            : base(start, end, boundaryType)
        {
            _offset = offset;
            Init();
        }

        protected override void Init()
        {
            var length = Reference.Length().Finite!.Value;
            var beforeEnd = Reference.Start.Finite!.Value - _offset;
            var beforeStart = beforeEnd - length;
            var containsStart = Reference.Start.Finite!.Value + _offset;
            var containsEnd = Reference.End.Finite!.Value - _offset;
            var afterStart = Reference.End.Finite!.Value + _offset;
            var afterEnd = afterStart + length;

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            base.Init();
        }
    }

    public abstract record BaseOverlappingDataSet<T> : IOverlappingDataSet<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public Interval<T> Reference { get; init; }
        public Interval<T> Before { get; protected set; }
        public Interval<T> After { get; protected set; }
        public Interval<T> ContainedBy { get; protected set; }
        public Interval<T> Meets { get; private set; }
        public Interval<T> OverlappedBy { get; private set; }
        public Interval<T> StartedBy { get; private set; }
        public Interval<T> Contains { get; private set; }
        public Interval<T> Finishes { get; private set; }
        public Interval<T> Equal { get; private set; }
        public Interval<T> FinishedBy { get; private set; }
        public Interval<T> Starts { get; private set; }
        public Interval<T> Overlaps { get; private set; }
        public Interval<T> MetBy { get; private set; }

        /// <summary>
        /// Creates a DataSet with 13 different states <see cref="OverlappingState"/> relative to the reference interval.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="boundaryType"></param>
        /// <param name="offset"></param>
        public BaseOverlappingDataSet(T start, T end, BoundaryType boundaryType)
        {
            var (startInclusinve, endInclusive) = boundaryType.ToTuple();
            Reference = new Interval<T>(start, end, startInclusinve, endInclusive);
        }

        protected virtual void Init()
        {
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
        public List<Interval<T>> ToList() => new List<Interval<T>>
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
            After,
        };

        public TheoryData<Interval<T>> ToTheoryData() => new TheoryData<Interval<T>>
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
                After,
                Equal with { Start = null },
                Before with { Start = null },
                After with { Start = null },
                ContainedBy with { Start = null },
                Equal with { End = null },
                Before with { End = null },
                After with { End = null },
                ContainedBy with { End = null },
                Equal with { Start = null, End = null },
                Before with { Start = null, End = null },
                After with { Start = null, End = null },
                ContainedBy with { Start = null, End = null },
            };

        public IOverlappingDataSet<T> CopyWith(BoundaryType boundaryType)
        {
            if(Reference.GetBoundaryType() == boundaryType)
            {
                return this with { };
            }
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
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

        public TheoryData<Interval<T>, Interval<T>, OverlappingState> GetOverlappingState(bool includeHalfOpen)
        {
            var expectedMeets = includeHalfOpen
                ? Reference.GetBoundaryType() is BoundaryType.Closed or BoundaryType.OpenClosed or BoundaryType.ClosedOpen
                    ? OverlappingState.Meets : OverlappingState.Before
                : Reference.GetBoundaryType() == BoundaryType.Closed ? OverlappingState.Meets : OverlappingState.Before;
            
            var expectedMetBy = includeHalfOpen
                ? Reference.GetBoundaryType() is BoundaryType.Closed or BoundaryType.OpenClosed or BoundaryType.ClosedOpen
                    ? OverlappingState.MetBy : OverlappingState.After
                : Reference.GetBoundaryType() == BoundaryType.Closed ? OverlappingState.MetBy : OverlappingState.After;

            return new TheoryData<Interval<T>, Interval<T>, OverlappingState>
            {
                { Before, Reference, OverlappingState.Before },
                { Meets, Reference, expectedMeets },
                { Overlaps, Reference, OverlappingState.Overlaps },
                { Starts, Reference, OverlappingState.Starts },
                { ContainedBy, Reference, OverlappingState.ContainedBy },
                { Finishes, Reference, OverlappingState.Finishes },
                { Equal, Reference, OverlappingState.Equal },
                { FinishedBy, Reference, OverlappingState.FinishedBy },
                { Contains, Reference, OverlappingState.Contains },
                { StartedBy, Reference, OverlappingState.StartedBy },
                { OverlappedBy, Reference, OverlappingState.OverlappedBy },
                { MetBy, Reference, expectedMetBy },
                { After, Reference, OverlappingState.After },
                { Before with { Start = null }, Reference with { End = null }, OverlappingState.Before },
                { Meets with { Start = null }, Reference with { End = null }, expectedMeets },
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
                { MetBy with { End = null }, Reference with { Start = null }, expectedMetBy },
                { After with { End = null }, Reference with { Start = null }, OverlappingState.After },
            };
        }
    }
}
