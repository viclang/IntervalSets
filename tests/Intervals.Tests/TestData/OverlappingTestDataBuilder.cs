using System.Collections.Generic;
using System.Linq;

namespace Intervals.Tests.TestData
{
    public record OverlappingTestDataBuilder(int StartingPoint, int Length, int Offset, IntervalType[] IntervalTypes)
    {
        public bool IncludeHalfOpen { get; init; } = true;
        public bool WithOverlappingState { get; init; } = true;

        public IEnumerable<object[]> Build()
        {
            var list = new List<object[]>();
            IntOverlappingDataSet dataSet;
            foreach (var intervalType in IntervalTypes)
            {
                dataSet = new IntOverlappingDataSet(StartingPoint, Length, Offset, intervalType);
                list.AddRange(GetPairsWithOverlappingState(dataSet, IncludeHalfOpen));
            }

            if (!WithOverlappingState)
            {
                return list.Select(x => x[..(x.Length - 1)]);
            }
            return list;
        }

        private static IEnumerable<object[]> GetPairsWithOverlappingState(IntOverlappingDataSet dataSet, bool includeHalfOpen)
        {
            var expectedMeets = includeHalfOpen
                ? dataSet.Reference.GetIntervalType() is IntervalType.Closed or IntervalType.OpenClosed or IntervalType.ClosedOpen
                    ? IntervalOverlapping.Meets : IntervalOverlapping.Before
                : dataSet.Reference.GetIntervalType() == IntervalType.Closed ? IntervalOverlapping.Meets : IntervalOverlapping.Before;

            var expectedMetBy = includeHalfOpen
                ? dataSet.Reference.GetIntervalType() is IntervalType.Closed or IntervalType.OpenClosed or IntervalType.ClosedOpen
                    ? IntervalOverlapping.MetBy : IntervalOverlapping.After
                : dataSet.Reference.GetIntervalType() == IntervalType.Closed ? IntervalOverlapping.MetBy : IntervalOverlapping.After;

            return new List<object[]>
            {
                new object[] { dataSet.Before, dataSet.Reference, IntervalOverlapping.Before },
                new object[] { dataSet.Meets, dataSet.Reference, expectedMeets },
                new object[] { dataSet.Overlaps, dataSet.Reference, IntervalOverlapping.Overlaps },
                new object[] { dataSet.Starts, dataSet.Reference, IntervalOverlapping.Starts },
                new object[] { dataSet.ContainedBy, dataSet.Reference, IntervalOverlapping.ContainedBy },
                new object[] { dataSet.Finishes, dataSet.Reference, IntervalOverlapping.Finishes },
                new object[] { dataSet.Reference, dataSet.Reference, IntervalOverlapping.Equal },
                new object[] { dataSet.FinishedBy, dataSet.Reference, IntervalOverlapping.FinishedBy },
                new object[] { dataSet.Contains, dataSet.Reference, IntervalOverlapping.Contains },
                new object[] { dataSet.StartedBy, dataSet.Reference, IntervalOverlapping.StartedBy },
                new object[] { dataSet.OverlappedBy, dataSet.Reference, IntervalOverlapping.OverlappedBy },
                new object[] { dataSet.MetBy, dataSet.Reference, expectedMetBy },
                new object[] { dataSet.After, dataSet.Reference, IntervalOverlapping.After },
                new object[] { dataSet.Before with { Start = null }, dataSet.Reference with { End = null }, IntervalOverlapping.Before },
                new object[] { dataSet.Meets with { Start = null }, dataSet.Reference with { End = null }, expectedMeets },
                new object[] { dataSet.After with { Start = null }, dataSet.Reference with { End = null }, IntervalOverlapping.Overlaps },
                new object[] { dataSet.Before with { Start = null }, dataSet.Reference with { Start = null }, IntervalOverlapping.Starts },
                new object[] { dataSet.Reference with { Start = null }, dataSet.Reference with { Start = null, End = null }, IntervalOverlapping.Starts },
                new object[] { dataSet.Reference, dataSet.Reference with { Start = null, End = null }, IntervalOverlapping.ContainedBy },
                new object[] { dataSet.After with { End = null }, dataSet.Reference with { End = null }, IntervalOverlapping.Finishes },
                new object[] { dataSet.Reference with { End = null }, dataSet.Reference with { Start = null, End = null }, IntervalOverlapping.Finishes },
                new object[] { dataSet.Reference with { Start = null, End = null }, dataSet.Reference  with { Start = null, End = null }, IntervalOverlapping.Equal },
                new object[] { dataSet.Before with { End = null }, dataSet.Reference with { End = null }, IntervalOverlapping.FinishedBy },
                new object[] { dataSet.Reference with { Start = null, End = null }, dataSet.Reference, IntervalOverlapping.Contains },
                new object[] { dataSet.After with { Start = null }, dataSet.Reference with { Start = null }, IntervalOverlapping.StartedBy },
                new object[] { dataSet.Before with { End = null }, dataSet.Reference with { Start = null }, IntervalOverlapping.OverlappedBy },
                new object[] { dataSet.MetBy with { End = null }, dataSet.Reference with { Start = null }, expectedMetBy },
                new object[] { dataSet.After with { End = null }, dataSet.Reference with { Start = null }, IntervalOverlapping.After },
            };
        }
    }
}
