using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData;
using IntervalRecords.Tests.TestData.ClassData;

namespace IntervalRecords.Tests
{
    public class ComparableTests
    {
        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void CompareTo_ShouldBeExpected(IntervalRelationTestData<int> testData)
        {
            // Arrange
            var expected = ExpectedCompareResult(testData.Relation);

            // Act
            var actual = testData.First.CompareTo(testData.Second);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void GreaterThan_ShouldBeExpected(IntervalRelationTestData<int> testData)
        {
            // Arrange
            var expected = ExpectedCompareResult(testData.Relation) > 0;

            // Act
            var actual = testData.First > testData.Second;

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void LessThan_ShouldBeExpected(IntervalRelationTestData<int> testData)
        {
            // Arrange
            var expected = ExpectedCompareResult(testData.Relation) < 0;

            // Act
            var actual = testData.First < testData.Second;

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void GreaterOrEqualTo_ShouldBeExpected(IntervalRelationTestData<int> testData)
        {
            // Arrange
            var expected = ExpectedCompareResult(testData.Relation) >= 0;

            // Act
            var actual = testData.First >= testData.Second;

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void LessOrEqualTo_ShouldBeExpected(IntervalRelationTestData<int> testData)
        {
            // Arrange
            var expected = ExpectedCompareResult(testData.Relation) <= 0;

            // Act
            var actual = testData.First <= testData.Second;

            // Assert
            actual.Should().Be(expected);
        }

        private static int ExpectedCompareResult(IntervalRelation relation)
        {
            return ((int)relation).CompareTo((int)IntervalRelation.Equal);
        }
    }
}
