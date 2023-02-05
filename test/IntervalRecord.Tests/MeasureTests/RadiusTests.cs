//using FluentAssertions.Execution;
//using System;

//namespace IntervalRecord.Tests.Calculators
//{
//    public class RadiusTests
//    {
//        private static readonly DateTimeOffset _referenceDateTimeOffset = new(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
//        private static readonly DateOnly _referenceDateOnly = new(_referenceDateTimeOffset.Year, _referenceDateTimeOffset.Month, _referenceDateTimeOffset.Day);
//        private static readonly TimeOnly _referenceTimeOnly = new(_referenceDateTimeOffset.Hour, _referenceDateTimeOffset.Minute, _referenceDateTimeOffset.Second);

//        [Theory]
//        [InlineData(1, 1)]
//        [InlineData(1, 2)]
//        [InlineData(1, 3)]
//        [InlineData(1, 4)]
//        [InlineData(1, 5)]
//        [InlineData(1, 6)]
//        public void Radius(int start, int end)
//        {
//            // Arrange
//            var integer = new Interval<int>(start, end, false, false);
//            var doubles = new Interval<double>(start, end, false, false);
//            var dateOnly = new Interval<DateOnly>(_referenceDateOnly.AddDays(start), _referenceDateOnly.AddDays(end), false, false);
//            var timeOnly = new Interval<TimeOnly>(_referenceTimeOnly.AddHours(start), _referenceTimeOnly.AddHours(end), false, false);
//            var dateTime = new Interval<DateTime>(_referenceDateTimeOffset.DateTime.AddHours(start), _referenceDateTimeOffset.DateTime.AddHours(end), false, false);
//            var dateTimeOffset = new Interval<DateTimeOffset>(_referenceDateTimeOffset.AddHours(start), _referenceDateTimeOffset.AddHours(end), false, false);

//            // Act
//            var actualInteger = integer.Radius();
//            var actualDouble = doubles.Radius();
//            var actualDateOnly = dateOnly.Radius();
//            var actualTimeOnly = timeOnly.Radius();
//            var actualDateTime = dateTime.Radius();
//            var actualDateTimeOffset = dateTimeOffset.Radius();

//            // Assert
//            using (new AssertionScope())
//            {
//                actualInteger.Should().Be((int?)integer.Length() / 2);
//                actualDouble.Should().Be((double?)doubles.Length() / 2);
//                actualDateOnly.Should().Be((double?)dateOnly.Length() / 2);
//                actualTimeOnly.Should().Be((TimeSpan?)timeOnly.Length() / 2);
//                actualDateTime.Should().Be((TimeSpan?)dateTime.Length() / 2);
//                actualDateTimeOffset.Should().Be((TimeSpan?)dateTimeOffset.Length() / 2);
//            }
//        }

//        [Theory]
//        [InlineData(null, null)]
//        [InlineData(1, null)]
//        [InlineData(null, 1)]
//        public void RadiusInfinity_ShouldBeNull(int? start, int? end)
//        {
//            // Arrange
//            var integer = new Interval<int>(start, end, false, false);
//            var doubles = new Interval<double>(start, end, false, false);

//            var dateOnly = new Interval<DateOnly>(
//                start.HasValue ? _referenceDateOnly.AddDays(start.Value) : null,
//                end.HasValue ? _referenceDateOnly.AddDays(end.Value) : null,
//                false,
//                false);

//            var timeOnly = new Interval<TimeOnly>(
//                start.HasValue ? _referenceTimeOnly.AddHours(start.Value) : null,
//                end.HasValue ? _referenceTimeOnly.AddHours(end.Value) : null,
//                false,
//                false);

//            var dateTime = new Interval<DateTime>(
//                start.HasValue ? _referenceDateTimeOffset.DateTime.AddHours(start.Value) : null,
//                end.HasValue ? _referenceDateTimeOffset.DateTime.AddHours(end.Value) : null,
//                false,
//                false);

//            var dateTimeOffset = new Interval<DateTimeOffset>(
//                start.HasValue ? _referenceDateTimeOffset.AddHours(start.Value) : null,
//                end.HasValue ? _referenceDateTimeOffset.AddHours(end.Value) : null,
//                false,
//                false);

//            // Act
//            var actualInteger = integer.Radius();
//            var actualDouble = doubles.Radius();
//            var actualDateOnly = dateOnly.Radius();
//            var actualTimeOnly = timeOnly.Radius();
//            var actualDateTime = dateTime.Radius();
//            var actualDateTimeOffset = dateTimeOffset.Radius();

//            // Assert
//            using (new AssertionScope())
//            {
//                actualInteger.Should().BeNull();
//                actualDouble.Should().BeNull();
//                actualDateOnly.Should().BeNull();
//                actualTimeOnly.Should().BeNull();
//                actualDateTime.Should().BeNull();
//                actualDateTimeOffset.Should().BeNull();
//            }
//        }

//        [Fact]
//        public void RadiusEmpty_ShouldBeNull()
//        {
//            // Arrange
//            var integer = Interval.Empty<int>();
//            var doubles = new Interval<double>(default, default, false, false);
//            var dateOnly = Interval.Empty<DateOnly>();
//            var timeOnly = Interval.Empty<TimeOnly>();
//            var dateTime = Interval.Empty<DateTime>();
//            var dateTimeOffset = Interval.Empty<DateTimeOffset>();

//            // Act
//            var actualInteger = integer.Radius();
//            var actualDouble = doubles.Radius();
//            var actualDateOnly = dateOnly.Radius();
//            var actualTimeOnly = timeOnly.Radius();
//            var actualDateTime = dateTime.Radius();
//            var actualDateTimeOffset = dateTimeOffset.Radius();

//            // Assert
//            using (new AssertionScope())
//            {
//                actualInteger.Should().BeNull();
//                actualDouble.Should().BeNull();
//                actualDateOnly.Should().BeNull();
//                actualTimeOnly.Should().BeNull();
//                actualDateTime.Should().BeNull();
//                actualDateTimeOffset.Should().BeNull();
//            }
//        }
//    }
//}
