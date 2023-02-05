using IntervalRecord.Tests.TestData;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.ExtensionsTests
{
    public class PairwiseTests : BaseIntervalSetTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 3;

        [Fact]
        public void Pairwise()
        {
            // Arrange
            var list = OverlapList(start, end, BoundaryType.Closed, offset);

            // Act
            var actual = list.Union().ToList();

            // Assert
            actual.Should().HaveCount(1);
        }
    }
}
