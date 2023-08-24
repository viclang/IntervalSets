using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData;
using IntervalRecords.Tests.TestData.ClassData;

namespace IntervalRecords.Tests
{
    public class OverlapsTests
    {
        [Theory]
        [ClassData(typeof(Int32OverlappingClassData))]
        public void GivenTwoOverlappingIntervals_WhenComparing_ReturnsTrue(IntervalRelationTestData<int> testData)
        {
            var actual = testData.Left.Overlaps(testData.Right);

            actual.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(Int32NonOverlappingClassData))]
        public void GivenTwoNonOverlappingIntervals_WhenComparing_ReturnsFalse(IntervalRelationTestData<int> testData)
        {
            var actual = testData.Left.Overlaps(testData.Right);

            actual.Should().BeFalse();
        }
    }
}
