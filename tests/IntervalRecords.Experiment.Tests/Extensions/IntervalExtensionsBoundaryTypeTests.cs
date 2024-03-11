using IntervalRecords.Experiment.Extensions;

namespace IntervalRecords.Experiment.Tests.Extensions
{
    public class IntervalExtensionsBoundaryTypeTests
    {
        private const int start = 6;
        private const int end = 10;

        [Theory]
        [InlineData(true, true, BoundaryType.Closed)]
        [InlineData(true, false, BoundaryType.ClosedOpen)]
        [InlineData(false, true, BoundaryType.OpenClosed)]
        [InlineData(false, false, BoundaryType.Open)]
        public void GetBoundaryType_ToTuple_ShouldBeExpected(bool startInclusive, bool endInclusive, BoundaryType expected)
        {
            // Arrange
            var interval = new Interval<int>(start, end, startInclusive, endInclusive);

            // Act
            var actualIntervalType = interval.GetBoundaryType();

            // Assert
            actualIntervalType.Should().Be(expected);
        }

        [Theory]
        [InlineData(true, true, false)]
        [InlineData(true, false, true)]
        [InlineData(false, true, true)]
        [InlineData(false, false, false)]
        public void IsHalfOpen_ShouldBeExpected(bool startInclusive, bool endInclusive, bool expected)
        {
            // Arrange
            var interval = new Interval<int>(start, end, startInclusive, endInclusive);
            // Act
            var actual = interval.IsHalfOpen();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
