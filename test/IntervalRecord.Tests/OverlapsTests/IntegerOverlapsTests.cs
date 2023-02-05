using FluentAssertions;
using IntervalRecord.Tests.DataSets;
using System;
using Xunit;

namespace IntervalRecord.Tests.OverlapsTests
{
    public class IntegerOverlapsTests
    {
        //arrange
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;
        private static IntervalDataSet<int> _openDataSet = new IntervalDataSet<int>(start, end, BoundaryType.Open, offset);
        private static IntervalDataSet<int> _closedDataSet = _openDataSet.CopyWith(BoundaryType.Closed);
        private static IntervalDataSet<int> _openClosedDataSet = _openDataSet.CopyWith(BoundaryType.OpenClosed);
        private static IntervalDataSet<int> _closedOpenDataSet = _openDataSet.CopyWith(BoundaryType.ClosedOpen);
        
        private static IntervalDataSet<int> _dataSet = new IntervalDataSet<int>(start, end, BoundaryType.Closed, offset);

        public static TheoryData<Interval<int>, Interval<int>, bool> IntervalOverlaps(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetIsConnectedData(false);

        public static TheoryData<Interval<int>, Interval<int>, bool> IntervalOverlaps_IncludeHalfOpen(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetIsConnectedData(true);

        [Theory]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Open)]
        public void IsConnected(Interval<int> a, Interval<int> b, bool expectedResult)
        {
            //act
            var result = a.IsConnected(b);
            //assert
            result.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.Open)]
        public void IsConnected_IncludeHalfOpen(Interval<int> a, Interval<int> b, bool expectedResult)
        {
            //act
            var result = a.IsConnected(b, true);
            //assert
            result.Should().Be(expectedResult);
        }
    }
}
