using IntervalRecords.Tests.TestHelper;
using System;

namespace IntervalRecords.Tests.CanonicalizeTests
{
    public class CanonicalizeInt32Tests : CanonicalizeTestsBase<int, int> { }

    public class CanonicalizeDoubleTests : CanonicalizeTestsBase<double, double> { }

    public class CanonicalizeDateTimeTests : CanonicalizeTestsBase<DateTime, TimeSpan> { }

    public class CanonicalizeDateTimeOffsetTests : CanonicalizeTestsBase<DateTimeOffset, TimeSpan> { }

    public class CanonicalizeDateOnlyTests : CanonicalizeTestsBase<DateOnly, int> { }

    public class CanonicalizeTimeOnlyTests : CanonicalizeTestsBase<TimeOnly, TimeSpan> { }

    public abstract class CanonicalizeTestsBase<T, TStep>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TStep : struct
    {
        [Theory]
        [Trait("Canonicalize", "Canonicalize")]
        [InlineData(2, 4, IntervalType.ClosedOpen, 2, 3, IntervalType.Closed)]
        [InlineData(2, 4, IntervalType.OpenClosed, 3, 4, IntervalType.Closed)]
        [InlineData(2, 4, IntervalType.Open, 3, 3, IntervalType.Closed)]
        [InlineData(2, 4, IntervalType.Closed, 2, 5, IntervalType.ClosedOpen)]
        [InlineData(2, 4, IntervalType.OpenClosed, 3, 5, IntervalType.ClosedOpen)]
        [InlineData(2, 4, IntervalType.Open, 3, 4, IntervalType.ClosedOpen)]
        [InlineData(2, 4, IntervalType.Closed, 1, 4, IntervalType.OpenClosed)]
        [InlineData(2, 4, IntervalType.ClosedOpen, 1, 3, IntervalType.OpenClosed)]
        [InlineData(2, 4, IntervalType.Open, 2, 3, IntervalType.OpenClosed)]
        [InlineData(2, 4, IntervalType.Closed, 1, 5, IntervalType.Open)]
        [InlineData(2, 4, IntervalType.ClosedOpen, 1, 4, IntervalType.Open)]
        [InlineData(2, 4, IntervalType.OpenClosed, 2, 5, IntervalType.Open)]
        public void GivenBoundedInterval_WhenCanonicalize_ReturnsExpected(int? start, int? end, IntervalType intervalType, int? expectedStart, int? expectedEnd, IntervalType expectedIntervalType)
        {
            // Arrange
            var interval = Interval.WithIntervalType<T>(start.ToBoundary<T>(), end.ToBoundary<T>(), intervalType);

            // Act
            var actual = Canonicalize(interval, expectedIntervalType, 1.ToStep<T, TStep>());

            // Assert
            actual.Should()
                .Be(Interval.WithIntervalType<T>(expectedStart.ToBoundary<T>(), expectedEnd.ToBoundary<T>(), expectedIntervalType));
        }

        [Theory]
        [Trait("Canonicalize", "Closure")]
        // Bounded
        [InlineData(2, 4, IntervalType.ClosedOpen, 2, 3, IntervalType.Closed)]
        [InlineData(2, 4, IntervalType.OpenClosed, 3, 4, IntervalType.Closed)]
        [InlineData(2, 4, IntervalType.Open, 3, 3, IntervalType.Closed)]
        // Unbounded and Halfbound
        [InlineData(null, null, IntervalType.Open, null, null, IntervalType.Open)]
        [InlineData(null, 2, IntervalType.Open, null, 1, IntervalType.OpenClosed)]
        [InlineData(2, null, IntervalType.Open, 3, null, IntervalType.ClosedOpen)]
        // Empty
        [InlineData(2, 2, IntervalType.ClosedOpen, 2, 2, IntervalType.ClosedOpen)]
        [InlineData(2, 2, IntervalType.OpenClosed, 2, 2, IntervalType.OpenClosed)]
        [InlineData(2, 2, IntervalType.Open, 2, 2, IntervalType.Open)]
        public void GivenInterval_WhenUsingClosure_ReturnsExpected(int? start, int? end, IntervalType intervalType, int? expectedStart, int? expectedEnd, IntervalType expectedIntervalType)
        {
            // Arrange
            var interval = Interval.WithIntervalType<T>(start.ToBoundary<T>(), end.ToBoundary<T>(), intervalType);

            // Act
            var actual = Closure(interval, 1.ToStep<T, TStep>());

            // Assert
            actual.Should()
                .Be(Interval.WithIntervalType<T>(expectedStart.ToBoundary<T>(), expectedEnd.ToBoundary<T>(), expectedIntervalType));
        }

        [Theory]
        [Trait("Canonicalize", "Interior")]
        // Bounded
        [InlineData(2, 4, IntervalType.Closed, 1, 5, IntervalType.Open)]
        [InlineData(2, 4, IntervalType.ClosedOpen, 1, 4, IntervalType.Open)]
        [InlineData(2, 4, IntervalType.OpenClosed, 2, 5, IntervalType.Open)]
        // Unbounded and Halfbound
        [InlineData(null, null, IntervalType.Open, null, null, IntervalType.Open)]
        [InlineData(null, 2, IntervalType.OpenClosed, null, 3, IntervalType.Open)]
        [InlineData(2, null, IntervalType.ClosedOpen, 1, null, IntervalType.Open)]
        // Empty
        [InlineData(2, 2, IntervalType.ClosedOpen, 2, 2, IntervalType.ClosedOpen)]
        [InlineData(2, 2, IntervalType.OpenClosed, 2, 2, IntervalType.OpenClosed)]
        public void GivenInterval_WhenUsingInterior_ReturnsExpected(int? start, int? end, IntervalType intervalType, int? expectedStart, int? expectedEnd, IntervalType expectedIntervalType)
        {
            // Arrange
            var interval = Interval.WithIntervalType<T>(start.ToBoundary<T>(), end.ToBoundary<T>(), intervalType);

            // Act
            var actual = Interior(interval, 1.ToStep<T, TStep>());

            // Assert
            actual.Should()
                .Be(Interval.WithIntervalType<T>(expectedStart.ToBoundary<T>(), expectedEnd.ToBoundary<T>(), expectedIntervalType));
        }

        public Interval<T> Canonicalize(Interval<T> interval, IntervalType intervalType, TStep step)
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (Interval<T>)(object)((Interval<int>)(object)interval).Canonicalize(intervalType, (int)(object)step),
                TypeCode.Double => (Interval<T>)(object)((Interval<double>)(object)interval).Canonicalize(intervalType, (double)(object)step),
                TypeCode.DateTime => (Interval<T>)(object)((Interval<DateTime>)(object)interval).Canonicalize(intervalType, (TimeSpan)(object)step),
                _ when type == typeof(DateTimeOffset) => (Interval<T>)(object)((Interval<DateTimeOffset>)(object)interval).Canonicalize(intervalType, (TimeSpan)(object)step),
                _ when type == typeof(DateOnly) => (Interval<T>)(object)((Interval<DateOnly>)(object)interval).Canonicalize(intervalType, (int)(object)step),
                _ when type == typeof(TimeOnly) => (Interval<T>)(object)((Interval<TimeOnly>)(object)interval).Canonicalize(intervalType, (TimeSpan)(object)step),
                _ => throw new NotSupportedException(type.FullName)
            };
        }

        public Interval<T> Closure(Interval<T> interval, TStep step)
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (Interval<T>)(object)((Interval<int>)(object)interval).Closure((int)(object)step),
                TypeCode.Double => (Interval<T>)(object)((Interval<double>)(object)interval).Closure((double)(object)step),
                TypeCode.DateTime => (Interval<T>)(object)((Interval<DateTime>)(object)interval).Closure((TimeSpan)(object)step),
                _ when type == typeof(DateTimeOffset) => (Interval<T>)(object)((Interval<DateTimeOffset>)(object)interval).Closure((TimeSpan)(object)step),
                _ when type == typeof(DateOnly) => (Interval<T>)(object)((Interval<DateOnly>)(object)interval).Closure((int)(object)step),
                _ when type == typeof(TimeOnly) => (Interval<T>)(object)((Interval<TimeOnly>)(object)interval).Closure((TimeSpan)(object)step),
                _ => throw new NotSupportedException(type.FullName)
            };
        }

        public Interval<T> Interior(Interval<T> interval, TStep step)
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (Interval<T>)(object)((Interval<int>)(object)interval).Interior((int)(object)step),
                TypeCode.Double => (Interval<T>)(object)((Interval<double>)(object)interval).Interior((double)(object)step),
                TypeCode.DateTime => (Interval<T>)(object)((Interval<DateTime>)(object)interval).Interior((TimeSpan)(object)step),
                _ when type == typeof(DateTimeOffset) => (Interval<T>)(object)((Interval<DateTimeOffset>)(object)interval).Interior((TimeSpan)(object)step),
                _ when type == typeof(DateOnly) => (Interval<T>)(object)((Interval<DateOnly>)(object)interval).Interior((int)(object)step),
                _ when type == typeof(TimeOnly) => (Interval<T>)(object)((Interval<TimeOnly>)(object)interval).Interior((TimeSpan)(object)step),
                _ => throw new NotSupportedException(type.FullName)
            };
        }
    }
}
