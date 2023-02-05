using FluentAssertions;
using Xunit;

namespace IntervalRecord.Tests.Calculators
{
    public class InfinityStateTests
    {
        [Fact]
        public void NegativeInfinity_ShouldBeGreaterThanMin()
        {
            var minValue = new Interval<int>(int.MinValue, 0, true, true);
            var negativeInfinite = new Interval<int>(null, 0, false, true);

            negativeInfinite.Should().BeGreaterThan(minValue);
        }

        [Fact]
        public void PositiveInfinity_ShouldBeGreaterThanMax()
        {
            var maxValue = new Interval<int>(0, int.MaxValue, true, true);
            var positiveInfinite = new Interval<int>(0, null, true, false);

            positiveInfinite.Should().BeGreaterThan(maxValue);
        }

        [Fact]
        public void Infinity_ShouldBeGreaterThanMinMax()
        {
            var minMaxValue = new Interval<int>(int.MinValue, int.MaxValue, true, true);
            var positiveInfinite = new Interval<int>(null, null, false, false);

            positiveInfinite.Should().BeGreaterThan(minMaxValue);
        }

        [Fact]
        public void PositiveInfinite_ShouldBeGreaterThanNegativeInfinite()
        {
            var negativeInfinite = new Interval<int>(null, 0, false, true);
            var positiveInfinite = new Interval<int>(0, null, true, false);

            positiveInfinite.Should().BeGreaterThan(negativeInfinite);
        }

        [Fact]
        public void Infinity_ShouldBeOpen()
        {
            var infinity = new Interval<int>(null, null, true, true);

            infinity.StartInclusive.Should().BeFalse();
            infinity.EndInclusive.Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 0, InfinityState.Bounded)]
        [InlineData(0, null, InfinityState.LeftBounded)]
        [InlineData(null, 0, InfinityState.RightBounded)]
        [InlineData(null, null, InfinityState.Unbounded)]
        public void GetInfinityState_ShouldBeExpected(int? start, int? end, InfinityState expected)
        {
            // Arrange
            var interval = new Interval<int>(start, end, true, true);

            // Act
            var actual = interval.GetInfinityState();

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
