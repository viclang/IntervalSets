using FluentAssertions;
using IntervalRecord.Tests.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntegerOverlapsTests
    {
        //arrange
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;
        private static IntervalDataSet<int, int> _openDataSet = IntervalDataSet.Open(start, end, offset);
        private static IntervalDataSet<int, int> _closedDataSet = IntervalDataSet.Closed(start, end, offset);
        private static IntervalDataSet<int, int> _openClosedDataSet = IntervalDataSet.OpenClosed(start, end, offset);
        private static IntervalDataSet<int, int> _closedOpenDataSet = IntervalDataSet.ClosedOpen(start, end, offset);


        public static TheoryData<Interval<int>, Interval<int>, bool> OpenOverlapsWith => _openDataSet.OverlapsWith;
        public static TheoryData<Interval<int>, Interval<int>, bool> ClosedOverlapsWith => _closedDataSet.OverlapsWith;
        public static TheoryData<Interval<int>, Interval<int>, bool> OpenClosedOverlapsWith => _openClosedDataSet.OverlapsWith;
        public static TheoryData<Interval<int>, Interval<int>, bool> ClosedOpenOverlapsWith => _closedOpenDataSet.OverlapsWith;

        public static TheoryData<Interval<int>, Interval<int>, bool> OpenIsConnected => _openDataSet.IsConnected;
        public static TheoryData<Interval<int>, Interval<int>, bool> ClosedIsConnected => _closedDataSet.IsConnected;
        public static TheoryData<Interval<int>, Interval<int>, bool> OpenClosedIsConnected => _openClosedDataSet.IsConnected;
        public static TheoryData<Interval<int>, Interval<int>, bool> ClosedOpenIsConnected => _closedOpenDataSet.IsConnected;


        [Theory]
        [MemberData(nameof(OpenOverlapsWith))]
        [MemberData(nameof(ClosedOverlapsWith))]
        [MemberData(nameof(OpenClosedOverlapsWith))]
        [MemberData(nameof(ClosedOpenOverlapsWith))]
        public void OverlapsWith(Interval<int> a, Interval<int> b, bool expectedResult)
        {
            //act
            var result = a.OverlapsWith(b);

            //assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(OpenOverlapsWith))]
        [MemberData(nameof(ClosedOverlapsWith))]
        [MemberData(nameof(OpenClosedOverlapsWith))]
        [MemberData(nameof(ClosedOpenOverlapsWith))]
        public void IsConnected(Interval<int> a, Interval<int> b, bool expectedResult)
        {
            //act
            var result = a.IsConnected(b);

            //assert
            result.Should().Be(expectedResult);
        }
    }
}
