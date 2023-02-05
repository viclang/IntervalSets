using IntervalRecord.Tests.TestData;
using System;

namespace IntervalRecord.Tests
{
    public class IntervalOverlapsTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;
        private static IOverlappingDataSet<int> _dataSet = new IntegerOverlappingDataSet(start, end, BoundaryType.Closed, offset);

        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> IntervalOverlaps(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlappingState(false);

        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> IntervalOverlaps_IncludeHalfOpen(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlappingState(true);

        [Theory]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Open)]
        public void Overlaps(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            var expectedResult = overlappingState != OverlappingState.Before
                && overlappingState != OverlappingState.After;

            //act
            var actual = a.Overlaps(b);

            //assert
            actual.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps_IncludeHalfOpen), BoundaryType.Open)]
        public void Overlaps_IncludeHalfOpen(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Arrange
            var expectedResult = overlappingState != OverlappingState.Before
                && overlappingState != OverlappingState.After;


            // Act
            var actual = a.Overlaps(b, true);

            // Assert
            actual.Should().Be(expectedResult);
        }
    }
}
