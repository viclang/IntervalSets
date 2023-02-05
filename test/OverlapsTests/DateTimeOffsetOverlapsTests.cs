using FluentAssertions;
using IntervalRecord.Enums;
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
        private static IntervalDataSet<DateTimeOffset> _openDataSet = new IntervalDataSet<DateTimeOffset>(_start, _end, BoundaryType.Open, offset);
        private static IntervalDataSet<DateTimeOffset> _closedDataSet = _openDataSet.CopyWith(BoundaryType.Closed);
        private static IntervalDataSet<DateTimeOffset> _openClosedDataSet = _openDataSet.CopyWith(BoundaryType.OpenClosed);
        private static IntervalDataSet<DateTimeOffset> _closedOpenDataSet = _openDataSet.CopyWith(BoundaryType.ClosedOpen);


        public static TheoryData<Interval<DateTimeOffset>, Interval<DateTimeOffset>, bool> OpenIntervalOverlapsWith => _openDataSet.OverlapsWith;
        public static TheoryData<Interval<DateTimeOffset>, Interval<DateTimeOffset>, bool> ClosedIntervalOverlapsWith => _closedDataSet.OverlapsWith;
        public static TheoryData<Interval<DateTimeOffset>, Interval<DateTimeOffset>, bool> OpenClosedIntervalOverlapsWith => _openClosedDataSet.OverlapsWith;
        public static TheoryData<Interval<DateTimeOffset>, Interval<DateTimeOffset>, bool> ClosedOpenIntervalOverlapsWith => _closedOpenDataSet.OverlapsWith;

        [Theory]
        [MemberData(nameof(OpenIntervalOverlapsWith))]
        [MemberData(nameof(ClosedIntervalOverlapsWith))]
        [MemberData(nameof(OpenClosedIntervalOverlapsWith))]
        [MemberData(nameof(ClosedOpenIntervalOverlapsWith))]
        public void OverlapsWith(Interval<DateTimeOffset> a, Interval<DateTimeOffset> b, bool expectedResult)
        {
            //act
            var result = a.OverlapsWith(b);
            //assert
            result.Should().Be(expectedResult);
        }
    }
}
