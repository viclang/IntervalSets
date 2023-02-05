//using FluentAssertions.Execution;
//using System;

//namespace IntervalRecord.Tests.Canonicalizers
//{
//    public class CanonicalizeTestsOld
//    {
//        private const int start = 6;
//        private const int end = 10;
//        private const int step = 1;
//        private static readonly TimeSpan _stepInHours = TimeSpan.FromHours(step);
//        private static readonly DateTimeOffset _referenceDateTimeOffset = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
//        private static readonly DateTime _referenceDateTime = _referenceDateTimeOffset.DateTime;
//        private static readonly DateOnly _referenceDateOnly = new DateOnly(_referenceDateTime.Year, _referenceDateTime.Month, _referenceDateTime.Day);
//        private static readonly TimeOnly _referenceTimeOnly = new TimeOnly(_referenceDateTime.Hour, _referenceDateTime.Minute, _referenceDateTime.Second, _referenceDateTime.Millisecond);

//        [Theory]
//        [InlineData(IntervalType.Closed)]
//        [InlineData(IntervalType.ClosedOpen)]
//        [InlineData(IntervalType.OpenClosed)]
//        [InlineData(IntervalType.Open)]
//        public void Closure(IntervalType intervalType)
//        {
//            // Arrange
//            var (startInclusive, endInclusive) = intervalType.ToTuple();
//            var integer = new Interval<int>(start, end, startInclusive, endInclusive);
//            var doubles = new Interval<double>(start, end, startInclusive, endInclusive);
//            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), startInclusive, endInclusive);
//            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), startInclusive, endInclusive);
//            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), startInclusive, endInclusive);
//            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), startInclusive, endInclusive);

//            // Act
//            var actualInteger = integer.Closure(step);
//            var actualDouble = doubles.Closure(step);
//            var actualDateOnly = dateOnly.Closure(step);
//            var actualTimeOnly = timeOnly.Closure(_stepInHours);
//            var actualDateTime = dateTime.Closure(_stepInHours);
//            var actualDateTimeOffset = dateTimeOffset.Closure(_stepInHours);

//            // Assert
//            var (expectedStartInclusive, expectedEndInclusive) = IntervalType.Closed.ToTuple();
//            var expectedStart = startInclusive ? start : start + step;
//            var expectedEnd = endInclusive ? end : end - step;

//            using (new AssertionScope())
//            {
//                actualInteger.Should().Be(new Interval<int>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
//                actualDouble.Should().Be(new Interval<double>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
//                actualDateOnly.Should().Be(new Interval<DateOnly>(_referenceDateOnly.AddDays(expectedStart), _referenceDateOnly.AddDays(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualTimeOnly.Should().Be(new Interval<TimeOnly>(_referenceTimeOnly.AddHours(expectedStart), _referenceTimeOnly.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualDateTime.Should().Be(new Interval<DateTime>(_referenceDateTime.AddHours(expectedStart), _referenceDateTime.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualDateTimeOffset.Should().Be(new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(expectedStart), _referenceDateTimeOffset.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//            }
//        }

//        [Theory]
//        [InlineData(IntervalType.Closed)]
//        [InlineData(IntervalType.ClosedOpen)]
//        [InlineData(IntervalType.OpenClosed)]
//        [InlineData(IntervalType.Open)]
//        public void Interior(IntervalType intervalType)
//        {
//            // Arrange
//            var (startInclusive, endInclusive) = intervalType.ToTuple();
//            var integer = new Interval<int>(start, end, startInclusive, endInclusive);
//            var doubles = new Interval<double>(start, end, startInclusive, endInclusive);
//            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), startInclusive, endInclusive);
//            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), startInclusive, endInclusive);
//            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), startInclusive, endInclusive);
//            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), startInclusive, endInclusive);

//            // Act
//            var actualInteger = integer.Interior(step);
//            var actualDouble = doubles.Interior(step);
//            var actualDateOnly = dateOnly.Interior(step);
//            var actualTimeOnly = timeOnly.Interior(_stepInHours);
//            var actualDateTime = dateTime.Interior(_stepInHours);
//            var actualDateTimeOffset = dateTimeOffset.Interior(_stepInHours);

//            // Assert
//            var (expectedStartInclusive, expectedEndInclusive) = IntervalType.Open.ToTuple();
//            var expectedStart = startInclusive ? start - step : start;
//            var expectedEnd = endInclusive ? end + step : end;

//            using (new AssertionScope())
//            {
//                actualInteger.Should().Be(new Interval<int>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
//                actualDouble.Should().Be(new Interval<double>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
//                actualDateOnly.Should().Be(new Interval<DateOnly>(_referenceDateOnly.AddDays(expectedStart), _referenceDateOnly.AddDays(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualTimeOnly.Should().Be(new Interval<TimeOnly>(_referenceTimeOnly.AddHours(expectedStart), _referenceTimeOnly.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualDateTime.Should().Be(new Interval<DateTime>(_referenceDateTime.AddHours(expectedStart), _referenceDateTime.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualDateTimeOffset.Should().Be(new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(expectedStart), _referenceDateTimeOffset.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//            }
//        }

//        [Theory]
//        [InlineData(IntervalType.Closed, IntervalType.Closed)]
//        [InlineData(IntervalType.ClosedOpen, IntervalType.Closed)]
//        [InlineData(IntervalType.OpenClosed, IntervalType.Closed)]
//        [InlineData(IntervalType.Open, IntervalType.Closed)]

//        [InlineData(IntervalType.Closed, IntervalType.ClosedOpen)]
//        [InlineData(IntervalType.ClosedOpen, IntervalType.ClosedOpen)]
//        [InlineData(IntervalType.OpenClosed, IntervalType.ClosedOpen)]
//        [InlineData(IntervalType.Open, IntervalType.ClosedOpen)]

//        [InlineData(IntervalType.Closed, IntervalType.OpenClosed)]
//        [InlineData(IntervalType.ClosedOpen, IntervalType.OpenClosed)]
//        [InlineData(IntervalType.OpenClosed, IntervalType.OpenClosed)]
//        [InlineData(IntervalType.Open, IntervalType.OpenClosed)]

//        [InlineData(IntervalType.Closed, IntervalType.Open)]
//        [InlineData(IntervalType.ClosedOpen, IntervalType.Open)]
//        [InlineData(IntervalType.OpenClosed, IntervalType.Open)]
//        [InlineData(IntervalType.Open, IntervalType.Open)]
//        public void Canonicalize_ShouldBeExpected(IntervalType intervalType, IntervalType expectedIntervalType)
//        {
//            // Arrange
//            var (startInclusive, endInclusive) = intervalType.ToTuple();
//            var integer = new Interval<int>(start, end, startInclusive, endInclusive);
//            var doubles = new Interval<double>(start, end, startInclusive, endInclusive);
//            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), startInclusive, endInclusive);
//            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), startInclusive, endInclusive);
//            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), startInclusive, endInclusive);
//            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), startInclusive, endInclusive);

//            // Act
//            var actualInteger = integer.Canonicalize(expectedIntervalType, step);
//            var actualDouble = doubles.Canonicalize(expectedIntervalType, step);
//            var actualDateOnly = dateOnly.Canonicalize(expectedIntervalType, step);
//            var actualTimeOnly = timeOnly.Canonicalize(expectedIntervalType, _stepInHours);
//            var actualDateTime = dateTime.Canonicalize(expectedIntervalType, _stepInHours);
//            var actualDateTimeOffset = dateTimeOffset.Canonicalize(expectedIntervalType, _stepInHours);

//            // Assert
//            var (expectedStartInclusive, expectedEndInclusive) = expectedIntervalType.ToTuple();

//            var expectedStart = expectedStartInclusive
//                ? startInclusive ? start : start + step
//                : startInclusive ? start - step : start;

//            var expectedEnd = expectedEndInclusive
//                ? endInclusive ? end : end - step
//                : endInclusive ? end + step : end;

//            using (new AssertionScope())
//            {
//                actualInteger.Should().Be(new Interval<int>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
//                actualDouble.Should().Be(new Interval<double>(expectedStart, expectedEnd, expectedStartInclusive, expectedEndInclusive));
//                actualDateOnly.Should().Be(new Interval<DateOnly>(_referenceDateOnly.AddDays(expectedStart), _referenceDateOnly.AddDays(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualTimeOnly.Should().Be(new Interval<TimeOnly>(_referenceTimeOnly.AddHours(expectedStart), _referenceTimeOnly.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualDateTime.Should().Be(new Interval<DateTime>(_referenceDateTime.AddHours(expectedStart), _referenceDateTime.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//                actualDateTimeOffset.Should().Be(new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(expectedStart), _referenceDateTimeOffset.AddHours(expectedEnd), expectedStartInclusive, expectedEndInclusive));
//            }
//        }

//        [Fact]
//        public void Closure_ShouldBe_CanonicalizeClosed()
//        {
//            // Arrange
//            var open = Interval.Open(start, end);

//            // Act
//            var actual = open.Closure(1);
//            var actualCanonicalize = open.Canonicalize(IntervalType.Closed, 1);

//            // Assert
//            actual.Should().Be(actualCanonicalize);
//        }

//        [Fact]
//        public void Interior_ShouldBe_CanonicalizeOpen()
//        {
//            // Arrange
//            var closed = Interval.Closed(start, end);

//            // Act
//            var actualInterior = closed.Interior(1);
//            var actualCanonicalize = closed.Canonicalize(IntervalType.Open, 1);

//            // Assert
//            actualInterior.Should().Be(actualCanonicalize);
//        }

//        [Fact]
//        public void UnboundedInterval_ShouldNeverBeClosed()
//        {
//            // Arrange
//            var unbounded = Interval.All<int>();

//            // Act
//            var actual = new Interval<int>[]
//            {
//                unbounded.Closure(1),
//                unbounded.Canonicalize(IntervalType.ClosedOpen, 1),
//                unbounded.Canonicalize(IntervalType.OpenClosed, 1)
//            };

//            // Assert
//            actual.Should().AllBeEquivalentTo(unbounded);
//        }

//        [Fact]
//        public void EmptyInterval_ShouldNeverBeClosed()
//        {
//            // Arrange
//            var empty = Interval.Empty<int>();

//            // Act
//            var actual = new Interval<int>[]
//            {
//                empty.Closure(1),
//                empty.Canonicalize(IntervalType.ClosedOpen, 1),
//                empty.Canonicalize(IntervalType.OpenClosed, 1)
//            };

//            // Assert
//            actual.Should().AllBeEquivalentTo(empty);
//        }
//    }
//}
