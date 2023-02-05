using IntervalRecord.Tests.DataSets;
using IntervalRecord.Tests.TestData;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.ExtensionsTests
{
    public class PairwiseTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;
        private static readonly List<Interval<int>> _dataSet = new IntOverlappingDataSet(start, end, BoundaryType.Closed, offset).ToList();

        [Fact]
        public void Pairwise()
        {
            // Arrange
            var list = new List<Interval<int>>
            {
                new Interval<int>(1, 2, true, true),
                new Interval<int>(2, 4, true, true),
                new Interval<int>(3, 5, true, true),
                new Interval<int>(4, 6, true, true),
                new Interval<int>(5, 7, true, true),
                new Interval<int>(6, 8, true, true),
            };

            // Act
            var actual = list.Union().ToList();

            // Assert
            actual.Should().HaveCount(1);
        }
    }
}
