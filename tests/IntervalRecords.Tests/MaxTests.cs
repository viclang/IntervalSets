using FluentAssertions.Execution;
using System.Linq;

namespace IntervalRecords.Tests
{
    public class MaxTests
    {
        [Theory]
        [InlineData(1, 2, 2, 3)]
        [InlineData(2, 3, 1, 2)]
        [InlineData(1, 2, 1, 2)]
        [InlineData(1, 2, 1, 3)]
        [InlineData(1, 3, 2, 3)]
        public void MaxBy_ShouldBeEnumerableMaxBy(int startA, int endA, int startB, int endB)
        {
            // Arrange
            var a = Interval.Closed(startA, endA);
            var b = Interval.Closed(startB, endB);

            // Act
            var actualMaxByStart = Interval.MaxBy(a, b, i => i.Start);
            var actualMaxByEnd = Interval.MaxBy(a, b, i => i.End);

            // Assert
            using (new AssertionScope())
            {
                actualMaxByStart.Should().Be(new[] { a, b }.MaxBy(i => i.Start));
                actualMaxByEnd.Should().Be(new[] { a, b }.MaxBy(i => i.End));
            }
        }

        [Theory]
        [InlineData(1, 2, 2, 3)]
        [InlineData(2, 3, 1, 2)]
        [InlineData(1, 2, 1, 2)]
        [InlineData(1, 2, 1, 3)]
        [InlineData(1, 3, 2, 3)]
        public void Max_ShouldBeEnumerableMax(int startA, int endA, int startB, int endB)
        {
            // Arrange
            var a = Interval.Closed(startA, endA);
            var b = Interval.Closed(startB, endB);

            // Act
            var actualMaxByStart = Interval.Max(a, b);

            // Assert
            actualMaxByStart.Should().Be(new[] { a, b }.Max());
        }
    }
}
