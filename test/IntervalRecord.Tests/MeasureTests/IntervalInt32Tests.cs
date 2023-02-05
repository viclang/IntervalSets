using FluentAssertions.Execution;
using InfinityComparable;

namespace IntervalRecord.Tests.MeasurementsTests
{
    public class IntervalInt32Tests
    {
        private const int start = 6;
        private const int end = 10;
        private const int step = 1;

        [Theory]
        [InlineData(1, 1, IntervalType.Closed, 0)]
        [InlineData(1, 2, IntervalType.Closed, 1)]
        [InlineData(1, 3, IntervalType.Closed, 2)]
        [InlineData(1, 1, IntervalType.ClosedOpen, 0)]
        [InlineData(1, 2, IntervalType.ClosedOpen, 1)]
        [InlineData(1, 3, IntervalType.ClosedOpen, 2)]
        [InlineData(1, 1, IntervalType.OpenClosed, 0)]
        [InlineData(1, 2, IntervalType.OpenClosed, 1)]
        [InlineData(1, 3, IntervalType.OpenClosed, 2)]
        [InlineData(1, 1, IntervalType.Open, 0)]
        [InlineData(1, 2, IntervalType.Open, 1)]
        [InlineData(1, 3, IntervalType.Open, 2)]
        public void GivenBoundedInterval_WhenMeasuringLength_ReturnsExpected(int start, int end, IntervalType boundaryType, int expected)
        {
            // Arrange
            var interval = Interval.WithBoundaryType<int>(start, end, boundaryType);

            // Act
            var actual = interval.Length();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 1, IntervalType.Closed, 1d)]
        [InlineData(1, 2, IntervalType.Closed, 1.5)]
        [InlineData(1, 3, IntervalType.Closed, 2d)]
        [InlineData(1, 1, IntervalType.ClosedOpen, null)]
        [InlineData(1, 2, IntervalType.ClosedOpen, 1.5)]
        [InlineData(1, 3, IntervalType.ClosedOpen, 2d)]
        [InlineData(1, 1, IntervalType.OpenClosed, null)]
        [InlineData(1, 2, IntervalType.OpenClosed, 1.5)]
        [InlineData(1, 3, IntervalType.OpenClosed, 2d)]
        [InlineData(1, 1, IntervalType.Open, null)]
        [InlineData(1, 2, IntervalType.Open, 1.5)]
        [InlineData(1, 3, IntervalType.Open, 2d)]
        public void GivenBoundedInterval_WhenMeasuringCentre_ReturnsExpected(int? start, int? end, IntervalType boundaryType, double? expected)
        {
            // Arrange
            var interval = Interval.WithBoundaryType<int>(start, end, boundaryType);

            // Act
            var actual = interval.Centre();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 1, IntervalType.Closed, 0d)]
        [InlineData(1, 2, IntervalType.Closed, 0.5)]
        [InlineData(1, 3, IntervalType.Closed, 1d)]
        [InlineData(1, 1, IntervalType.ClosedOpen, null)]
        [InlineData(1, 2, IntervalType.ClosedOpen, 0.5)]
        [InlineData(1, 3, IntervalType.ClosedOpen, 1d)]
        [InlineData(1, 1, IntervalType.OpenClosed, null)]
        [InlineData(1, 2, IntervalType.OpenClosed, 0.5)]
        [InlineData(1, 3, IntervalType.OpenClosed, 1d)]
        [InlineData(1, 1, IntervalType.Open, null)]
        [InlineData(1, 2, IntervalType.Open, 0.5)]
        [InlineData(1, 3, IntervalType.Open, 1d)]
        public void GivenBoundedInterval_WhenMeasuringRadius_ReturnsExpected(int? start, int? end, IntervalType boundaryType, double? expected)
        {
            // Arrange
            var interval = Interval.WithBoundaryType<int>(start, end, boundaryType);

            // Act
            var actual = interval.Radius();

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(IntervalType.Closed, IntervalType.Closed)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.Closed)]
        [InlineData(IntervalType.OpenClosed, IntervalType.Closed)]
        [InlineData(IntervalType.Open, IntervalType.Closed)]
        [InlineData(IntervalType.Closed, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.Open, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.Closed, IntervalType.OpenClosed)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.OpenClosed)]
        [InlineData(IntervalType.OpenClosed, IntervalType.OpenClosed)]
        [InlineData(IntervalType.Open, IntervalType.OpenClosed)]
        [InlineData(IntervalType.Closed, IntervalType.Open)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.Open)]
        [InlineData(IntervalType.OpenClosed, IntervalType.Open)]
        [InlineData(IntervalType.Open, IntervalType.Open)]
        public void GivenBoundedIntervalWithBoundaryType_WhenCanonicalized_ReturnsExpected(IntervalType boundaryType, IntervalType expectedBoundaryType)
        {
            // Arrange
            var interval = Interval.WithBoundaryType<int>(start, end, boundaryType);

            // Act
            var actual = interval.Canonicalize(expectedBoundaryType, step);

            // Assert
            var (expectedStartInclusive, expectedEndInclusive) = expectedBoundaryType.ToTuple();

            var expectedStart = expectedStartInclusive
                ? interval.StartInclusive ? start : start + step
                : interval.StartInclusive ? start - step : start;

            var expectedEnd = expectedEndInclusive
                ? interval.EndInclusive ? end : end - step
                : interval.EndInclusive ? end + step : end;

            actual.Should().Be(new Interval<int>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
        }

        [Fact]
        public void GivenEmptyInterval_WhenMeasuringLength_ReturnsZero()
        {
            // Arrange
            var interval = Interval.Empty<int>();

            // Act
            var actual = interval.Length();

            // Assert
            actual.Should().Be(0);
        }

        [Fact]
        public void GivenEmptyInterval_WhenMeasuringCentre_ReturnsNull()
        {
            // Arrange
            var interval = Interval.Empty<int>();

            // Act
            var actual = interval.Centre();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void GivenEmptyInterval_WhenMeasuringRadius_ReturnsNull()
        {
            // Arrange
            var interval = Interval.Empty<int>();

            // Act
            var actual = interval.Radius();

            // Assert
            actual.Should().BeNull();
        }

        [Theory]
        [InlineData(IntervalType.Closed)]
        [InlineData(IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed)]
        [InlineData(IntervalType.Open)]
        public void GivenEmptyInterval_WhenCanonicalized_ThenIntervalRemainsTheSame(IntervalType boundaryType)
        {
            // Arrange
            var empty = Interval.Empty<int>();

            // Act
            var actual = empty.Canonicalize(boundaryType, 1);

            // Assert
            actual.Should().Be(empty);
        }

        [Fact]
        public void GivenOpenInterval_WhenUsingClosure_ReturnsCanonicalizeClosed()
        {
            // Arrange
            var open = Interval.Open(start, end);

            // Act
            var actual = open.Closure(1);

            // Assert
            actual.Should().Be(open.Canonicalize(IntervalType.Closed, 1));
        }

        [Fact]
        public void GivenClosedInterval_WhenUsingInterior_ReturnsCanonicalizeOpen()
        {
            // Arrange
            var closed = Interval.Closed(start, end);

            // Act
            var actualInterior = closed.Interior(1);

            // Assert
            actualInterior.Should().Be(closed.Canonicalize(IntervalType.Open, 1));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void GivenUnboundedOrHalfBoundedInterval_WhenMeasuringLength_ReturnsPositiveInfinity(int? start, int? end)
        {
            // Arrange
            var interval = new Interval<int>(start, end, false, false);

            // Act
            var actual = interval.Length();

            // Assert
            actual.Should().Be(Infinity<int>.PositiveInfinity);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void GivenUnboundedOrHalfBoundedInterval_WhenMeasuringCentre_ReturnsNull(int? start, int? end)
        {
            // Arrange
            var interval = new Interval<int>(start, end, true, true);

            // Act
            var actual = interval.Centre();

            // Assert
            actual.Should().BeNull();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void GivenUnboundedOrHalfBoundedInterval_WhenMeasuringRadius_ReturnsNull(int? start, int? end)
        {
            // Arrange
            var interval = new Interval<int>(start, end, true, true);

            // Act
            var actual = interval.Radius();

            // Assert
            actual.Should().BeNull();
        }

        [Theory]
        [InlineData(IntervalType.Closed)]
        [InlineData(IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed)]
        [InlineData(IntervalType.Open)]
        public void GivenUnboundedOrHalfBoundedInterval_WhenCanonicalized_ThenIntervalRemainsTheSame(IntervalType boundaryType)
        {
            // Arrange
            var unbounded = Interval.All<int>();

            // Act
            var actual = unbounded.Canonicalize(boundaryType, 1);

            // Assert
            actual.Should().Be(unbounded);
        }
    }
}
