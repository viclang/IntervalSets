using System;

namespace IntervalRecord.Tests.Canonicalizers
{
    public class CanonicalizeTests
    {
        public static TheoryData<object, IntervalType, object, object> BoundedIntervalWithExpectedResult = new()
        {
            { Interval.Open(2, 4), IntervalType.Closed, 1, Interval.Closed(3, 3) },
            { Interval.OpenClosed(2, 4), IntervalType.ClosedOpen, 1, Interval.ClosedOpen(3, 5) },
            { Interval.ClosedOpen(2, 4), IntervalType.OpenClosed, 1, Interval.OpenClosed(1, 3) },
            { Interval.Closed(2, 4), IntervalType.Open, 1, Interval.Open(1, 5) },

            { Interval.Open(2d, 4d), IntervalType.Closed, 1d, Interval.Closed(3d, 3d) },
            { Interval.OpenClosed(2d, 4d), IntervalType.ClosedOpen, 1d, Interval.ClosedOpen(3d, 5d) },
            { Interval.ClosedOpen(2d, 4d), IntervalType.OpenClosed, 1d, Interval.OpenClosed(1d, 3d) },
            { Interval.Closed(2d, 4d), IntervalType.Open, 1d, Interval.Open(1d, 5d) },

            { Interval.Open(new DateTime(2022, 1, 2), new DateTime(2022, 1, 4)), IntervalType.Closed, TimeSpan.FromDays(1), Interval.Closed(new DateTime(2022, 1, 3), new DateTime(2022, 1, 3)) },
            { Interval.OpenClosed(new DateTime(2022, 1, 2), new DateTime(2022, 1, 4)), IntervalType.ClosedOpen, TimeSpan.FromDays(1), Interval.ClosedOpen(new DateTime(2022, 1, 3), new DateTime(2022, 1, 5)) },
            { Interval.ClosedOpen(new DateTime(2022, 1, 2), new DateTime(2022, 1, 4)), IntervalType.OpenClosed, TimeSpan.FromDays(1), Interval.OpenClosed(new DateTime(2022, 1, 1), new DateTime(2022, 1, 3)) },
            { Interval.Closed(new DateTime(2022, 1, 2), new DateTime(2022, 1, 4)), IntervalType.Open, TimeSpan.FromDays(1), Interval.Open(new DateTime(2022, 1, 1), new DateTime(2022, 1, 5)) },

            { Interval.Open(new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 4, 0, 0, 0, TimeSpan.Zero)), IntervalType.Closed, TimeSpan.FromDays(1), Interval.Closed(new DateTimeOffset(2022, 1, 3, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 3, 0, 0, 0, TimeSpan.Zero)) },
            { Interval.OpenClosed(new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 4, 0, 0, 0, TimeSpan.Zero)), IntervalType.ClosedOpen, TimeSpan.FromDays(1), Interval.ClosedOpen(new DateTimeOffset(2022, 1, 3, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 5, 0, 0, 0, TimeSpan.Zero)) },
            { Interval.ClosedOpen(new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 4, 0, 0, 0, TimeSpan.Zero)), IntervalType.OpenClosed, TimeSpan.FromDays(1), Interval.OpenClosed(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 3, 0, 0, 0, TimeSpan.Zero)) },
            { Interval.Closed(new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 4, 0, 0, 0, TimeSpan.Zero)), IntervalType.Open, TimeSpan.FromDays(1), Interval.Open(new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero), new DateTimeOffset(2022, 1, 5, 0, 0, 0, TimeSpan.Zero)) },

            { Interval.Open(new DateOnly(2022, 1, 2), new DateOnly(2022, 1, 4)), IntervalType.Closed, 1, Interval.Closed(new DateOnly(2022, 1, 3), new DateOnly(2022, 1, 3)) },
            { Interval.OpenClosed(new DateOnly(2022, 1, 2), new DateOnly(2022, 1, 4)), IntervalType.ClosedOpen, 1, Interval.ClosedOpen(new DateOnly(2022, 1, 3), new DateOnly(2022, 1, 5)) },
            { Interval.ClosedOpen(new DateOnly(2022, 1, 2), new DateOnly(2022, 1, 4)), IntervalType.OpenClosed, 1, Interval.OpenClosed(new DateOnly(2022, 1, 1), new DateOnly(2022, 1, 3)) },
            { Interval.Closed(new DateOnly(2022, 1, 2), new DateOnly(2022, 1, 4)), IntervalType.Open, 1, Interval.Open(new DateOnly(2022, 1, 1), new DateOnly(2022, 1, 5)) },

            { Interval.Open(new TimeOnly(2, 0), new TimeOnly(4, 0)), IntervalType.Closed, TimeSpan.FromHours(1), Interval.Closed(new TimeOnly(1, 0), new TimeOnly(3, 0)) },
            { Interval.OpenClosed(new TimeOnly(2, 0), new TimeOnly(4, 0)), IntervalType.ClosedOpen, TimeSpan.FromHours(1), Interval.ClosedOpen(new TimeOnly(3, 0), new TimeOnly(5, 0)) },
            { Interval.ClosedOpen(new TimeOnly(2, 0), new TimeOnly(4, 0)), IntervalType.OpenClosed, TimeSpan.FromHours(1), Interval.OpenClosed(new TimeOnly(1, 0), new TimeOnly(3, 0)) },
            { Interval.Closed(new TimeOnly(2, 0), new TimeOnly(4, 0)), IntervalType.Open, TimeSpan.FromHours(1), Interval.Open(new TimeOnly(1, 0), new TimeOnly(5, 0)) },
        };
        /*
        [InlineData(IntervalType.ClosedOpen, IntervalType.Closed)]
        [InlineData(IntervalType.OpenClosed, IntervalType.Closed)]
        [InlineData(IntervalType.Open, IntervalType.Closed)]

        [InlineData(IntervalType.Closed, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.Open, IntervalType.ClosedOpen)]

        [InlineData(IntervalType.Closed, IntervalType.OpenClosed)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.OpenClosed)]
        [InlineData(IntervalType.Open, IntervalType.OpenClosed)]

        [InlineData(IntervalType.Closed, IntervalType.Open)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.Open)]
        [InlineData(IntervalType.OpenClosed, IntervalType.Open)]
         */
        [Theory]
        [MemberData(nameof(BoundedIntervalWithExpectedResult))]
        public void GivenBoundedInterval_WhenCanonicalize_ReturnsExpected<T, TStep>(Interval<T> interval, IntervalType intervalType, TStep step, Interval<T> expected)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TStep : struct
        {
            // Act
            var actual = Canonicalize(interval, intervalType, step);

            // Assert
            actual.Should().Be(expected);
        }


        public Interval<T> Canonicalize<T, TStep>(Interval<T> interval, IntervalType intervalType, TStep step)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TStep : struct
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (Interval<T>)(object)Interval.Canonicalize((Interval<int>)(object)interval, intervalType, (int)(object)step),
                TypeCode.Double => (Interval<T>)(object)Interval.Canonicalize((Interval<double>)(object)interval, intervalType, (double)(object)step),
                TypeCode.DateTime => (Interval<T>)(object)Interval.Canonicalize((Interval<DateTime>)(object)interval, intervalType, (TimeSpan)(object)step),
                _ when type == typeof(DateTimeOffset) => (Interval<T>)(object)Interval.Canonicalize((Interval<DateTimeOffset>)(object)interval, intervalType, (TimeSpan)(object)step),
                _ when type == typeof(DateOnly) => (Interval<T>)(object)Interval.Canonicalize((Interval<DateOnly>)(object)interval, intervalType, (int)(object)step),
                _ when type == typeof(TimeOnly) => (Interval<T>)(object)Interval.Canonicalize((Interval<TimeOnly>)(object)interval, intervalType, (TimeSpan)(object)step),
                _ => throw new NotSupportedException(type.FullName)
            };
        }
    }
}
