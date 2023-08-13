using IntervalRecords.Tests.TestHelper;
using System;

namespace IntervalRecords.Tests.Measure.Centre
{
    public class CentreInt32Tests : CentreTests<int, double> { }
    public class CentreDoubleTests : CentreTests<double, double> { }
    public class CentreDateTimeTests : CentreTests<DateTime, DateTime> { }
    public class CentreDateTimeOffsetTests : CentreTests<DateTimeOffset, DateTimeOffset> { }
    public class CentreDateOnlyTests : CentreTests<DateOnly, DateOnly> { }
    public class CentreTimeOnlyTests : CentreTests<TimeOnly, TimeOnly> { }

    public abstract class CentreTests<T, TResult>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
    {
        private static readonly DateTimeOffset _referenceDate = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);

        [Theory]
        [Trait("Measure", "Centre")]
        [InlineData(1, 2, IntervalType.Closed, 1.5)]
        [InlineData(1, 3, IntervalType.Closed, 2d)]
        [InlineData(1, 2, IntervalType.ClosedOpen, 1.5)]
        [InlineData(1, 3, IntervalType.ClosedOpen, 2d)]
        [InlineData(1, 2, IntervalType.OpenClosed, 1.5)]
        [InlineData(1, 3, IntervalType.OpenClosed, 2d)]
        [InlineData(1, 2, IntervalType.Open, 1.5)]
        [InlineData(1, 3, IntervalType.Open, 2d)]
        // Unbounded and Halfbound
        [InlineData(null, null, IntervalType.Open, null)]
        [InlineData(null, 2, IntervalType.Open, null)]
        [InlineData(2, null, IntervalType.Open, null)]
        // Singleton
        [InlineData(1, 1, IntervalType.Closed, 1d)]
        // Empty
        [InlineData(2, 2, IntervalType.ClosedOpen, null)]
        [InlineData(2, 2, IntervalType.OpenClosed, null)]
        [InlineData(2, 2, IntervalType.Open, null)]
        public void GivenBoundedInterval_WhenMeasureCentre_ReturnsExpected(int? start, int? end, IntervalType intervalType, double? expected)
        {
            // Arrange
            var interval = Interval.Create<T>(start.ToBoundary<T>(), end.ToBoundary<T>(), intervalType);

            // Act
            var actual = Centre(interval);

            // Assert
            actual.Should().Be(CentreResult(expected));
        }

        public TResult? Centre(Interval<T> interval)
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

        public static TResult? CentreResult(double? result)
        {
            var type = typeof(T);
            if (type == typeof(int) || type == typeof(double))
            {
                return (TResult?)(object?)result;
            }
            if (type == typeof(DateOnly))
            {
                return (TResult?)(object?)(result is null ? null : new DateOnly(2022, 1, (int)result.Value));
            }
            else if (type == typeof(DateTime))
            {
                return (TResult?)(object?)(result is null ? null : _referenceDate.DateTime.AddDays(result.Value - 1));
            }
            else if (type == typeof(DateTimeOffset))
            {
                return (TResult?)(object?)(result is null ? null : _referenceDate.AddDays(result.Value - 1));
            }
            else if (type == typeof(TimeOnly))
            {
                return (TResult?)(object?)(result is null ? null : TimeOnly.FromTimeSpan(TimeSpan.FromHours(result.Value)));
            }
            else
            {
                throw new NotSupportedException(type.FullName);
            }
        }
    }
}
