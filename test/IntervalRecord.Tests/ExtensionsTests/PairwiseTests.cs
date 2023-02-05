using IntervalRecord.Tests.TestData;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace IntervalRecord.Tests.ExtensionsTests
{
    public class PairwiseTests : BaseIntervalSetTests
    {
        private const int startingPoint = 0;
        private const int length = 4;
        private const int offset = 2;

        [Fact]
        public void Pairwise()
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, BoundaryType.Closed).ToList();

            // Act
            var actual = list.Complement(x => !x.Closure(1).IsEmpty()).ToList();

            // Assert
            actual.Should().HaveCount(4);
        }
    }
}
