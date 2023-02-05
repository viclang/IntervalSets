using System.Linq;

namespace IntervalRecord.Tests.Calculators
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
            var a = new Interval<int>(startA, endA, true, true);
            var b = new Interval<int>(startB, endB, true, true);

            // Act
            var actualMaxByStart = Interval.MaxBy(a, b, x => x.Start);
            var actualMaxByEnd = Interval.MaxBy(a, b, x => x.End);

            // Assert
            actualMaxByStart.Should().Be(new[] { a, b }.MaxBy(x => x.Start));
            actualMaxByEnd.Should().Be(new[] { a, b }.MaxBy(x => x.End));
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
            var a = new Interval<int>(startA, endA, true, true);
            var b = new Interval<int>(startB, endB, true, true);

            // Act
            var actualMaxByStart = Interval.Max(a, b);

            // Assert
            actualMaxByStart.Should().Be(new[] { a, b }.Max());
        }
    }
}
