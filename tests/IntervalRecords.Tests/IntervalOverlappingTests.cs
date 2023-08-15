using IntervalRecords.Tests.TestData;
using IntervalRecords.Extensions;

namespace IntervalRecords.Tests
{
    public class IntervalOverlappingTests
    {
        [Theory]
        [ClassData(typeof(Int32OverlapClassData))]
        public void GetOverlappingState_ShouldBeExpected(OverlapTestData<int> testData)
        {
            var result = testData.First.GetOverlap(testData.Second);
            result.Should().Be(testData.Overlap);
        }

    }
}
