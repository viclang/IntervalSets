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
    }
}
