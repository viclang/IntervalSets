using IntervalRecords.Tests.TestData;

namespace IntervalRecords.Tests
{
    public class OverlapsTests
    {
        [Theory]
        [ClassData(typeof(Int32OverlapClassData))]
        public void Overlaps(OverlapTestData<int> testData)
        {
            // Arrange
            var expectedResult = testData.Overlap is not IntervalOverlapping.Before
                and not IntervalOverlapping.After;

            // Act
            var actual = testData.First.Overlaps(testData.Second);

            // Assert
            actual.Should().Be(expectedResult);
        }
    }
}
