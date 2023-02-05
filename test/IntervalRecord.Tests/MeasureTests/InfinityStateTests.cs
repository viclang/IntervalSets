using FluentAssertions;
using InfinityComparable;
using Xunit;

namespace IntervalRecord.Tests.Calculators
{
    public class InfinityStateTests
    {
        [Fact]
        public void Infinity_ShouldBeOpen()
        {
            var infinity = new Interval<int>(null, null, true, true);

            infinity.StartInclusive.Should().BeFalse();
            infinity.EndInclusive.Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 0, BoundedState.Bounded)]
        [InlineData(0, null, BoundedState.LeftBounded)]
        [InlineData(null, 0, BoundedState.RightBounded)]
        [InlineData(null, null, BoundedState.Unbounded)]
        public void GetInfinityState_ShouldBeExpected(int? start, int? end, BoundedState expected)
        {
            // Arrange
            var interval = new Interval<int>(start, end, true, true);

            // Act
            var actual = interval.GetBoundedState();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, null, true)]
        [InlineData(null, 0, true)]
        [InlineData(null, null, false)]
        public void IsHalfBounded_ShouldBeExpected(int? start, int? end, bool expected)
        {
            // Arrange
            var interval = new Interval<int>(start, end, true, true);

            // Act
            var actual = interval.IsHalfBounded();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
