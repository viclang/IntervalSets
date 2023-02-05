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
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(1, 5)]
        [InlineData(1, 6)]
        public void Centre(int start, int end)
        {
            // Arrange
            var integer = new Interval<int>(start, end, true, true);
            var doubles = new Interval<double>(start, end, true, true);
            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), true, true);
            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), true, true);
            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), true, true);
            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), true, true);

            // Act
            var actualInteger = integer.Centre();
            var actualDouble = doubles.Centre();
            var actualDateOnly = dateOnly.Centre();
            var actualTimeOnly = timeOnly.Centre();
            var actualDateTime = dateTime.Centre();
            var actualDateTimeOffset = dateTimeOffset.Centre();

            // Assert
            actualInteger.Should().Be((integer.End.Finite!.Value + (double)integer.Start.Finite!.Value) / 2);
            actualDouble.Should().Be((doubles.End + doubles.Start) / 2);
            actualDateOnly.Should().Be(dateOnly.Start.Finite!.Value.AddDays(dateOnly.Length().Finite!.Value / 2));
            actualTimeOnly.Should().Be(timeOnly.Start.Finite!.Value.Add(timeOnly.Length().Finite!.Value / 2));
            actualDateTime.Should().Be(dateTime.Start.Finite!.Value.Add(dateTime.Length().Finite!.Value / 2));
            actualDateTimeOffset.Should().Be(dateTimeOffset.Start.Finite!.Value.Add(dateTimeOffset.Length().Finite!.Value / 2));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public void CentreInfinity_ShouldBeNull(int? start, int? end)
        {
            // Arrange
            var integer = new Interval<int>(start, end, true, true);
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
            var actualInteger = integer.Centre();
            var actualDouble = doubles.Centre();
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

        [Fact]
        public void CentreEmpty_ShouldBeNull()
        {
            // Arrange
            var integer = new Interval<int>(default, default, false, false);
            var doubles = new Interval<double>(default, default, false, false);
            var dateOnly = new Interval<DateOnly>(default, default, false, false);
            var timeOnly = new Interval<TimeOnly>(default, default, false, false);
            var dateTime = new Interval<DateTime>(default, default, false, false);
            var dateTimeOffset = new Interval<DateTimeOffset>(default, default, false, false);

            // Act
            var actualInteger = integer.Centre();
            var actualDouble = doubles.Centre();
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
