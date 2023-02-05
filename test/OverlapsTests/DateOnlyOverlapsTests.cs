using FluentAssertions;
using IntervalRecord.Tests.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests.OverlapsTests
{
    public class DateOnlyOverlapsTests
    {
        //arrange
        private static readonly DateOnly _start = new DateOnly(2022, 7, 30);
        private static readonly DateOnly _end = _start.AddDays(4);
        private const int offset = 1;
        private static IntervalDataSet<DateOnly, int> _openDataSet = IntervalDataSet.Open(_start, _end, offset);
        private static IntervalDataSet<DateOnly, int> _closedDataSet = IntervalDataSet.Closed(_start, _end, offset);
        private static IntervalDataSet<DateOnly, int> _openClosedDataSet = IntervalDataSet.OpenClosed(_start, _end, offset);
        private static IntervalDataSet<DateOnly, int> _closedOpenDataSet = IntervalDataSet.ClosedOpen(_start, _end, offset);


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
