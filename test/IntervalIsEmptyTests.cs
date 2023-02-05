using FluentAssertions;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntervalIsEmptyTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        [InlineData(443)]
        [InlineData(80)]
        public void AnyEqualValueWithAnOpenBound_ShouldBeEmpty(int value)
        {
            // Arrange
            var open = new Interval<int>(value, value, false, false);
            var openClosed = new Interval<int>(value, value, false, true);
            var closedOpen = new Interval<int>(value, value, true, false);
            var closed = new Interval<int>(value, value, true, true);

            // Act
            var resultTrue = new bool[]
            {
                open.IsEmpty(),
                openClosed.IsEmpty(),
                closedOpen.IsEmpty(),
            };

            var resultFalse = closed.IsEmpty();

            // Assert
            resultTrue.Should().AllBeEquivalentTo(true);
            resultFalse.Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(-1, 0)]
        [InlineData(int.MinValue, int.MaxValue)]
        [InlineData(80, 443)]
        public void NotEqualValueWithAnOpenBound_ShouldNeverBeEmpty(int start, int end)
        {
            // Arrange
            var open = new Interval<int>(start, end, false, false);
            var openClosed = new Interval<int>(start, end, false, true);
            var closedOpen = new Interval<int>(start, end, true, false);
            var closed = new Interval<int>(start, end, true, true);

            // Act
            var result = new bool[]
            {
                open.IsEmpty(),
                openClosed.IsEmpty(),
                closedOpen.IsEmpty(),
                closed.IsEmpty()
            };

            // Assert
            result.Should().AllBeEquivalentTo(false);
        }
    }
}
