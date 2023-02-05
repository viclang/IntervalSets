using FluentAssertions;
using IntervalRecord.Tests.DataSets;
using System;
using Xunit;

namespace IntervalRecord.Tests.OverlapsTests
{
    public class DateTimeOffsetOverlapsTests
    {
        //arrange
        private static readonly DateTimeOffset _start = new DateTimeOffset(new DateTime(2022, 7, 30), TimeSpan.Zero);
        private static readonly DateTimeOffset _end = _start.AddDays(4);
        private static readonly TimeSpan offset = TimeSpan.FromDays(1);

        private static IntervalDataSet<DateTimeOffset> _dataSet = new IntervalDataSet<DateTimeOffset>(_start, _end, BoundaryType.Closed, offset);

        public static TheoryData<Interval<DateTimeOffset>, Interval<DateTimeOffset>, bool> IntervalOverlaps(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlapsWithData(false);

        public static TheoryData<Interval<DateTimeOffset>, Interval<DateTimeOffset>, bool> IntervalOverlaps_IncludeHalfOpen(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlapsWithData(true);

        [Theory]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Open)]
        public void IsConnected(Interval<DateTimeOffset> a, Interval<DateTimeOffset> b, bool expectedResult)
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
        public void IsConnected_IncludeHalfOpen(Interval<DateTimeOffset> a, Interval<DateTimeOffset> b, bool expectedResult)
        {
            //act
            var result = a.IsConnected(b, true);
            //assert
            result.Should().Be(expectedResult);
        }
    }
}
