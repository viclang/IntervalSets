using System;

namespace IntervalRecord.Tests
{
    public class CanonicalizeTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int step = 1;
        private static readonly TimeSpan _stepInHours = TimeSpan.FromHours(step);
        private static readonly DateTimeOffset _referenceDateTimeOffset = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private static readonly DateTime _referenceDateTime = _referenceDateTimeOffset.DateTime;
        private static readonly DateOnly _referenceDateOnly = new DateOnly(_referenceDateTime.Year, _referenceDateTime.Month, _referenceDateTime.Day);
        private static readonly TimeOnly _referenceTimeOnly = new TimeOnly(_referenceDateTime.Hour, _referenceDateTime.Minute, _referenceDateTime.Second, _referenceDateTime.Millisecond);

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void Closure(bool startInclusive, bool endInclusive)
        {
            // Arrange
            var integer = new Interval<int>(start, end, startInclusive, endInclusive);
            var doubles = new Interval<double>(start, end, startInclusive, endInclusive);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), startInclusive, endInclusive);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), startInclusive, endInclusive);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), startInclusive, endInclusive);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), startInclusive, endInclusive);

            // Act
            var actualInteger = integer.Closure(step);
            var actualDouble = doubles.Closure(step);
            var actualDateOnly = dateOnly.Closure(step);
            var actualTimeOnly = timeOnly.Closure(_stepInHours);
            var actualDateTime = dateTime.Closure(_stepInHours);
            var actualDateTimeOffset = dateTimeOffset.Closure(_stepInHours);

            // Assert
            var expectedStart = startInclusive ? start : start + step;
            var expectedEnd = endInclusive ? end : end - step;
            var expectedStartInclusive = true;
            var expectedEndInclusive = true;

            actualInteger.Should().Be(new Interval<int>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
            actualDouble.Should().Be(new Interval<double>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
            actualDateOnly.Should().Be(new Interval<DateOnly>(_referenceDateOnly.AddDays(expectedStart), _referenceDateOnly.AddDays(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualTimeOnly.Should().Be(new Interval<TimeOnly>(_referenceTimeOnly.AddHours(expectedStart), _referenceTimeOnly.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualDateTime.Should().Be(new Interval<DateTime>(_referenceDateTime.AddHours(expectedStart), _referenceDateTime.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualDateTimeOffset.Should().Be(new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(expectedStart), _referenceDateTimeOffset.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void Interior(bool startInclusive, bool endInclusive)
        {
            // Arrange
            var integer = new Interval<int>(start, end, startInclusive, endInclusive);
            var doubles = new Interval<double>(start, end, startInclusive, endInclusive);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), startInclusive, endInclusive);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), startInclusive, endInclusive);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), startInclusive, endInclusive);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), startInclusive, endInclusive);

            // Act
            var actualInteger = integer.Interior(step);
            var actualDouble = doubles.Interior(step);
            var actualDateOnly = dateOnly.Interior(step);
            var actualTimeOnly = timeOnly.Interior(_stepInHours);
            var actualDateTime = dateTime.Interior(_stepInHours);
            var actualDateTimeOffset = dateTimeOffset.Interior(_stepInHours);

            // Assert
            var expectedStart = startInclusive ? start - step : start;
            var expectedEnd = endInclusive ? end + step : end;
            var expectedStartInclusive = false;
            var expectedEndInclusive = false;

            actualInteger.Should().Be(new Interval<int>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
            actualDouble.Should().Be(new Interval<double>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
            actualDateOnly.Should().Be(new Interval<DateOnly>(_referenceDateOnly.AddDays(expectedStart), _referenceDateOnly.AddDays(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualTimeOnly.Should().Be(new Interval<TimeOnly>(_referenceTimeOnly.AddHours(expectedStart), _referenceTimeOnly.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualDateTime.Should().Be(new Interval<DateTime>(_referenceDateTime.AddHours(expectedStart), _referenceDateTime.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualDateTimeOffset.Should().Be(new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(expectedStart), _referenceDateTimeOffset.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
        }

        [Theory]
        [InlineData(BoundaryType.Closed, false, false)]
        [InlineData(BoundaryType.Closed, true, false)]
        [InlineData(BoundaryType.Closed, false, true)]
        [InlineData(BoundaryType.Closed, true, true)]
        [InlineData(BoundaryType.ClosedOpen, false, false)]
        [InlineData(BoundaryType.ClosedOpen, true, false)]
        [InlineData(BoundaryType.ClosedOpen, false, true)]
        [InlineData(BoundaryType.ClosedOpen, true, true)]
        [InlineData(BoundaryType.OpenClosed, false, false)]
        [InlineData(BoundaryType.OpenClosed, true, false)]
        [InlineData(BoundaryType.OpenClosed, false, true)]
        [InlineData(BoundaryType.OpenClosed, true, true)]
        [InlineData(BoundaryType.Open, false, false)]
        [InlineData(BoundaryType.Open, true, false)]
        [InlineData(BoundaryType.Open, false, true)]
        [InlineData(BoundaryType.Open, true, true)]
        public void Canonicalize_ShouldBeExpected(BoundaryType expectedBoundaryType, bool startInclusive, bool endInclusive)
        {
            // Arrange
            var integer = new Interval<int>(start, end, startInclusive, endInclusive);
            var doubles = new Interval<double>(start, end, startInclusive, endInclusive);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), startInclusive, endInclusive);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), startInclusive, endInclusive);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), startInclusive, endInclusive);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), startInclusive, endInclusive);

            // Act
            var actualInteger = integer.Canonicalize(expectedBoundaryType, step);
            var actualDouble = doubles.Canonicalize(expectedBoundaryType, step);
            var actualDateOnly = dateOnly.Canonicalize(expectedBoundaryType, step);
            var actualTimeOnly = timeOnly.Canonicalize(expectedBoundaryType, _stepInHours);
            var actualDateTime = dateTime.Canonicalize(expectedBoundaryType, _stepInHours);
            var actualDateTimeOffset = dateTimeOffset.Canonicalize(expectedBoundaryType, _stepInHours);
            // Assert
            var (expectedStartInclusive, expectedEndInclusive) = expectedBoundaryType.ToTuple();

            var expectedStart = expectedStartInclusive
                ? startInclusive ? start : start + step
                : startInclusive ? start - step : start;

            var expectedEnd = expectedEndInclusive
                ? endInclusive ? end : end - step
                : endInclusive ? end + step : end;

            actualInteger.Should().Be(new Interval<int>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
            actualDouble.Should().Be(new Interval<double>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
            actualDateOnly.Should().Be(new Interval<DateOnly>(_referenceDateOnly.AddDays(expectedStart), _referenceDateOnly.AddDays(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualTimeOnly.Should().Be(new Interval<TimeOnly>(_referenceTimeOnly.AddHours(expectedStart), _referenceTimeOnly.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualDateTime.Should().Be(new Interval<DateTime>(_referenceDateTime.AddHours(expectedStart), _referenceDateTime.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
            actualDateTimeOffset.Should().Be(new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(expectedStart), _referenceDateTimeOffset.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
        }

        [Fact]
        public void Closure_ShouldBe_CanonicalizeClosed()
        {
            // Arrange
            var open = Interval.Open(start, end);

            // Act
            var actual = open.Closure(1);
            var actualCanonicalize = open.Canonicalize(BoundaryType.Closed, 1);

            // Assert
            actual.Should().Be(actualCanonicalize);
        }

        [Fact]
        public void Interior_ShouldBe_CanonicalizeOpen()
        {
            // Arrange
            var closed = Interval.Closed(start, end);

            // Act
            var actualInterior = closed.Interior(1);
            var actualCanonicalize = closed.Canonicalize(BoundaryType.Open, 1);

            // Assert
            actualInterior.Should().Be(actualCanonicalize);
        }

        [Fact]
        public void UnboundedInterval_ShouldNeverBeClosed()
        {
            // Arrange
            var unbounded = new Interval<int>();

            // Act
            var actual = new Interval<int>[]
            {
                unbounded.Closure(1),
                unbounded.Canonicalize(BoundaryType.ClosedOpen, 1),
                unbounded.Canonicalize(BoundaryType.OpenClosed, 1)
            };

            // Assert
            actual.Should().AllBeEquivalentTo(unbounded);
        }

        [Fact]
        public void EmptyInterval_ShouldNeverBeClosed()
        {
            // Arrange
            var empty = Interval.Empty<int>();

            // Act
            var actual = new Interval<int>[]
            {
                empty.Closure(1),
                empty.Canonicalize(BoundaryType.ClosedOpen, 1),
                empty.Canonicalize(BoundaryType.OpenClosed, 1)
            };

            // Assert
            actual.Should().AllBeEquivalentTo(empty);
        }
    }
}
