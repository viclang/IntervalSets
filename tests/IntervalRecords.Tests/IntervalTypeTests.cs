namespace IntervalRecords.Tests
{
    public class IntervalTypeTests
    {
        private const int start = 6;
        private const int end = 10;

        [Theory]
        [InlineData(true, true, IntervalType.Closed)]
        [InlineData(true, false, IntervalType.ClosedOpen)]
        [InlineData(false, true, IntervalType.OpenClosed)]
        [InlineData(false, false, IntervalType.Open)]
        public void GetIntervalType_ToTuple_ShouldBeExpected(bool startInclusive, bool endInclusive, IntervalType expected)
        {
            // Arrange
            var interval = Interval.Create<int>(start, end, startInclusive, endInclusive);

            // Act
            var actualIntervalType = interval.IntervalType;
            var (actualStartInclusive, actualEndInclusive) = actualIntervalType.ToTuple();

            // Assert
            actualIntervalType.Should().Be(expected);
            actualStartInclusive.Should().Be(interval.StartInclusive);
            actualEndInclusive.Should().Be(interval.EndInclusive);
        }

        [Theory]
        [InlineData(true, true, false)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(false, false, false)]
        public void IsHalfOpen_ShouldBeExpected(bool startInclusive, bool endInclusive, bool expected)
        {
            // Arrange
            var interval = Interval.Create<int>(start, end, startInclusive, endInclusive);

            // Act
            var actual = interval.IsHalfOpen();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
