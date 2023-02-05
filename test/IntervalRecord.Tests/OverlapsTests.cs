using System;
using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests
{
    public class OverlapsTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void Overlaps(Interval<int> left, Interval<int> right, OverlappingState overlappingState)
        {
            // Arrange
            var expectedResult = overlappingState != OverlappingState.Before
                && overlappingState != OverlappingState.After;

            // Act
            var actual = left.Overlaps(right);

            // Assert
            actual.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Overlaps_IncludeHalfOpen(Interval<int> left, Interval<int> right, OverlappingState overlappingState)
        {
            // Arrange
            var expectedResult = overlappingState != OverlappingState.Before
                && overlappingState != OverlappingState.After;

            // Act
            var actual = left.Overlaps(right, true);

            // Assert
            actual.Should().Be(expectedResult);
        }
    }
}
