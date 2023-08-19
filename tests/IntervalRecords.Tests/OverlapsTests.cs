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
            var actual = testData.First.Overlaps(testData.Second);

            actual.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(Int32NonOverlappingClassData))]
        public void GivenTwoNonOverlappingIntervals_WhenComparing_ReturnsFalse(IntervalRelationTestData<int> testData)
        {
            var actual = testData.First.Overlaps(testData.Second);

            actual.Should().BeFalse();
        }
    }
}
