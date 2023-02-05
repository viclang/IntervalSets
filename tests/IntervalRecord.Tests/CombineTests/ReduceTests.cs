using IntervalRecord.Tests.TestData;
using System.Linq;

namespace IntervalRecord.Tests.CombineTests
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

        [Theory]
        [InlineData(IntervalType.Closed)]
        [InlineData(IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed)]
        [InlineData(IntervalType.Open)]
        public void ReduceUnionOrDefault_ShouldHaveExpectedCount(IntervalType intervalType)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, intervalType);

            // Act
            var actual = list.Reduce((a, b) => a.UnionOrDefault(b, Interval.Empty<int>())).ToList();

            // Assert
            actual.Should().HaveCount(10);
        }

        [Fact]
        public void PairwiseEmptyList_ShouldBeEmpty()
        {
            // Arrange
            var list = Enumerable.Empty<Interval<int>>();

            // Act
            var actual = list.Reduce((a, b) => a.Union(b))
                .Concat(list.Reduce((a, b) => a.UnionOrDefault(b, Interval.Empty<int>())));

            // Assert
            actual.Should().BeEmpty();
        }
    }
}
