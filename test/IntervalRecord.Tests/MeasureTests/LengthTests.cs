using InfinityComparable;
using System;

namespace IntervalRecord.Tests.Calculators
{
    public class LengthTests
    {
        private static readonly Interval<int> _intervalInt32 = Interval.Singleton(1);
        private static readonly Interval<double> _intervalDouble = Interval.Singleton(1d);
        private static readonly Interval<DateTimeOffset> _intervalDateTimeOffset = Interval.Singleton(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero));
        private static readonly Interval<DateTime> _intervalDateTime = Interval.Singleton(new DateTime(2022, 1, 1));
        private static readonly Interval<DateOnly> _intervalDateOnly = Interval.Singleton(new DateOnly(2022, 1, 1));
        private static readonly Interval<TimeOnly> _intervalTimeOnly = Interval.Singleton(new TimeOnly(1, 0));

        public static TheoryData<object, object> BoundedIntervalsWithExpectedLenght = new()
        {
            { _intervalInt32, new Infinity<int>(0) },
            { _intervalInt32 with { End = 2 }, new Infinity<int>(1) },
            { _intervalInt32 with { End = 3 }, new Infinity<int>(2) },
            { _intervalDouble, new Infinity<double>(0) },
            { _intervalDouble with { End = 2 }, new Infinity<double>(1) },
            { _intervalDouble with { End = 3 }, new Infinity<double>(2) },
            { _intervalDateTime, new Infinity<TimeSpan>(TimeSpan.Zero) },
            { _intervalDateTime with { End = _intervalDateTime.End.AddDays(1) }, new Infinity<TimeSpan>(TimeSpan.FromDays(1)) },
            { _intervalDateTime with { End = _intervalDateTime.End.AddDays(2) }, new Infinity<TimeSpan>(TimeSpan.FromDays(2)) },
            { _intervalDateTimeOffset, new Infinity<TimeSpan>(TimeSpan.Zero) },
            { _intervalDateTimeOffset with { End = _intervalDateTimeOffset.End.AddDays(1) }, new Infinity<TimeSpan>(TimeSpan.FromDays(1)) },
            { _intervalDateTimeOffset with { End = _intervalDateTimeOffset.End.AddDays(2) }, new Infinity<TimeSpan>(TimeSpan.FromDays(2)) },
            { _intervalDateOnly, new Infinity<int>(0) },
            { _intervalDateOnly with { End = _intervalDateOnly.End.AddDays(1) }, new Infinity<int>(1) },
            { _intervalDateOnly with { End = _intervalDateOnly.End.AddDays(2) }, new Infinity<int>(2) },
            { _intervalTimeOnly, new Infinity<TimeSpan>(TimeSpan.Zero) },
            { _intervalTimeOnly with { End = _intervalTimeOnly.End.AddHours(1)}, new Infinity<TimeSpan>(TimeSpan.FromHours(1)) },
            { _intervalTimeOnly with { End = _intervalTimeOnly.End.AddHours(2)}, new Infinity<TimeSpan>(TimeSpan.FromHours(2)) },
        };

        [Theory]
        [MemberData(nameof(BoundedIntervalsWithExpectedLenght))]
        public void GivenBoundedInterval_WhenMeasureLength_ReturnsExpected<T, TResult>(Interval<T> interval, Infinity<TResult> expected)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            // Act
            var actual = Length<T, TResult>(interval);

            // Assert
            actual.Should().Be(expected);
        }

        public static TheoryData<object, object> EmptyOrSingletonIntervalWithZeroLength = new()
        {
            { Interval.Empty<int>(), new Infinity<int>(0) },
            { Interval.Empty<double>(), new Infinity<double>(0) },
            { Interval.Empty<DateTime>(), new Infinity<TimeSpan>(TimeSpan.Zero) },
            { Interval.Empty<DateTimeOffset>(), new Infinity<TimeSpan>(TimeSpan.Zero) },
            { Interval.Empty<DateOnly>(), new Infinity<int>(0) },
            { Interval.Empty<TimeOnly>(), new Infinity<TimeSpan>(TimeSpan.Zero) }
        };

        [Theory]
        [MemberData(nameof(EmptyOrSingletonIntervalWithZeroLength))]
        public void GivenEmptyInterval_WhenMeasureLength_ReturnsZero<T, TResult>(Interval<T> interval, Infinity<TResult> expected)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            // Act
            var actual = Length<T, TResult>(interval);

            // Assert
            actual.Should().Be(expected);
        }

        public static TheoryData<object, object> UnboundedOrHalfBoundedIntervalWithInfinityLength = new()
        {
            { Interval.All<int>(), Infinity<int>.PositiveInfinity },
            { Interval.All<double>(), Infinity<double>.PositiveInfinity },
            { Interval.All<DateTime>(), Infinity<TimeSpan>.PositiveInfinity },
            { Interval.All<DateTimeOffset>(), Infinity<TimeSpan>.PositiveInfinity },
            { Interval.All<DateOnly>(), Infinity<int>.PositiveInfinity },
            { Interval.All<TimeOnly>(), Infinity<TimeSpan>.PositiveInfinity },
            { Interval.GreaterThan(1), Infinity<int>.PositiveInfinity },
            { Interval.GreaterThan(1d), Infinity<double>.PositiveInfinity },
            { Interval.GreaterThan(new DateTime(2022, 1, 1)), Infinity<TimeSpan>.PositiveInfinity },
            { Interval.GreaterThan(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero)), Infinity<TimeSpan>.PositiveInfinity },
            { Interval.GreaterThan(new DateOnly(2022, 1, 1)), Infinity<int>.PositiveInfinity },
            { Interval.GreaterThan(new TimeOnly(1, 0)), Infinity<TimeSpan>.PositiveInfinity },
            { Interval.LessThan(1), Infinity<int>.PositiveInfinity },
            { Interval.LessThan(1d), Infinity<double>.PositiveInfinity },
            { Interval.LessThan(new DateTime(2022, 1, 1)), Infinity<TimeSpan>.PositiveInfinity },
            { Interval.LessThan(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero)), Infinity<TimeSpan>.PositiveInfinity },
            { Interval.LessThan(new DateOnly(2022, 1, 1)), Infinity<int>.PositiveInfinity },
            { Interval.LessThan(new TimeOnly(1, 0)), Infinity<TimeSpan>.PositiveInfinity },
        };

        [Theory]
        [MemberData(nameof(UnboundedOrHalfBoundedIntervalWithInfinityLength))]
        public void GivenUnboundedOrHalfBoundedInterval_WhenMeasureLength_ReturnsInfinity<T, TResult>(Interval<T> interval, Infinity<TResult> expected)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            // Act
            var actual = Length<T, TResult>(interval);

            // Assert
            actual.Should().Be(expected);
        }

        public Infinity<TResult> Length<T, TResult>(Interval<T> interval)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (Infinity<TResult>)(object)Interval.Length((Interval<int>)(object)interval),
                TypeCode.Double => (Infinity<TResult>)(object)Interval.Length((Interval<double>)(object)interval),
                TypeCode.DateTime => (Infinity<TResult>)(object)Interval.Length((Interval<DateTime>)(object)interval),
                _ when type == typeof(DateTimeOffset) => (Infinity<TResult>)(object)Interval.Length((Interval<DateTimeOffset>)(object)interval),
                _ when type == typeof(DateOnly) => (Infinity<TResult>)(object)Interval.Length((Interval<DateOnly>)(object)interval),
                _ when type == typeof(TimeOnly) => (Infinity<TResult>)(object)Interval.Length((Interval<TimeOnly>)(object)interval),
                _ => throw new NotSupportedException(type.FullName)
            };
        }
    }
}
