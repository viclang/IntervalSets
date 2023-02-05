using System.Linq;

namespace IntervalRecord.Tests.ExtensionsTests.BinaryOperations
{
    public class MinByMaxByTests
    {
        [Theory]
        [InlineData(1, 10, 10, 1)]
        [InlineData(10, 1, 1, 10)]
        [InlineData(5, 6, 7, 8)]
        [InlineData(8, 7, 6, 5)]
        public void MinBy(int startA, int endA, int startB, int endB)
        {
            // Arrange
            var a = new Interval<int>(startA, endA, true, true);
            var b = new Interval<int>(startB, endB, true, true);

            // Act
            var actualMinByStart = Interval.MinBy(a, b, x => x.Start);
            var actualMinByEnd = Interval.MinBy(a, b, x => x.End);

            // Assert
            actualMinByStart.Should().Be(new[] { a, b }.MinBy(x => x.Start));
            actualMinByEnd.Should().Be(new[] { a, b }.MinBy(x => x.End));
        }

        [Theory]
        [InlineData(1, 10, 10, 1)]
        [InlineData(10, 1, 1, 10)]
        [InlineData(5, 6, 7, 8)]
        [InlineData(8, 7, 6, 5)]
        public void MaxBy(int startA, int endA, int startB, int endB)
        {
            // Arrange
            var a = new Interval<int>(startA, endA, true, true);
            var b = new Interval<int>(startB, endB, true, true);

            // Act
            var actualMinByStart = Interval.MaxBy(a, b, x => x.Start);
            var actualMinByEnd = Interval.MaxBy(a, b, x => x.End);

            // Assert
            actualMinByStart.Should().Be(new[] { a, b }.MaxBy(x => x.Start));
            actualMinByEnd.Should().Be(new[] { a, b }.MaxBy(x => x.End));
        }
    }
}
