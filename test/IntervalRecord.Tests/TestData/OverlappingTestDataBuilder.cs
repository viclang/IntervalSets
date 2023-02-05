using IntervalRecord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestData
{
    public record OverlappingTestDataBuilder(int StartingPoint, int Length, int Offset, IntervalType[] BoundaryTypes)
    {
        public bool IncludeHalfOpen { get; init; } = true;
        public bool WithOverlappingState { get; init; } = true;

        public IEnumerable<object[]> Build()
        {
            var list = new List<object[]>();
            IntOverlappingDataSet dataSet;
            foreach (var intervalType in BoundaryTypes)
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
                    ? OverlappingState.Meets : OverlappingState.Before
                : dataSet.Reference.GetIntervalType() == IntervalType.Closed ? OverlappingState.Meets : OverlappingState.Before;

            var expectedMetBy = includeHalfOpen
                ? dataSet.Reference.GetIntervalType() is IntervalType.Closed or IntervalType.OpenClosed or IntervalType.ClosedOpen
                    ? OverlappingState.MetBy : OverlappingState.After
                : dataSet.Reference.GetIntervalType() == IntervalType.Closed ? OverlappingState.MetBy : OverlappingState.After;

            return new List<object[]>
            {
                new object[] { dataSet.Before, dataSet.Reference, OverlappingState.Before },
                new object[] { dataSet.Meets, dataSet.Reference, expectedMeets },
                new object[] { dataSet.Overlaps, dataSet.Reference, OverlappingState.Overlaps },
                new object[] { dataSet.Starts, dataSet.Reference, OverlappingState.Starts },
                new object[] { dataSet.ContainedBy, dataSet.Reference, OverlappingState.ContainedBy },
                new object[] { dataSet.Finishes, dataSet.Reference, OverlappingState.Finishes },
                new object[] { dataSet.Reference, dataSet.Reference, OverlappingState.Equal },
                new object[] { dataSet.FinishedBy, dataSet.Reference, OverlappingState.FinishedBy },
                new object[] { dataSet.Contains, dataSet.Reference, OverlappingState.Contains },
                new object[] { dataSet.StartedBy, dataSet.Reference, OverlappingState.StartedBy },
                new object[] { dataSet.OverlappedBy, dataSet.Reference, OverlappingState.OverlappedBy },
                new object[] { dataSet.MetBy, dataSet.Reference, expectedMetBy },
                new object[] { dataSet.After, dataSet.Reference, OverlappingState.After },
                new object[] { dataSet.Before with { Start = null }, dataSet.Reference with { End = null }, OverlappingState.Before },
                new object[] { dataSet.Meets with { Start = null }, dataSet.Reference with { End = null }, expectedMeets },
                new object[] { dataSet.After with { Start = null }, dataSet.Reference with { End = null }, OverlappingState.Overlaps },
                new object[] { dataSet.Before with { Start = null }, dataSet.Reference with { Start = null }, OverlappingState.Starts },
                new object[] { dataSet.Reference with { Start = null }, dataSet.Reference with { Start = null, End = null }, OverlappingState.Starts },
                new object[] { dataSet.Reference, dataSet.Reference with { Start = null, End = null }, OverlappingState.ContainedBy },
                new object[] { dataSet.After with { End = null }, dataSet.Reference with { End = null }, OverlappingState.Finishes },
                new object[] { dataSet.Reference with { End = null }, dataSet.Reference with { Start = null, End = null }, OverlappingState.Finishes },
                new object[] { dataSet.Reference with { Start = null, End = null }, dataSet.Reference  with { Start = null, End = null }, OverlappingState.Equal },
                new object[] { dataSet.Before with { End = null }, dataSet.Reference with { End = null }, OverlappingState.FinishedBy },
                new object[] { dataSet.Reference with { Start = null, End = null }, dataSet.Reference, OverlappingState.Contains },
                new object[] { dataSet.After with { Start = null }, dataSet.Reference with { Start = null }, OverlappingState.StartedBy },
                new object[] { dataSet.Before with { End = null }, dataSet.Reference with { Start = null }, OverlappingState.OverlappedBy },
                new object[] { dataSet.MetBy with { End = null }, dataSet.Reference with { Start = null }, expectedMetBy },
                new object[] { dataSet.After with { End = null }, dataSet.Reference with { Start = null }, OverlappingState.After },
            };
        }
    }
}
