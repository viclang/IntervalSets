using InfinityComparable;
using IntervalRecord.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.Measurements
{
    public class CentreTests
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
        public void CentreWithStep_ShouldBe_ClosureWithStepCentre(int start, int end, int step)
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
            var actualInteger = integer.Centre(step);
            var actualDouble = @double.Centre(step);
            var actualDateOnly = dateOnly.Centre(step);
            var actualTimeOnly = timeOnly.Centre(stepInHours);
            var actualDateTime = dateTime.Centre(stepInHours);
            var actualDateTimeOffset = dateTimeOffset.Centre(stepInHours);

            // Assert
            actualInteger.Should().Be(integer.Closure(step).Centre());
            actualDouble.Should().Be(@double.Closure(step).Centre());
            actualDateOnly.Should().Be(dateOnly.Closure(step).Centre());
            actualTimeOnly.Should().Be(timeOnly.Closure(stepInHours).Centre());
            actualDateTime.Should().Be(dateTime.Closure(stepInHours).Centre());
            actualDateTimeOffset.Should().Be(dateTimeOffset.Closure(stepInHours).Centre());
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(1, 5)]
        [InlineData(1, 6)]
        public void Centre(int start, int end)
        {
            // Arrange
            var integer = new Interval<int>(start, end, false, false);
            var @double = new Interval<double>(start, end, false, false);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), false, false);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), false, false);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), false, false);

            // Act
            var actualInteger = integer.Centre();
            var actualDouble = @double.Centre();
            var actualDateOnly = dateOnly.Centre();
            var actualTimeOnly = timeOnly.Centre();
            var actualDateTime = dateTime.Centre();
            var actualDateTimeOffset = dateTimeOffset.Centre();

            // Assert
            actualInteger.Should().Be((integer.End.Value + (double)integer.Start.Value) / 2);
            actualDouble.Should().Be((@double.End + @double.Start) / 2);
            actualDateOnly.Should().Be(dateOnly.Start.Value.AddDays(dateOnly.Length().Value / 2));
            actualTimeOnly.Should().Be(timeOnly.Start.Value.Add(timeOnly.Length().Value / 2));
            actualDateTime.Should().Be(dateTime.Start.Value.Add(dateTime.Length().Value / 2));
            actualDateTimeOffset.Should().Be(dateTimeOffset.Start.Value.Add(dateTimeOffset.Length().Value / 2));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void CentreInfinity_ShouldBeNull(int? start, int? end)
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
            var actualInteger = integer.Centre();
            var actualDouble = @double.Centre();
            var actualDateOnly = dateOnly.Centre();
            var actualTimeOnly = timeOnly.Centre();
            var actualDateTime = dateTime.Centre();
            var actualDateTimeOffset = dateTimeOffset.Centre();

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
        public void CentreWithStepInfinity_ShouldBeNull(int? start, int? end)
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
            var actualInteger = integer.Centre(step);
            var actualDouble = @double.Centre(step);
            var actualDateOnly = dateOnly.Centre(step);
            var actualTimeOnly = timeOnly.Centre(TimeSpan.FromHours(step));
            var actualDateTime = dateTime.Centre(TimeSpan.FromHours(step));
            var actualDateTimeOffset = dateTimeOffset.Centre(TimeSpan.FromHours(step));

            // Assert
            actualInteger.Should().BeNull();
            actualDouble.Should().BeNull();
            actualDateOnly.Should().BeNull();
            actualTimeOnly.Should().BeNull();
            actualDateTime.Should().BeNull();
            actualDateTimeOffset.Should().BeNull();
        }

        [Fact]
        public void CentreEmpty_ShouldBeNull()
        {
            // Arrange
            var integer = new Interval<int>(default, default, false, false);
            var @double = new Interval<double>(default, default, false, false);
            var dateOnly = new Interval<DateOnly>(default, default, false, false);
            var timeOnly = new Interval<TimeOnly>(default, default, false, false);
            var dateTime = new Interval<DateTime>(default, default, false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(default, default, false, false);

            // Act
            var actualInteger = integer.Centre();
            var actualDouble = @double.Centre();
            var actualDateOnly = dateOnly.Centre();
            var actualTimeOnly = timeOnly.Centre();
            var actualDateTime = dateTime.Centre();
            var actualDateTimeOffset = dateTimeOffset.Centre();

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
