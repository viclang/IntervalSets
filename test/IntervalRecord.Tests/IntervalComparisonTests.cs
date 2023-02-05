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
        private static readonly IntervalDataSet<int> _dataSet = new IntervalDataSet<int>(start, end, BoundaryType.Closed, offset);

        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> GetOverlappingState(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlappingState(false);
        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> GetOverlappingState_HalfOpen(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlappingState(true);

        [Theory]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Closed)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.ClosedOpen)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.OpenClosed)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Open)]
        public void GetOverlappingState_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b, false);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.Open)]
        public void GetOverlappingStateIncludeHalfOpen_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b, true);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.Open)]
        public void IsBefore_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            a.IsBefore(b).Should().Be(state == OverlappingState.Before);
            a.Meets(b).Should().Be(state == OverlappingState.Meets);
            a.Starts(b).Should().Be(state == OverlappingState.Starts);
            a.ContainedBy(b).Should().Be(state == OverlappingState.ContainedBy);
            a.Finishes(b).Should().Be(state == OverlappingState.Finishes);
            a.Equals(b).Should().Be(state == OverlappingState.Equal);
            a.FinishedBy(b).Should().Be(state == OverlappingState.FinishedBy);
            a.Contains(b).Should().Be(state == OverlappingState.Contains);
            a.StartedBy(b).Should().Be(state == OverlappingState.StartedBy);
            a.MetBy(b).Should().Be(state == OverlappingState.MetBy);
            a.IsAfter(b).Should().Be(state == OverlappingState.After);
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(GetOverlappingState_HalfOpen), BoundaryType.Open)]
        public void IsBefore_HalfOpen_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            a.IsBefore(b, true).Should().Be(state == OverlappingState.Before);
            a.Meets(b, true).Should().Be(state == OverlappingState.Meets);
            a.Starts(b).Should().Be(state == OverlappingState.Starts);
            a.ContainedBy(b).Should().Be(state == OverlappingState.ContainedBy);
            a.Finishes(b).Should().Be(state == OverlappingState.Finishes);
            a.Equals(b).Should().Be(state == OverlappingState.Equal);
            a.FinishedBy(b).Should().Be(state == OverlappingState.FinishedBy);
            a.Contains(b).Should().Be(state == OverlappingState.Contains);
            a.StartedBy(b).Should().Be(state == OverlappingState.StartedBy);
            a.MetBy(b, true).Should().Be(state == OverlappingState.MetBy);
            a.IsAfter(b, true).Should().Be(state == OverlappingState.After);
        }
    }
}
