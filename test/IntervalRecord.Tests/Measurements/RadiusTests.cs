using InfinityComparable;
using IntervalRecord.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.Measurements
{
    public class RadiusTests
    {
        private static readonly DateTimeOffset _referenceDateTimeOffset = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private static readonly DateOnly _referenceDateOnly = new DateOnly(_referenceDateTimeOffset.Year, _referenceDateTimeOffset.Month, _referenceDateTimeOffset.Day);
        private static readonly TimeOnly _referenceTimeOnly = new TimeOnly(_referenceDateTimeOffset.Hour, _referenceDateTimeOffset.Minute, _referenceDateTimeOffset.Second);

        [Theory]
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(1, 4, 1)]
        [InlineData(1, 5, 1)]
        [InlineData(1, 6, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(1, 3, 2)]
        [InlineData(1, 4, 2)]
        [InlineData(1, 5, 2)]
        [InlineData(1, 6, 2)]
        public void RadiusWithStep_ShouldBe_ClosureWithStepRadius(int start, int end, int step)
        {
            // Arrange
            var stepInHours = TimeSpan.FromHours(step);
            var integer = new Interval<int>(start, end, false, false);
            var @double = new Interval<double>(start, end, false, false);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), false, false);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), false, false);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), false, false);

            // Act
            var actualInteger = integer.Radius(step);
            var actualDouble = @double.Radius(step);
            var actualDateOnly = dateOnly.Radius(step);
            var actualTimeOnly = timeOnly.Radius(stepInHours);
            var actualDateTime = dateTime.Radius(stepInHours);
            var actualDateTimeOffset = dateTimeOffset.Radius(stepInHours);

            // Assert
            actualInteger.Should().Be(integer.Closure(step).Radius());
            actualDouble.Should().Be(@double.Closure(step).Radius());
            actualDateOnly.Should().Be(dateOnly.Closure(step).Radius());
            actualTimeOnly.Should().Be(timeOnly.Closure(stepInHours).Radius());
            actualDateTime.Should().Be(dateTime.Closure(stepInHours).Radius());
            actualDateTimeOffset.Should().Be(dateTimeOffset.Closure(stepInHours).Radius());
        }



        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(1, 5)]
        [InlineData(1, 6)]
        public void Radius(int start, int end)
        {
            // Arrange
            var integer = new Interval<int>(start, end, false, false);
            var @double = new Interval<double>(start, end, false, false);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), false, false);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), false, false);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), false, false);

            // Act
            var actualInteger = integer.Radius();
            var actualDouble = @double.Radius();
            var actualDateOnly = dateOnly.Radius();
            var actualTimeOnly = timeOnly.Radius();
            var actualDateTime = dateTime.Radius();
            var actualDateTimeOffset = dateTimeOffset.Radius();

            // Assert
            actualInteger.Should().Be(integer.Length() / 2);
            actualDouble.Should().Be(@double.Length() / 2);
            actualDateOnly.Should().Be(dateOnly.Length() / 2);
            actualTimeOnly.Should().Be(timeOnly.Length().Value / 2);
            actualDateTime.Should().Be(dateTime.Length().Value / 2);
            actualDateTimeOffset.Should().Be(dateTimeOffset.Length().Value / 2);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void RadiusInfinity_ShouldBeNull(int? start, int? end)
        {
            // Arrange
            var integer = new Interval<int>(start, end, false, false);
            var @double = new Interval<double>(start, end, false, false);

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
            var actualInteger = integer.Radius();
            var actualDouble = @double.Radius();
            var actualDateOnly = dateOnly.Radius();
            var actualTimeOnly = timeOnly.Radius();
            var actualDateTime = dateTime.Radius();
            var actualDateTimeOffset = dateTimeOffset.Radius();

            // Assert
            actualInteger.Should().BeNull();
            actualDouble.Should().BeNull();
            actualDateOnly.Should().BeNull();
            actualTimeOnly.Should().BeNull();
            actualDateTime.Should().BeNull();
            actualDateTimeOffset.Should().BeNull();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void RadiusWithStepInfinity_ShouldBeNull(int? start, int? end)
        {
            // Arrange
            var step = 1;
            var integer = new Interval<int>(start, end, false, false);
            var @double = new Interval<double>(start, end, false, false);

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
            var actualInteger = integer.Radius(step);
            var actualDouble = @double.Radius(step);
            var actualDateOnly = dateOnly.Radius(step);
            var actualTimeOnly = timeOnly.Radius(TimeSpan.FromHours(step));
            var actualDateTime = dateTime.Radius(TimeSpan.FromHours(step));
            var actualDateTimeOffset = dateTimeOffset.Radius(TimeSpan.FromHours(step));

            // Assert
            actualInteger.Should().BeNull();
            actualDouble.Should().BeNull();
            actualDateOnly.Should().BeNull();
            actualTimeOnly.Should().BeNull();
            actualDateTime.Should().BeNull();
            actualDateTimeOffset.Should().BeNull();
        }

        [Fact]
        public void RadiusEmpty_ShouldBeNull()
        {
            // Arrange
            var integer = new Interval<int>(default, default, false, false);
            var @double = new Interval<double>(default, default, false, false);
            var dateOnly = new Interval<DateOnly>(default, default, false, false);
            var timeOnly = new Interval<TimeOnly>(default, default, false, false);
            var dateTime = new Interval<DateTime>(default, default, false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(default, default, false, false);

            // Act
            var actualInteger = integer.Radius();
            var actualDouble = @double.Radius();
            var actualDateOnly = dateOnly.Radius();
            var actualTimeOnly = timeOnly.Radius();
            var actualDateTime = dateTime.Radius();
            var actualDateTimeOffset = dateTimeOffset.Radius();

            // Assert
            actualInteger.Should().BeNull();
            actualDouble.Should().BeNull();
            actualDateOnly.Should().BeNull();
            actualTimeOnly.Should().BeNull();
            actualDateTime.Should().BeNull();
            actualDateTimeOffset.Should().BeNull();
        }
    }
}
