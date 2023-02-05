namespace IntervalRecord.Tests
{
    public class IntervalCreatorTests
    {
        private const int start = 1;
        private const int end = 2;

        public static TheoryData<Interval<int>, Interval<int>> AllBuildersWithExpectedResults = new TheoryData<Interval<int>, Interval<int>>
        {
            { Interval.Empty<int>(), new(0, 0, false, false) },
            { Interval.All<int>(), new(null, null, false, false) },
            { Interval.Singleton(end), new(end, end, true, true) },
            { Interval.Closed(start, end), new(start, end, true, true) },
            { Interval.ClosedOpen(start, end), new(start, end, true, false) },
            { Interval.OpenClosed(start, end), new(start, end, false, true) },
            { Interval.Open(start, end), new(start, end, false, false) },
            { Interval.GreaterThan(start), new(start, null, false, false) },
            { Interval.AtLeast(start), new(start, null, true, false) },
            { Interval.LessThan(end), new(null, end, false, false) },
            { Interval.AtMost(end), new(null, end, false, true) }
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

            actual.Should().Be(new(start, end, startInclusive, endInclusive));
        }
    }
}
