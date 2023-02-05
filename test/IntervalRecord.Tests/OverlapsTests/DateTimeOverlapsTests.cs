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
        private static IntervalDataSet<DateTime> _openDataSet = new IntervalDataSet<DateTime>(_start, _end, BoundaryType.Open, offset);
        private static IntervalDataSet<DateTime> _closedDataSet = _openDataSet.CopyWith(BoundaryType.Closed);
        private static IntervalDataSet<DateTime> _openClosedDataSet = _openDataSet.CopyWith(BoundaryType.OpenClosed);
        private static IntervalDataSet<DateTime> _closedOpenDataSet = _openDataSet.CopyWith(BoundaryType.ClosedOpen);


        public static TheoryData<Interval<DateTime>, Interval<DateTime>, bool> OpenIntervalOverlapsWith => _openDataSet.OverlapsWith;
        public static TheoryData<Interval<DateTime>, Interval<DateTime>, bool> ClosedIntervalOverlapsWith => _closedDataSet.OverlapsWith;
        public static TheoryData<Interval<DateTime>, Interval<DateTime>, bool> OpenClosedIntervalOverlapsWith => _openClosedDataSet.OverlapsWith;
        public static TheoryData<Interval<DateTime>, Interval<DateTime>, bool> ClosedOpenIntervalOverlapsWith => _closedOpenDataSet.OverlapsWith;

        [Theory]
        [MemberData(nameof(OpenIntervalOverlapsWith))]
        [MemberData(nameof(ClosedIntervalOverlapsWith))]
        [MemberData(nameof(OpenClosedIntervalOverlapsWith))]
        [MemberData(nameof(ClosedOpenIntervalOverlapsWith))]
        public void OverlapsWith(Interval<DateTime> a, Interval<DateTime> b, bool expectedResult)
        {
            //act
            var result = a.OverlapsWith(b);

            //assert
            result.Should().Be(expectedResult);
        }
    }
}
