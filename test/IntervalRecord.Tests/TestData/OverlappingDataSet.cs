using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.TestData
{
    public sealed record class IntOverlappingDataSet : BaseOverlappingDataSet<int>
    {
        private readonly int length;
        private readonly int offset;

        public IntOverlappingDataSet(int startingPoint, int length, int offset, BoundaryType boundaryType)
            : base(
                  startingPoint + length + offset,
                  startingPoint + length + offset + length,
                  boundaryType)
        {
            if((offset * 2) > length) throw new ArgumentException("Length must be greater or equal to (offset * 2)");

            this.length = length;
            this.offset = offset;
            Init();
        }

        protected override void Init()
        {
            var beforeEnd = Reference.Start.Finite!.Value - offset;
            var beforeStart = beforeEnd - length;
            var containedByStart = Reference.Start.Finite!.Value + offset;
            var containedByEnd = Reference.End.Finite!.Value - offset;
            var afterStart = Reference.End.Finite!.Value + offset;
            var afterEnd = afterStart + length;

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = Reference with { Start = containedByStart, End = containedByEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            base.Init();
        }
    }

    public abstract record BaseOverlappingDataSet<T>
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
            FinishedBy = Reference with { Start = Before.Start };
            Starts = Reference with { End = ContainedBy.End };
            Overlaps = Reference with { Start = Before.Start, End = ContainedBy.Start };
            MetBy = Reference with { Start = Reference.End, End = After.End };
        }

        public IEnumerable<object[]> GetIntervalPairsWithOverlappingState(bool includeHalfOpen)
        {
            var expectedMeets = includeHalfOpen
                ? Reference.GetBoundaryType() is BoundaryType.Closed or BoundaryType.OpenClosed or BoundaryType.ClosedOpen
                    ? OverlappingState.Meets : OverlappingState.Before
                : Reference.GetBoundaryType() == BoundaryType.Closed ? OverlappingState.Meets : OverlappingState.Before;

            var expectedMetBy = includeHalfOpen
                ? Reference.GetBoundaryType() is BoundaryType.Closed or BoundaryType.OpenClosed or BoundaryType.ClosedOpen
                    ? OverlappingState.MetBy : OverlappingState.After
                : Reference.GetBoundaryType() == BoundaryType.Closed ? OverlappingState.MetBy : OverlappingState.After;

            return new List<object[]>
            {
                new object[] { Before, Reference, OverlappingState.Before },
                new object[] { Meets, Reference, expectedMeets },
                new object[] { Overlaps, Reference, OverlappingState.Overlaps },
                new object[] { Starts, Reference, OverlappingState.Starts },
                new object[] { ContainedBy, Reference, OverlappingState.ContainedBy },
                new object[] { Finishes, Reference, OverlappingState.Finishes },
                new object[] { Reference, Reference, OverlappingState.Equal },
                new object[] { FinishedBy, Reference, OverlappingState.FinishedBy },
                new object[] { Contains, Reference, OverlappingState.Contains },
                new object[] { StartedBy, Reference, OverlappingState.StartedBy },
                new object[] { OverlappedBy, Reference, OverlappingState.OverlappedBy },
                new object[] { MetBy, Reference, expectedMetBy },
                new object[] { After, Reference, OverlappingState.After },
                new object[] { Before with { Start = null }, Reference with { End = null }, OverlappingState.Before },
                new object[] { Meets with { Start = null }, Reference with { End = null }, expectedMeets },
                new object[] { After with { Start = null }, Reference with { End = null }, OverlappingState.Overlaps },
                new object[] { Before with { Start = null }, Reference with { Start = null }, OverlappingState.Starts },
                new object[] { Reference with { Start = null }, Reference with { Start = null, End = null }, OverlappingState.Starts },
                new object[] { Reference, Reference with { Start = null, End = null }, OverlappingState.ContainedBy },
                new object[] { After with { End = null }, Reference with { End = null }, OverlappingState.Finishes },
                new object[] { Reference with { End = null }, Reference with { Start = null, End = null }, OverlappingState.Finishes },
                new object[] { Reference with { Start = null, End = null }, Reference  with { Start = null, End = null }, OverlappingState.Equal },
                new object[] { Before with { End = null }, Reference with { End = null }, OverlappingState.FinishedBy },
                new object[] { Reference with { Start = null, End = null }, Reference, OverlappingState.Contains },
                new object[] { After with { Start = null }, Reference with { Start = null }, OverlappingState.StartedBy },
                new object[] { Before with { End = null }, Reference with { Start = null }, OverlappingState.OverlappedBy },
                new object[] { MetBy with { End = null }, Reference with { Start = null }, expectedMetBy },
                new object[] { After with { End = null }, Reference with { Start = null }, OverlappingState.After },
            };
        }

        public IEnumerable<object[]> GetIntervalPairs()
        {
            return GetIntervalPairsWithOverlappingState(false).Select(x => x[..(x.Length-1)]);
        }
    }
}
