using IntervalRecords.Tests.TestData;

namespace IntervalRecords.Tests
{
    public class IntervalOverlappingTests : DataSetTestsBase
    {
        [Theory]
        [ClassData(typeof(OverlapInt32ClassData))]
        public void GetOverlappingState_ShouldBeExpected(OverlapTestData<int> testData)
        {
            var result = testData.First.GetOverlap(testData.Second);
            result.Should().Be(testData.Overlap);
        }

    }
}
