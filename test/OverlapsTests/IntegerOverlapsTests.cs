using FluentAssertions;
using IntervalRecord.Enums;
using IntervalRecord.Tests.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //[MemberData(nameof(OpenOverlapsWith))]
        [MemberData(nameof(ClosedOverlapsWith))]
        //[MemberData(nameof(OpenClosedOverlapsWith))]
        //[MemberData(nameof(ClosedOpenOverlapsWith))]
        public void IsConnected(Interval<int> a, Interval<int> b, bool expectedResult)
        {
            //act
            var result = a.IsConnected(b);

            //assert
            result.Should().Be(expectedResult);
        }
    }
}
