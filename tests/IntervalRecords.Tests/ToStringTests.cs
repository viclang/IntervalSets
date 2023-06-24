namespace IntervalRecords.Tests
{
    public class ToStringTests
    {

        [Theory]
        [InlineData(null, null, IntervalType.Closed, "[-Infinity, Infinity]")]
        [InlineData(1, 2, IntervalType.Closed, "[1, 2]")]
        [InlineData(3, 4, IntervalType.Closed, "[3, 4]")]
        [InlineData(1, 2, IntervalType.ClosedOpen, "[1, 2)")]
        [InlineData(3, 4, IntervalType.ClosedOpen, "[3, 4)")]
        [InlineData(1, 2, IntervalType.OpenClosed, "(1, 2]")]
        [InlineData(3, 4, IntervalType.OpenClosed, "(3, 4]")]
        [InlineData(1, 2, IntervalType.Open, "(1, 2)")]
        [InlineData(3, 4, IntervalType.Open, "(3, 4)")]
        public void ToString_ShouldBeExpected(int? start, int? end, IntervalType intervalType, string expected)
        {
            // Arrange
            var (startInclusive, endInclusive) = intervalType.ToTuple();
            var interval = Interval<int>.Create(start, end, startInclusive, endInclusive);

            // Act
            var actual = interval.ToString();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
