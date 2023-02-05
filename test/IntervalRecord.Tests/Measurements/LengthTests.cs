using InfinityComparable;
using IntervalRecord.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.Measurements
{
    public class LengthTests
    {
        private static readonly DateTimeOffset _referenceDateTimeOffset = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private static readonly DateOnly _referenceDateOnly = new DateOnly(_referenceDateTimeOffset.Year, _referenceDateTimeOffset.Month, _referenceDateTimeOffset.Day);
        private static readonly TimeOnly _referenceTimeOnly = new TimeOnly(_referenceDateTimeOffset.Hour, _referenceDateTimeOffset.Minute, _referenceDateTimeOffset.Second);

        [Theory]
        [InlineData(1, 4, 1)]
        [InlineData(1, 5, 1)]
        [InlineData(1, 6, 1)]
        [InlineData(1, 6, 2)]
        public void LengthWithStep_ShouldBe_ClosureWithStepLength(int start, int end, int step)
        {
            // Arrange
            var stepInHours = TimeSpan.FromHours(step);
            var integer = new Interval<int>(start, end, false, false);
            var doubles = new Interval<double>(start, end, false, false);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), false, false);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), false, false);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), false, false);

            // Act
            var actualInteger = integer.Length(step);
            var actualDouble = doubles.Length(step);
            var actualDateOnly = dateOnly.Length(step);
            var actualTimeOnly = timeOnly.Length(stepInHours);
            var actualDateTime = dateTime.Length(stepInHours);
            var actualDateTimeOffset = dateTimeOffset.Length(stepInHours);

            // Assert
            actualInteger.Should().Be(integer.Closure(step).Length());
            actualDouble.Should().Be(doubles.Closure(step).Length());
            actualDateOnly.Should().Be(dateOnly.Closure(step).Length());
            actualTimeOnly.Should().Be(timeOnly.Closure(stepInHours).Length());
            actualDateTime.Should().Be(dateTime.Closure(stepInHours).Length());
            actualDateTimeOffset.Should().Be(dateTimeOffset.Closure(stepInHours).Length());
        }



        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(1, 5)]
        [InlineData(1, 6)]
        public void Length(int start, int end)
        {
            // Arrange
            var integer = new Interval<int>(start, end, false, false);
            var doubles = new Interval<double>(start, end, false, false);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), false, false);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), false, false);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), false, false);

            // Act
            var actualInteger = integer.Length();
            var actualDouble = doubles.Length();
            var actualDateOnly = dateOnly.Length();
            var actualTimeOnly = timeOnly.Length();
            var actualDateTime = dateTime.Length();
            var actualDateTimeOffset = dateTimeOffset.Length();

            // Assert
            actualInteger.Should().Be(integer.End - integer.Start);
            actualDouble.Should().Be(doubles.End - doubles.Start);
            actualDateOnly.Should().Be(dateOnly.End.Value.DayNumber - dateOnly.Start.Value.DayNumber);
            actualTimeOnly.Should().Be(timeOnly.End.Value - timeOnly.Start.Value);
            actualDateTime.Should().Be(dateTime.End.Value - dateTime.Start.Value);
            actualDateTimeOffset.Should().Be(dateTimeOffset.End.Value - dateTimeOffset.Start.Value);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void LengthInfinity_ShouldBePositiveInfinity(int? start, int? end)
        {
            // Arrange
            var integer = new Interval<int>(start, end, false, false);
            var doubles = new Interval<double>(start, end, false, false);
            
            var dateOnly = new Interval<DateOnly>(
                start.HasValue ? _referenceDateOnly.AddDays(start.Value) : null,
                end.HasValue ?  _referenceDateOnly.AddDays(end.Value) : null,
                false,
                false);

            var timeOnly = new Interval<TimeOnly>(
                start.HasValue ? _referenceTimeOnly.AddHours(start.Value) : null,
                end.HasValue ? _referenceTimeOnly.AddHours(end.Value) : null,
                false,
                false);

            var dateTime = new Interval<DateTime>(
                start.HasValue ? _referenceDateTimeOffset.DateTime.AddHours(start.Value) : null,
                end.HasValue ? _referenceDateTimeOffset.DateTime.AddHours(end.Value) : null,
                false,
                false);

            var dateTimeOffset = new Interval<DateTimeOffset>(
                start.HasValue ? _referenceDateTimeOffset.AddHours(start.Value) : null,
                end.HasValue ? _referenceDateTimeOffset.AddHours(end.Value) : null,
                false,
                false);

            // Act
            var actualInteger = integer.Length();
            var actualDouble = doubles.Length();
            var actualDateOnly = dateOnly.Length();
            var actualTimeOnly = timeOnly.Length();
            var actualDateTime = dateTime.Length();
            var actualDateTimeOffset = dateTimeOffset.Length();

            // Assert
            actualInteger.Should().Be(Infinity<int>.PositiveInfinity);
            actualDouble.Should().Be(Infinity<double>.PositiveInfinity);
            actualDateOnly.Should().Be(Infinity<int>.PositiveInfinity);
            actualTimeOnly.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
            actualDateTime.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
            actualDateTimeOffset.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void LengthWithStepInfinity_ShouldBePositiveInfinity(int? start, int? end)
        {
            // Arrange
            var step = 1;
            var integer = new Interval<int>(start, end, false, false);
            var doubles = new Interval<double>(start, end, false, false);

            var dateOnly = new Interval<DateOnly>(
                start.HasValue ? _referenceDateOnly.AddDays(start.Value) : null,
                end.HasValue ? _referenceDateOnly.AddDays(end.Value) : null,
                false,
                false);

            var timeOnly = new Interval<TimeOnly>(
                start.HasValue ? _referenceTimeOnly.AddHours(start.Value) : null,
                end.HasValue ? _referenceTimeOnly.AddHours(end.Value) : null,
                false,
                false);

            var dateTime = new Interval<DateTime>(
                start.HasValue ? _referenceDateTimeOffset.DateTime.AddHours(start.Value) : null,
                end.HasValue ? _referenceDateTimeOffset.DateTime.AddHours(end.Value) : null,
                false,
                false);

            var dateTimeOffset = new Interval<DateTimeOffset>(
                start.HasValue ? _referenceDateTimeOffset.AddHours(start.Value) : null,
                end.HasValue ? _referenceDateTimeOffset.AddHours(end.Value) : null,
                false,
                false);

            // Act
            var actualInteger = integer.Length(step);
            var actualDouble = doubles.Length(step);
            var actualDateOnly = dateOnly.Length(step);
            var actualTimeOnly = timeOnly.Length(TimeSpan.FromHours(step));
            var actualDateTime = dateTime.Length(TimeSpan.FromHours(step));
            var actualDateTimeOffset = dateTimeOffset.Length(TimeSpan.FromHours(step));

            // Assert
            actualInteger.Should().Be(Infinity<int>.PositiveInfinity);
            actualDouble.Should().Be(Infinity<double>.PositiveInfinity);
            actualDateOnly.Should().Be(Infinity<int>.PositiveInfinity);
            actualTimeOnly.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
            actualDateTime.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
            actualDateTimeOffset.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
        }

        [Fact]
        public void LengthEmpty_ShouldBeZero()
        {
            // Arrange
            var integer = new Interval<int>(default, default, false, false);
            var doubles = new Interval<double>(default, default, false, false);
            var dateOnly = new Interval<DateOnly>(default, default, false, false);
            var timeOnly = new Interval<TimeOnly>(default, default, false, false);
            var dateTime = new Interval<DateTime>(default, default, false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(default, default, false, false);

            // Act
            var actualInteger = integer.Length();
            var actualDouble = doubles.Length();
            var actualDateOnly = dateOnly.Length();
            var actualTimeOnly = timeOnly.Length();
            var actualDateTime = dateTime.Length();
            var actualDateTimeOffset = dateTimeOffset.Length();

            // Assert
            actualInteger.Should().Be(0);
            actualDouble.Should().Be(0);
            actualDateOnly.Should().Be(0);
            actualTimeOnly.Should().Be(TimeSpan.Zero);
            actualDateTime.Should().Be(TimeSpan.Zero);
            actualDateTimeOffset.Should().Be(TimeSpan.Zero);
        }
    }
}
