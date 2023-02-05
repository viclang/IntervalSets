using IntervalRecord.Tests.TestData;
using System;

namespace IntervalRecord.Tests.ExtensionsTests.BinaryOperations
{
    public class OverlapsTests : BaseIntervalPairSetTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;
        
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, true })]
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
