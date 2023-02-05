using InfinityComparable;
using System;

namespace IntervalRecord.Tests.Calculators
{
    public class CentreTests
    {
        private static readonly Interval<int> _intervalInt32 = Interval.Singleton(1);
        private static readonly Interval<double> _intervalDouble = Interval.Singleton(1d);
        private static readonly Interval<DateTimeOffset> _intervalDateTimeOffset = Interval.Singleton(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero));
        private static readonly Interval<DateTime> _intervalDateTime = Interval.Singleton(new DateTime(2022, 1, 1));
        private static readonly Interval<DateOnly> _intervalDateOnly = Interval.Singleton(new DateOnly(2022, 1, 1));
        private static readonly Interval<TimeOnly> _intervalTimeOnly = Interval.Singleton(new TimeOnly(1, 0));

        public static TheoryData<object, object> BoundedIntervalsWithExpectedLenght = new()
        {
            { _intervalInt32, 1d },
            { _intervalInt32 with { End = 2 }, 1.5 },
            { _intervalInt32 with { End = 3 }, 2d },
            { _intervalDouble, 1d },
            { _intervalDouble with { End = 2 }, 1.5 },
            { _intervalDouble with { End = 3 }, 2d },
            { _intervalDateTime, new DateTime(2022, 1, 1) },
            { _intervalDateTime with { End = _intervalDateTime.End.AddDays(1) }, new DateTime(2022, 1, 1, 12, 0, 0) },
            { _intervalDateTime with { End = _intervalDateTime.End.AddDays(2) }, new DateTime(2022, 1, 2) },
            { _intervalDateTimeOffset, new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero) },
            { _intervalDateTimeOffset with { End = _intervalDateTimeOffset.End.AddDays(1) }, new DateTimeOffset(2022, 1, 1, 12, 0, 0, TimeSpan.Zero) },
            { _intervalDateTimeOffset with { End = _intervalDateTimeOffset.End.AddDays(2) }, new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero) },
            { _intervalDateOnly with { End = _intervalDateOnly.End.AddDays(1) }, new DateOnly(2022, 1, 1) },
            { _intervalDateOnly with { End = _intervalDateOnly.End.AddDays(2) }, new DateOnly(2022, 1, 2) },
            { _intervalTimeOnly with { End = _intervalTimeOnly.End.AddHours(1)}, new TimeOnly(1, 30) },
            { _intervalTimeOnly with { End = _intervalTimeOnly.End.AddHours(2)}, new TimeOnly(2, 0) },
        };

        [Theory]
        [MemberData(nameof(BoundedIntervalsWithExpectedLenght))]
        public void GivenBoundedInterval_WhenMeasureCentre_ReturnsExpected<T, TResult>(Interval<T> interval, TResult? expected)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            // Act
            var actual = Centre<T, TResult?>(interval);

            // Assert
            actual.Should().Be(expected);
        }

        public static TheoryData<object> EmptyOrUnboundedInterval = new()
        {
            Interval.Empty<int>(),
            Interval.Empty<double>(),
            Interval.Empty<DateTime>(),
            Interval.Empty<DateTimeOffset>(),
            Interval.Empty<DateOnly>(),
            Interval.Empty<TimeOnly>(),
            Interval.All<int>(),
            Interval.All<double>(),
            Interval.All<DateTime>(),
            Interval.All<DateTimeOffset>(),
            Interval.All<DateOnly>(),
            Interval.All<TimeOnly>(),
            Interval.GreaterThan(1),
            Interval.GreaterThan(1d),
            Interval.GreaterThan(new DateTime(2022, 1, 1)),
            Interval.GreaterThan(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero)),
            Interval.GreaterThan(new DateOnly(2022, 1, 1)),
            Interval.GreaterThan(new TimeOnly(1, 0)),
            Interval.LessThan(1),
            Interval.LessThan(1d),
            Interval.LessThan(new DateTime(2022, 1, 1)),
            Interval.LessThan(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero)),
            Interval.LessThan(new DateOnly(2022, 1, 1)),
            Interval.LessThan(new TimeOnly(1, 0))
        };

        [Theory]
        [MemberData(nameof(EmptyOrUnboundedInterval))]
        public void GivenEmptyOrUnboundedInterval_WhenMeasureCentre_ReturnsNull<T, TResult>(Interval<T> interval)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            // Act
            var actual = Centre<T, TResult?>(interval);

            // Assert
            actual.Should().BeNull();
        }

        public TResult? Centre<T, TResult>(Interval<T> interval)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (TResult?)(object?)Interval.Centre((Interval<int>)(object)interval),
                TypeCode.Double => (TResult?)(object?)Interval.Centre((Interval<double>)(object)interval),
                TypeCode.DateTime => (TResult?)(object?)Interval.Centre((Interval<DateTime>)(object)interval),
                _ when type == typeof(DateTimeOffset) => (TResult?)(object?)Interval.Centre((Interval<DateTimeOffset>)(object)interval),
                _ when type == typeof(DateOnly) => (TResult?)(object?)Interval.Centre((Interval<DateOnly>)(object)interval),
                _ when type == typeof(TimeOnly) => (TResult?)(object?)Interval.Centre((Interval<TimeOnly>)(object)interval),
                _ => throw new NotSupportedException(type.FullName)
            };
        }
    }
}
