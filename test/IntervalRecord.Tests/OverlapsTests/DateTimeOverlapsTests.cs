using FluentAssertions;
using IntervalRecord.Tests.DataSets;
using System;
using Xunit;

namespace IntervalRecord.Tests.OverlapsTests
{
    public class DateTimeOverlapsTests
    {
        //arrange
        private static readonly DateTime _start = new DateTime(2022, 7, 30);
        private static readonly DateTime _end = _start.AddDays(4);
        private static readonly TimeSpan offset = TimeSpan.FromDays(1);
        private static readonly IntervalDataSet<DateTime> _openDataSet = new IntervalDataSet<DateTime>(_start, _end, BoundaryType.Open, offset);
        private static readonly IntervalDataSet<DateTime> _closedDataSet = _openDataSet.CopyWith(BoundaryType.Closed);
        private static readonly IntervalDataSet<DateTime> _openClosedDataSet = _openDataSet.CopyWith(BoundaryType.OpenClosed);
        private static readonly IntervalDataSet<DateTime> _closedOpenDataSet = _openDataSet.CopyWith(BoundaryType.ClosedOpen);

        private static readonly IntervalDataSet<DateTime> _dataSet = new IntervalDataSet<DateTime>(_start, _end, BoundaryType.Closed, offset);

        public static TheoryData<Interval<DateTime>, Interval<DateTime>, bool> IntervalOverlaps(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlapsWithData(false);

        public static TheoryData<Interval<DateTime>, Interval<DateTime>, bool> IntervalOverlaps_IncludeHalfOpen(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlapsWithData(true);

        [Theory]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Open)]
        public void OverlapsWith(Interval<DateTime> a, Interval<DateTime> b, bool expectedResult)
        {
            //act
            var result = a.OverlapsWith(b);
            //assert
            result.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.Open)]
        public void OverlapsWith_IncludeHalfOpen(Interval<DateTime> a, Interval<DateTime> b, bool expectedResult)
        {
            //act
            var result = a.IsConnected(b);
            //assert
            result.Should().Be(expectedResult);
        }
    }
}
