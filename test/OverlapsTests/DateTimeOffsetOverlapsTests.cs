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
    public class DateTimeOffsetOverlapsTests
    {
        //arrange
        private static readonly DateTimeOffset _start = new DateTimeOffset(new DateTime(2022, 7, 30), TimeSpan.Zero);
        private static readonly DateTimeOffset _end = _start.AddDays(4);
        private static readonly TimeSpan offset = TimeSpan.FromDays(1);
        private static IntervalDataSet<DateTimeOffset, TimeSpan> _openDataSet = IntervalDataSet.Open(_start, _end, offset);
        private static IntervalDataSet<DateTimeOffset, TimeSpan> _closedDataSet = IntervalDataSet.Closed(_start, _end, offset);
        private static IntervalDataSet<DateTimeOffset, TimeSpan> _openClosedDataSet = IntervalDataSet.OpenClosed(_start, _end, offset);
        private static IntervalDataSet<DateTimeOffset, TimeSpan> _closedOpenDataSet = IntervalDataSet.ClosedOpen(_start, _end, offset);


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
