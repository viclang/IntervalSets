using IntervalRecord.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.Combiners
{
    public class PairwiseTests : DataSetTestsBase
    {
        [Theory]
        [InlineData(BoundaryType.Closed, 4)]
        [InlineData(BoundaryType.ClosedOpen, 4)]
        [InlineData(BoundaryType.OpenClosed, 4)]
        [InlineData(BoundaryType.Open, 5)]
        public void PairwiseGap_ShouldHaveExpectedCount(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);

            // Act
            var actual = list.Pairwise((a, b) => a.Gap(b)).ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(BoundaryType.Closed)]
        [InlineData(BoundaryType.ClosedOpen)]
        [InlineData(BoundaryType.OpenClosed)]
        [InlineData(BoundaryType.Open)]
        public void PairwiseGapOrDefault_ShouldHaveExpectedCount(BoundaryType boundaryType)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);

            // Act
            var actual = list.Pairwise((a, b) => a.GapOrDefault(b, Interval.Empty<int>())).ToList();

            // Assert
            actual.Should().HaveCount(10);
        }

        [Fact]
        public void PairwiseEmptyList_ShouldBeEmpty()
        {
            // Arrange
            var list = Enumerable.Empty<Interval<int>>();

            // Act
            var actual = list.Pairwise((a, b) => a.Gap(b))
                .Concat(list.Pairwise((a, b) => a.GapOrDefault(b, Interval.Empty<int>())));

            // Assert
            actual.Should().BeEmpty();
        }
    }
}
