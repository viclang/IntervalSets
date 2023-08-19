using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestHelper;
using System;
using Unbounded;

namespace IntervalRecords.Tests.Measure.Length
{
    public class LengthInt32Tests : LengthTests<int, int> { }
    public class LengthDoubleTests : LengthTests<double, double> { }
    public class LengthDateTimeTests : LengthTests<DateTime, TimeSpan> { }
    public class LengthDateTimeOffsetTests : LengthTests<DateTimeOffset, TimeSpan> { }
    public class LengthDateOnlyTests : LengthTests<DateOnly, int> { }
    public class LengthTimeOnlyTests : LengthTests<TimeOnly, TimeSpan> { }

    public abstract class LengthTests<T, TResult>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
    {
        [Theory]
        [Trait("Measure", "Length")]
        [InlineData(1, 2, IntervalType.Closed, 1)]
        [InlineData(1, 3, IntervalType.Closed, 2)]
        [InlineData(1, 2, IntervalType.ClosedOpen, 1)]
        [InlineData(1, 3, IntervalType.ClosedOpen, 2)]
        [InlineData(1, 2, IntervalType.OpenClosed, 1)]
        [InlineData(1, 3, IntervalType.OpenClosed, 2)]
        [InlineData(1, 2, IntervalType.Open, 1)]
        [InlineData(1, 3, IntervalType.Open, 2)]
        // Unbounded and Halfbound
        [InlineData(null, null, IntervalType.Open, null)]
        [InlineData(null, 2, IntervalType.Open, null)]
        [InlineData(2, null, IntervalType.Open, null)]
        // Singleton
        [InlineData(1, 1, IntervalType.Closed, 0)]
        // Empty
        [InlineData(2, 2, IntervalType.ClosedOpen, 0)]
        [InlineData(2, 2, IntervalType.OpenClosed, 0)]
        [InlineData(2, 2, IntervalType.Open, 0)]
        public void GivenInterval_WhenMeasureLength_ReturnsFiniteOrPositiveInfinity(int? start, int? end, IntervalType intervalType, int? expected)
        {
            // Arrange
            var interval = IntervalFactory.Create<T>(start.ToBoundary<T>(), end.ToBoundary<T>(), intervalType);

            // Act
            var actual = Length(interval);

            // Assert
            actual.Should().Be(FiniteOrPositiveInfinity(expected));
        }

        public Unbounded<TResult> Length(Interval<T> interval)
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (Unbounded<TResult>)(object)IntervalCalculator.Length((Interval<int>)(object)interval),
                TypeCode.Double => (Unbounded<TResult>)(object)IntervalCalculator.Length((Interval<double>)(object)interval),
                TypeCode.DateTime => (Unbounded<TResult>)(object)IntervalCalculator.Length((Interval<DateTime>)(object)interval),
                _ when type == typeof(DateTimeOffset) => (Unbounded<TResult>)(object)IntervalCalculator.Length((Interval<DateTimeOffset>)(object)interval),
                _ when type == typeof(DateOnly) => (Unbounded<TResult>)(object)IntervalCalculator.Length((Interval<DateOnly>)(object)interval),
                _ when type == typeof(TimeOnly) => (Unbounded<TResult>)(object)IntervalCalculator.Length((Interval<TimeOnly>)(object)interval),
                _ => throw new NotSupportedException(type.FullName)
            };
        }


        public static Unbounded<TResult> FiniteOrPositiveInfinity(int? result)
        {
            var type = typeof(T);
            if (type == typeof(int) || type == typeof(DateOnly))
            {
                return (Unbounded<TResult>)(object)UnboundedExtensions.ToPositiveInfinity(result);
            }
            if (type == typeof(double))
            {
                return (Unbounded<TResult>)(object)UnboundedExtensions.ToPositiveInfinity<double>(result);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
            {
                return (Unbounded<TResult>)(object)UnboundedExtensions.ToPositiveInfinity<TimeSpan>(result.HasValue ? TimeSpan.FromDays(result.Value) : null);
            }
            else if (type == typeof(TimeOnly))
            {
                return (Unbounded<TResult>)(object)UnboundedExtensions.ToPositiveInfinity<TimeSpan>(result.HasValue ? TimeSpan.FromHours(result.Value) : null);
            }
            else
            {
                throw new NotSupportedException(type.FullName);
            }
        }
    }
}
