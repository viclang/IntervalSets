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
        [InlineData(IntervalType.Closed, 4)]
        [InlineData(IntervalType.ClosedOpen, 4)]
        [InlineData(IntervalType.OpenClosed, 4)]
        [InlineData(IntervalType.Open, 5)]
        public void PairwiseGap_ShouldHaveExpectedCount(IntervalType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);

            // Act
            var actual = list.Pairwise((a, b) => a.Gap(b)).ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(IntervalType.Closed)]
        [InlineData(IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed)]
        [InlineData(IntervalType.Open)]
        public void PairwiseGapOrDefault_ShouldHaveExpectedCount(IntervalType boundaryType)
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
