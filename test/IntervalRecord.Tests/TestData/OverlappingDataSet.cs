using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.TestData
{
    public sealed record class IntOverlappingDataSet : BaseOverlappingDataSet<int>
    {
        /// <summary>
        /// Creates a DataSet with 13 different states <see cref="OverlappingState"/> relative to the reference interval.
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="length"></param>
        /// <param name="offset"></param>
        /// <param name="boundaryType"></param>
        /// <exception cref="ArgumentException"></exception>
        public IntOverlappingDataSet(int startingPoint, int length, int offset, BoundaryType boundaryType)
        {
            if((offset * 2) >= length) throw new ArgumentException("Length must be greater than (offset * 2)");

            var (startInclusinve, endInclusive) = boundaryType.ToTuple();
            Reference = new Interval<int>(
                startingPoint + length + offset,
                startingPoint + length + offset + length,
                startInclusinve,
                endInclusive);

            Before = Reference.GetBefore(offset);
            Meets = Reference.GetMeets();
            Overlaps = Reference.GetOverlaps(offset);
            Starts = Reference.GetStarts(offset);
            ContainedBy = Reference.GetContainedBy(offset);
            Finishes = Reference.GetFinishes(offset);
            FinishedBy = Reference.GetFinishedBy(offset);
            Contains = Reference.GetContains(offset);
            StartedBy = Reference.GetStartedBy(offset);
            OverlappedBy = Reference.GetOverlappedBy(offset);
            MetBy = Reference.GetMetBy();
            After = Reference.GetAfter(offset);
        }
    }

    public abstract record BaseOverlappingDataSet<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public Interval<T> Reference { get; protected set; }
        public Interval<T> Before { get; protected set; }
        public Interval<T> Meets { get; protected set; }
        public Interval<T> Overlaps { get; protected set; }
        public Interval<T> Starts { get; protected set; }
        public Interval<T> ContainedBy { get; protected set; }
        public Interval<T> Finishes { get; protected set; }
        public Interval<T> FinishedBy { get; protected set; }
        public Interval<T> Contains { get; protected set; }
        public Interval<T> StartedBy { get; protected set; }
        public Interval<T> OverlappedBy { get; protected set; }
        public Interval<T> MetBy { get; protected set; }
        public Interval<T> After { get; protected set; }

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
