using IntervalRecords.Tests.TestData;

namespace IntervalRecords.Tests
{
    public class IntervalOverlappingTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void GetOverlappingState_ShouldBeExpected(Interval<int> first, Interval<int> second, IntervalOverlapping expectedResult)
        {
            var result = first.GetIntervalOverlapping(second);
            result.Should().Be(expectedResult);
        }

    }
}
