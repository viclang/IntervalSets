using IntervalRecords.Tests.TestData;
using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData.ClassData;

namespace IntervalRecords.Tests
{
    public class IntervalRelationTests
    {
        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void GetOverlappingState_ShouldBeExpected(IntervalRelationTestData<int> testData)
        {
            var result = testData.Left.GetRelation(testData.Right);
            result.Should().Be(testData.Relation);
        }

    }
}
