using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecords.Tests.TestData
{
    public sealed record class IntOverlappingDataSet : BaseOverlappingDataSet<int>
    {
        /// <summary>
        /// Creates a DataSet with 13 different states <see cref="IntervalOverlapping"/> relative to the reference interval.
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="length"></param>
        /// <param name="offset"></param>
        /// <param name="intervalType"></param>
        /// <exception cref="ArgumentException"></exception>
        public IntOverlappingDataSet(int startingPoint, int length, int offset, IntervalType intervalType)
        {
            if ((offset * 2) >= length) throw new ArgumentException("Length must be greater than (offset * 2)");

            var (startInclusinve, endInclusive) = intervalType.ToTuple();
            Reference = Interval.CreateInterval<int>(
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
                ? Reference.GetIntervalType() is IntervalType.Closed or IntervalType.OpenClosed or IntervalType.ClosedOpen
                    ? IntervalOverlapping.Meets : IntervalOverlapping.Before
                : Reference.GetIntervalType() == IntervalType.Closed ? IntervalOverlapping.Meets : IntervalOverlapping.Before;

            var expectedMetBy = includeHalfOpen
                ? Reference.GetIntervalType() is IntervalType.Closed or IntervalType.OpenClosed or IntervalType.ClosedOpen
                    ? IntervalOverlapping.MetBy : IntervalOverlapping.After
                : Reference.GetIntervalType() == IntervalType.Closed ? IntervalOverlapping.MetBy : IntervalOverlapping.After;

            return new List<object[]>
            {
                new object[] { Before, Reference, IntervalOverlapping.Before },
                new object[] { Meets, Reference, expectedMeets },
                new object[] { Overlaps, Reference, IntervalOverlapping.Overlaps },
                new object[] { Starts, Reference, IntervalOverlapping.Starts },
                new object[] { ContainedBy, Reference, IntervalOverlapping.ContainedBy },
                new object[] { Finishes, Reference, IntervalOverlapping.Finishes },
                new object[] { Reference, Reference, IntervalOverlapping.Equal },
                new object[] { FinishedBy, Reference, IntervalOverlapping.FinishedBy },
                new object[] { Contains, Reference, IntervalOverlapping.Contains },
                new object[] { StartedBy, Reference, IntervalOverlapping.StartedBy },
                new object[] { OverlappedBy, Reference, IntervalOverlapping.OverlappedBy },
                new object[] { MetBy, Reference, expectedMetBy },
                new object[] { After, Reference, IntervalOverlapping.After },
                new object[] { Before with { Start = null }, Reference with { End = null }, IntervalOverlapping.Before },
                new object[] { Meets with { Start = null }, Reference with { End = null }, expectedMeets },
                new object[] { After with { Start = null }, Reference with { End = null }, IntervalOverlapping.Overlaps },
                new object[] { Before with { Start = null }, Reference with { Start = null }, IntervalOverlapping.Starts },
                new object[] { Reference with { Start = null }, Reference with { Start = null, End = null }, IntervalOverlapping.Starts },
                new object[] { Reference, Reference with { Start = null, End = null }, IntervalOverlapping.ContainedBy },
                new object[] { After with { End = null }, Reference with { End = null }, IntervalOverlapping.Finishes },
                new object[] { Reference with { End = null }, Reference with { Start = null, End = null }, IntervalOverlapping.Finishes },
                new object[] { Reference with { Start = null, End = null }, Reference  with { Start = null, End = null }, IntervalOverlapping.Equal },
                new object[] { Before with { End = null }, Reference with { End = null }, IntervalOverlapping.FinishedBy },
                new object[] { Reference with { Start = null, End = null }, Reference, IntervalOverlapping.Contains },
                new object[] { After with { Start = null }, Reference with { Start = null }, IntervalOverlapping.StartedBy },
                new object[] { Before with { End = null }, Reference with { Start = null }, IntervalOverlapping.OverlappedBy },
                new object[] { MetBy with { End = null }, Reference with { Start = null }, expectedMetBy },
                new object[] { After with { End = null }, Reference with { Start = null }, IntervalOverlapping.After },
            };
        }

        public IEnumerable<object[]> GetIntervalPairs()
        {
            return GetIntervalPairsWithOverlappingState(false).Select(x => x[..(x.Length - 1)]);
        }
    }
}
