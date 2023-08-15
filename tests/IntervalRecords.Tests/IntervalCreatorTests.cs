using IntervalRecords.Extensions;
using Unbounded;

namespace IntervalRecords.Tests
{
    public class IntervalCreatorTests
    {
        private const int start = 1;
        private const int end = 2;

        public static TheoryData<Interval<int>, Interval<int>> AllBuildersWithExpectedResults = new TheoryData<Interval<int>, Interval<int>>
        {
            { OpenInterval<int>.Empty, new OpenInterval<int>(Unbounded<int>.NaN, Unbounded<int>.NaN) },
            { OpenInterval<int>.Unbounded, new OpenInterval<int>(Unbounded<int>.NegativeInfinity, Unbounded<int>.PositiveInfinity) },
            { ClosedInterval<int>.Singleton(end), new ClosedInterval<int>(end, end) },
            { OpenInterval<int>.LeftBounded(start), new OpenInterval<int>(start, Unbounded<int>.PositiveInfinity) },
            { ClosedOpenInterval<int>.LeftBounded(start), new ClosedOpenInterval<int> (start, Unbounded<int>.PositiveInfinity) },
            { OpenInterval<int>.RightBounded(end), new OpenInterval<int>(Unbounded<int>.NegativeInfinity, end) },
            { OpenClosedInterval<int>.RightBounded(end), new OpenClosedInterval<int>(Unbounded<int>.NegativeInfinity, end) }
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
            var actual = IntervalFactory.Create<int>(start, end, intervalType);

            actual.Should().Be(IntervalFactory.Create<int>(start, end, startInclusive, endInclusive));
        }
    }
}
