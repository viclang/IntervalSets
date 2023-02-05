using System;
using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests
{
    public class OverlapsTests : OverlappingStateTestsBase
    {
        private const int startingPoint = 0;
        private const int length = 4;
        private const int offset = 1;

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, false })]
        public void Overlaps(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Arrange
            var expectedResult = overlappingState != OverlappingState.Before
                && overlappingState != OverlappingState.After;

            // Act
            var actual = a.Overlaps(b);

            // Assert
            actual.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, true })]
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
