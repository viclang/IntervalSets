using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestHelper;
using System;

namespace IntervalRecords.Tests.Measure.Radius
{
    public class RadiusInt32Tests : RadiusTests<int, double> { }
    public class RadiusDoubleTests : RadiusTests<double, double> { }
    public class RadiusDateTimeTests : RadiusTests<DateTime, TimeSpan> { }
    public class RadiusDateTimeOffsetTests : RadiusTests<DateTimeOffset, TimeSpan> { }
    public class RadiusDateOnlyTests : RadiusTests<DateOnly, int> { }
    public class RadiusTimeOnlyTests : RadiusTests<TimeOnly, TimeSpan> { }

    public abstract class RadiusTests<T, TResult>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
    {
        [Theory]
        [Trait("Measure", "Radius")]
        [InlineData(1, 2, IntervalType.Closed, 0.5)]
        [InlineData(1, 3, IntervalType.Closed, 1d)]
        [InlineData(1, 2, IntervalType.ClosedOpen, 0.5)]
        [InlineData(1, 3, IntervalType.ClosedOpen, 1d)]
        [InlineData(1, 2, IntervalType.OpenClosed, 0.5)]
        [InlineData(1, 3, IntervalType.OpenClosed, 1d)]
        [InlineData(1, 2, IntervalType.Open, 0.5)]
        [InlineData(1, 3, IntervalType.Open, 1d)]
        // Unbounded and Halfbound
        [InlineData(null, null, IntervalType.Open, null)]
        [InlineData(null, 2, IntervalType.Open, null)]
        [InlineData(2, null, IntervalType.Open, null)]
        // Singleton
        [InlineData(1, 1, IntervalType.Closed, 0d)]
        // Empty
        [InlineData(2, 2, IntervalType.ClosedOpen, null)]
        [InlineData(2, 2, IntervalType.OpenClosed, null)]
        [InlineData(2, 2, IntervalType.Open, null)]
        public void GivenBoundedInterval_WhenMeasureRadius_ReturnsExpected(int? start, int? end, IntervalType intervalType, double? expected)
        {
            // Arrange
            var interval = IntervalFactory.Create<T>(start.ToBoundary<T>(), end.ToBoundary<T>(), intervalType);

            // Act
            var actual = Radius(interval);

            // Assert
            actual.Should().Be(RadiusResult(expected));
        }

        public TResult? Radius(Interval<T> interval)
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (TResult?)(object?)IntervalCalculator.Radius((Interval<int>)(object)interval),
                TypeCode.Double => (TResult?)(object?)IntervalCalculator.Radius((Interval<double>)(object)interval),
                TypeCode.DateTime => (TResult?)(object?)IntervalCalculator.Radius((Interval<DateTime>)(object)interval),
                _ when type == typeof(DateTimeOffset) => (TResult?)(object?)IntervalCalculator.Radius((Interval<DateTimeOffset>)(object)interval),
                _ when type == typeof(DateOnly) => (TResult?)(object?)IntervalCalculator.Radius((Interval<DateOnly>)(object)interval),
                _ when type == typeof(TimeOnly) => (TResult?)(object?)IntervalCalculator.Radius((Interval<TimeOnly>)(object)interval),
                _ => throw new NotSupportedException(type.FullName)
            };
        }

        public static TResult? RadiusResult(double? result)
        {
            var type = typeof(T);
            if (type == typeof(int) || type == typeof(double))
            {
                return (TResult?)(object?)result;
            }
            if (type == typeof(DateOnly))
            {
                return (TResult?)(object?)(result is null ? null : (int)result.Value);
            }
            else if (type == typeof(DateTime))
            {
                return (TResult?)(object?)(result is null ? null : TimeSpan.FromDays(result.Value));
            }
            else if (type == typeof(DateTimeOffset))
            {
                return (TResult?)(object?)(result is null ? null : TimeSpan.FromDays(result.Value));
            }
            else if (type == typeof(TimeOnly))
            {
                return (TResult?)(object?)(result is null ? null : TimeSpan.FromHours(result.Value));
            }
            else
            {
                throw new NotSupportedException(type.FullName);
            }
        }
    }
}
