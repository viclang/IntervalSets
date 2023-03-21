using IntervalRecords.Tests.TestData;
using System.Linq;

namespace IntervalRecords.Tests.CombineTests
{
    public class ReduceTests : DataSetTestsBase
    {
        [Theory]
        [InlineData(IntervalType.Closed, 4)]
        [InlineData(IntervalType.ClosedOpen, 4)]
        [InlineData(IntervalType.OpenClosed, 4)]
        [InlineData(IntervalType.Open, 6)]
        public void ReduceUnion_ShouldHaveExpectedCount(IntervalType intervalType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, intervalType);

            // Act
            var actual = list.Reduce((a, b) => a.Union(b)).ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void ReduceEmptyList_ShouldBeEmpty()
        {
            // Arrange
            var list = Enumerable.Empty<Interval<int>>();

            // Act
            var actual = list.Reduce((a, b) => a.Union(b))
                .Concat(list.Reduce((a, b) => a.UnionOrEmpty(b)));

            // Assert
            actual.Should().BeEmpty();
        }
    }
}
