using FluentAssertions;
using IntervalRecord.Tests.DataSets;
using System.Collections.Generic;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntervalComparisonTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;
        private static readonly IntervalDataSet<int> _openDataSet = new IntervalDataSet<int>(start, end, IntervalType.Open, offset);

        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> OpenGetOverlappingState => _openDataSet.GetOverlappingState;
        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> ClosedGetOverlappingState => _openDataSet.CopyWith(IntervalType.Closed).GetOverlappingState;
        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> OpenClosedGetOverlappingState => _openDataSet.CopyWith(IntervalType.OpenClosed).GetOverlappingState;
        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> ClosedOpenGetOverlappingState => _openDataSet.CopyWith(IntervalType.ClosedOpen).GetOverlappingState;

        [Theory]
        [MemberData(nameof(OpenGetOverlappingState))]
        [MemberData(nameof(ClosedGetOverlappingState))]
        [MemberData(nameof(OpenClosedGetOverlappingState))]
        [MemberData(nameof(ClosedOpenGetOverlappingState))]
        public void GetOverlappingState_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b);
            result.Should().Be(expectedResult);
        }
    }
}
