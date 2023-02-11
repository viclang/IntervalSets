using Intervals.Tests.TestData;

namespace Intervals.Tests
{
    public class OverlapsTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void Overlaps(Interval<int> left, Interval<int> right, IntervalOverlapping overlappingState)
        {
            // Arrange
            var expectedResult = overlappingState != IntervalOverlapping.Before
                && overlappingState != IntervalOverlapping.After;

            // Act
            var actual = left.Overlaps(right);

            // Assert
            actual.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Overlaps_IncludeHalfOpen(Interval<int> left, Interval<int> right, IntervalOverlapping overlappingState)
        {
            // Arrange
            var expectedResult = overlappingState != IntervalOverlapping.Before
                && overlappingState != IntervalOverlapping.After;

            // Act
            var actual = left.Overlaps(right, true);

            // Assert
            actual.Should().Be(expectedResult);
        }
    }
}
