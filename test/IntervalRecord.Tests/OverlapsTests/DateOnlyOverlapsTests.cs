using FluentAssertions;
using IntervalRecord.Tests.DataSets;
using System;
using Xunit;

namespace IntervalRecord.Tests.OverlapsTests
{
    public class DateOnlyOverlapsTests
    {
        //arrange
        private static readonly DateOnly _start = new DateOnly(2022, 7, 30);
        private static readonly DateOnly _end = _start.AddDays(4);
        private const int offset = 1;
        private static IntervalDataSet<DateOnly> _openDataSet = new IntervalDataSet<DateOnly>(_start, _end, BoundaryType.Open, offset);
        private static IntervalDataSet<DateOnly> _closedDataSet = _openDataSet.CopyWith(BoundaryType.Closed);
        private static IntervalDataSet<DateOnly> _openClosedDataSet = _openDataSet.CopyWith(BoundaryType.OpenClosed);
        private static IntervalDataSet<DateOnly> _closedOpenDataSet = _openDataSet.CopyWith(BoundaryType.ClosedOpen);


        public static TheoryData<Interval<DateOnly>, Interval<DateOnly>, bool> OpenIntervalOverlapsWith => _openDataSet.OverlapsWith;
        public static TheoryData<Interval<DateOnly>, Interval<DateOnly>, bool> ClosedIntervalOverlapsWith => _closedDataSet.OverlapsWith;
        public static TheoryData<Interval<DateOnly>, Interval<DateOnly>, bool> OpenClosedIntervalOverlapsWith => _openClosedDataSet.OverlapsWith;
        public static TheoryData<Interval<DateOnly>, Interval<DateOnly>, bool> ClosedOpenIntervalOverlapsWith => _closedOpenDataSet.OverlapsWith;

        [Theory]
        [MemberData(nameof(OpenIntervalOverlapsWith))]
        [MemberData(nameof(ClosedIntervalOverlapsWith))]
        [MemberData(nameof(OpenClosedIntervalOverlapsWith))]
        [MemberData(nameof(ClosedOpenIntervalOverlapsWith))]
        public void OverlapsWith(Interval<DateOnly> a, Interval<DateOnly> b, bool expectedResult)
        {
            //act
            var result = a.OverlapsWith(b);

            //assert
            result.Should().Be(expectedResult);
        }
    }
}
