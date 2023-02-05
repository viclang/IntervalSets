namespace IntervalRecord.Tests
{
    public class ToStringTests
    {

        [Theory]
        [InlineData(null, null, BoundaryType.Closed, "(-Infinity, Infinity)")]
        [InlineData(1, 2, BoundaryType.Closed, "[1, 2]")]
        [InlineData(3, 4, BoundaryType.Closed, "[3, 4]")]
        [InlineData(1, 2, BoundaryType.ClosedOpen, "[1, 2)")]
        [InlineData(3, 4, BoundaryType.ClosedOpen, "[3, 4)")]
        [InlineData(1, 2, BoundaryType.OpenClosed, "(1, 2]")]
        [InlineData(3, 4, BoundaryType.OpenClosed, "(3, 4]")]
        [InlineData(1, 2, BoundaryType.Open, "(1, 2)")]
        [InlineData(3, 4, BoundaryType.Open, "(3, 4)")]
        public void ToString_ShouldBeExpected(int? start, int? end, BoundaryType boundaryType, string expected)
        {
            // Arrange
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var interval = new Interval<int>(start, end, startInclusive, endInclusive);

            // Act
            var actual = interval.ToString();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
