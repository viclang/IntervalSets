using IntervalRecord.Tests.TestData;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.ExtensionsTests
{
    public class PairwiseTests : BaseIntervalSetTests
    {
        private const int startingPoint = 0;
        private const int length = 4;
        private const int offset = 1;

        [Fact]
        public void Pairwise()
        {
            // Arrange
            var dataset = new IntOverlappingDataSet(startingPoint, length, offset, BoundaryType.Closed);
            var list = new List<Interval<int>>
            {
                dataset.Before,
                dataset.Starts,
                dataset.Finishes,
                dataset.After
            };

            // Act
            var actual = list.Union().ToList();

            // Assert
            actual.Should().HaveCount(3);
        }
    }
}
