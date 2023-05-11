namespace IntervalRecords.Tests
{
    public class BoundaryStateTests
    {
        [Fact]
        public void Infinity_ShouldBeOpen()
        {
            var infinity = new Interval<int>(null, null, true, true);

            infinity.StartInclusive.Should().BeFalse();
            infinity.EndInclusive.Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 0, BoundaryState.Bounded)]
        [InlineData(0, null, BoundaryState.LeftBounded)]
        [InlineData(null, 0, BoundaryState.RightBounded)]
        [InlineData(null, null, BoundaryState.Unbounded)]
        public void GetBoundedState_ShouldBeExpected(int? start, int? end, BoundaryState expected)
        {
            // Arrange
            var interval = new Interval<int>(start, end, true, true);

            // Act
            var actual = interval.GetBoundaryState();

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
