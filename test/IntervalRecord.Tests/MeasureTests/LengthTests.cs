using FluentAssertions.Execution;
using InfinityComparable;
using IntervalRecord.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.Calculators
{
    public class LengthTests
    {
        private static readonly DateTimeOffset _referenceDateTimeOffset = new(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private static readonly DateOnly _referenceDateOnly = new(_referenceDateTimeOffset.Year, _referenceDateTimeOffset.Month, _referenceDateTimeOffset.Day);
        private static readonly TimeOnly _referenceTimeOnly = new(_referenceDateTimeOffset.Hour, _referenceDateTimeOffset.Minute, _referenceDateTimeOffset.Second);

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(1, 5)]
        [InlineData(1, 6)]
        public void GivenBoundedInterval_WhenMeasureLength_ReturnsExpected<T, TResult>(Interval<T> interval, Interval<TResult> expected)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            // Act
            var actual = Length<T, TResult>(interval);

            // Assert
            actual.Should().BeEquivalentTo(expected);
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
            var actualInteger = integer.Length();
            var actualDouble = doubles.Length();
            var actualDateOnly = dateOnly.Length();
            var actualTimeOnly = timeOnly.Length();
            var actualDateTime = dateTime.Length();
            var actualDateTimeOffset = dateTimeOffset.Length();

            // Assert
            using (new AssertionScope())
            {
                actualInteger.Should().Be(Infinity<int>.PositiveInfinity);
                actualDouble.Should().Be(Infinity<double>.PositiveInfinity);
                actualDateOnly.Should().Be(Infinity<int>.PositiveInfinity);
                actualTimeOnly.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
                actualDateTime.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
                actualDateTimeOffset.Should().Be(Infinity<TimeSpan>.PositiveInfinity);
            }
        }

        public Infinity<T> Length<T, TResult>(Interval<T> interval)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (Infinity<T>)(object)Interval.Length((Interval<int>)(object)interval),
                TypeCode.Double => (Infinity<T>)(object)Interval.Length((Interval<double>)(object)interval),
                TypeCode.DateTime => (Infinity<T>)(object)Interval.Length((Interval<DateTime>)(object)interval),
                _ when type == typeof(DateTimeOffset) => (Infinity<T>)(object)Interval.Length((Interval<DateTimeOffset>)(object)interval),
                _ when type == typeof(DateOnly) => (Infinity<T>)(object)Interval.Length((Interval<DateOnly>)(object)interval),
                _ when type == typeof(TimeOnly) => (Infinity<T>)(object)Interval.Length((Interval<TimeOnly>)(object)interval),
                _ => throw new NotSupportedException()
            };
        }
    }
}
