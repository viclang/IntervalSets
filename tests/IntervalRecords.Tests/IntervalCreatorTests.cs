using IntervalRecords.Types;
using Unbounded;

namespace IntervalRecords.Tests
{
    public class IntervalCreatorTests
    {
        private const int start = 1;
        private const int end = 2;

        public static TheoryData<Interval<int>, Interval<int>> AllBuildersWithExpectedResults = new TheoryData<Interval<int>, Interval<int>>
        {
            { Interval.Empty<int>(), new OpenInterval<int>(0, 0) },
            { Interval.All<int>(), new OpenInterval<int>(Unbounded<int>.NegativeInfinity, Unbounded<int>.PositiveInfinity) },
            { Interval.Singleton(end), new ClosedInterval<int>(end, end) },
            { Interval.Closed(start, end), new ClosedInterval<int>(start, end) },
            { Interval.ClosedOpen(start, end), new ClosedOpenInterval<int>(start, end) },
            { Interval.OpenClosed(start, end), new OpenClosedInterval<int>(start, end) },
            { Interval.Open(start, end), new OpenInterval<int>(start, end) },
            { Interval.GreaterThan(start), new OpenInterval<int>(start, Unbounded<int>.PositiveInfinity) },
            { Interval.AtLeast(start), new ClosedOpenInterval<int> (start, Unbounded<int>.PositiveInfinity) },
            { Interval.LessThan(end), new OpenInterval<int>(Unbounded<int>.NegativeInfinity, end) },
            { Interval.AtMost(end), new OpenClosedInterval<int>(Unbounded<int>.NegativeInfinity, end) }
        };

        [Theory]
        [MemberData(nameof(AllBuildersWithExpectedResults))]
        public void BuilderShouldBeEqualToExpectedResult(Interval<int> result, Interval<int> expectedResult)
        {
            result.Should().Be(expectedResult);
        }


        [Theory]
        [InlineData(1, 3, IntervalType.Closed, true, true)]
        [InlineData(2, 4, IntervalType.Closed, true, true)]
        [InlineData(1, 3, IntervalType.ClosedOpen, true, false)]
        [InlineData(2, 4, IntervalType.ClosedOpen, true, false)]
        [InlineData(1, 3, IntervalType.OpenClosed, false, true)]
        [InlineData(2, 4, IntervalType.OpenClosed, false, true)]
        [InlineData(1, 3, IntervalType.Open, false, false)]
        [InlineData(2, 4, IntervalType.Open, false, false)]
        public void IntervalWithIntervalType_ReturnsExpected(
            int start,
            int end,
            IntervalType intervalType,
            bool startInclusive,
            bool endInclusive)
        {
            var actual = Interval.WithIntervalType<int>(start, end, intervalType);

            actual.Should().Be(Interval.CreateInterval<int>(start, end, startInclusive, endInclusive));
        }
    }
}
