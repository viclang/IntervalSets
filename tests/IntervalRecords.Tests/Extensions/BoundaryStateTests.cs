using IntervalRecords.Extensions;

namespace IntervalRecords.Tests.ExtensionsTests
{
    public class BoundaryStateTests
    {
        [Theory]
        [InlineData(0, 0, BoundaryState.Bounded)]
        [InlineData(0, null, BoundaryState.LeftBounded)]
        [InlineData(null, 0, BoundaryState.RightBounded)]
        [InlineData(null, null, BoundaryState.Unbounded)]
        public void GetBoundedState_ShouldBeExpected(int? start, int? end, BoundaryState expected)
        {
            // Arrange
            var interval = IntervalFactory.Create<int>(start, end, true, true);

            // Act
            var actual = interval.GetBoundaryState();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, null, true)]
        [InlineData(null, 0, true)]
        [InlineData(0, 0, false)]
        [InlineData(null, null, false)]
        public void IsHalfBounded_ShouldBeExpected(int? start, int? end, bool expected)
        {
            // Arrange
            var interval = IntervalFactory.Create<int>(start, end, true, true);

            // Act
            var actual = interval.IsHalfBounded();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
