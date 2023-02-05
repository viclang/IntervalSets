using System.Linq;

namespace IntervalRecord.Tests
{
    public class IterateTests
    {
        [Theory]
        [InlineData(BoundaryType.Closed, 10)]
        [InlineData(BoundaryType.ClosedOpen, 9)]
        [InlineData(BoundaryType.OpenClosed, 9)]
        [InlineData(BoundaryType.Open, 8)]
        public void Iterate_ShouldHaveExpectedCount(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var interval = new Interval<int>(1, 10, startInclusive, endInclusive);

            // Act
            var actual = interval.Iterate(x => x + 1).ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(BoundaryType.Closed, 6)]
        [InlineData(BoundaryType.ClosedOpen, 5)]
        [InlineData(BoundaryType.OpenClosed, 6)]
        [InlineData(BoundaryType.Open, 5)]
        public void IterateWithStart_ShouldHaveExpectedCount(BoundaryType boundaryType, int count)
        {
            // Arrange
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var interval = new Interval<int>(1, 10, startInclusive, endInclusive);

            // Act
            var actual = interval.Iterate(5, x => x + 1).ToList();

            // Assert
            actual.Should().HaveCount(count);
        }

        [Fact]
        public void IterateEmpty_ShouldBeEmpty()
        {
            // Arrange
            var interval = Interval.Empty<int>();

            // Act
            var actual = interval.Iterate(x => x + 1).ToList();

            // Assert
            actual.Should().BeEmpty();
        }

        [Fact]
        public void IterateInfinity_ShouldBeEmpty()
        {
            // Arrange
            var interval = Interval.All<int>();

            // Act
            var actual = interval.Iterate(x => x + 1).ToList();

            // Assert
            actual.Should().BeEmpty();
        }
    }
}
