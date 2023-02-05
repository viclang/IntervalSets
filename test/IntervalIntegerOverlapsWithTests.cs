using FluentAssertions;
using IntervalRecord.Tests.TestDataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntervalIntegerOverlapsWithTests
    {
        //arrange
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;

        private static IntIntervalTestDataSet _openDataSet = new IntIntervalTestDataSet(Interval.Open(start, end), offset);
        private static IntIntervalTestDataSet _closedDataSet = new IntIntervalTestDataSet(Interval.Closed(start, end), offset);
        private static IntIntervalTestDataSet _openClosedDataSet = new IntIntervalTestDataSet(Interval.OpenClosed(start, end), offset);
        private static IntIntervalTestDataSet _closedOpenDataSet = new IntIntervalTestDataSet(Interval.ClosedOpen(start, end), offset);


        public static TheoryData<Interval<int>, Interval<int>, bool> OpenIntervalOverlapsWith => _openDataSet.OverlapsWith;
        public static TheoryData<Interval<int>, Interval<int>, bool> ClosedIntervalOverlapsWith => _closedDataSet.OverlapsWith;
        public static TheoryData<Interval<int>, Interval<int>, bool> OpenClosedIntervalOverlapsWith => _openClosedDataSet.OverlapsWith;
        public static TheoryData<Interval<int>, Interval<int>, bool> ClosedOpenIntervalOverlapsWith => _closedOpenDataSet.OverlapsWith;

        [Theory]
        [MemberData(nameof(OpenIntervalOverlapsWith))]
        [MemberData(nameof(ClosedIntervalOverlapsWith))]
        [MemberData(nameof(OpenClosedIntervalOverlapsWith))]
        [MemberData(nameof(ClosedOpenIntervalOverlapsWith))]
        public void OverlapsWith(Interval<int> a, Interval<int> b, bool expectedResult)
        {
            //act
            var result = a.OverlapsWith(b);

            //assert
            result.Should().Be(expectedResult);
        }
    }
}
