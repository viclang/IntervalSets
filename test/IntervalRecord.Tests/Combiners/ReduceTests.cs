using IntervalRecord.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.Combiners
{
    public class ReduceTests : DataSetTestsBase
    {
        [Theory]
        [InlineData(BoundaryType.Closed, 4)]
        [InlineData(BoundaryType.ClosedOpen, 4)]
        [InlineData(BoundaryType.OpenClosed, 4)]
        [InlineData(BoundaryType.Open, 6)]
        public void ReduceUnion_ShouldHaveExpectedCount(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);

            // Act
            var actual = list.Reduce((a, b) => a.Union(b)).ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(BoundaryType.Closed)]
        [InlineData(BoundaryType.ClosedOpen)]
        [InlineData(BoundaryType.OpenClosed)]
        [InlineData(BoundaryType.Open)]
        public void ReduceUnionOrDefault_ShouldHaveExpectedCount(BoundaryType boundaryType)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);

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
