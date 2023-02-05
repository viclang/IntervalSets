using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests
{
    public class IteratorTests
    {
        [Theory]
        [InlineData(IntervalType.Closed, 10)]
        [InlineData(IntervalType.ClosedOpen, 9)]
        [InlineData(IntervalType.OpenClosed, 9)]
        [InlineData(IntervalType.Open, 8)]
        public void Iterate_ShouldHaveExpectedCount(IntervalType boundaryType, int expectedCount)
        {
            // Arrange
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var interval = new Interval<int>(1, 10, startInclusive, endInclusive);
            var list = new List<Interval<int>>();
            // Act
            var actual = interval.Iterate(x => x + 1).ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(IntervalType.Closed, 6)]
        [InlineData(IntervalType.ClosedOpen, 5)]
        [InlineData(IntervalType.OpenClosed, 6)]
        [InlineData(IntervalType.Open, 5)]
        public void IterateWithStart_ShouldHaveExpectedCount(IntervalType boundaryType, int count)
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
