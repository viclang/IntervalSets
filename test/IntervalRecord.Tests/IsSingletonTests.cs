using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests
{
    public class IsSingletonTests
    {
        [Theory]
        [InlineData(1, 1, BoundaryType.Closed, true)]
        [InlineData(1, 1, BoundaryType.ClosedOpen, false)]
        [InlineData(1, 1, BoundaryType.OpenClosed, false)]
        [InlineData(1, 1, BoundaryType.Open, false)]
        [InlineData(1, 2, BoundaryType.Closed, false)]
        [InlineData(1, 2, BoundaryType.ClosedOpen, false)]
        [InlineData(1, 2, BoundaryType.OpenClosed, false)]
        [InlineData(1, 2, BoundaryType.Open, false)]
        public void IsSingleton_ShouldBeExpected(int start, int end, BoundaryType boundaryType, bool expected)
        {
            // Arrange
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var interval = new Interval<int>(start, end, startInclusive, endInclusive);

            // Act
            var actual = interval.IsSingleton();

            // Assert
            actual.Should().Be(expected);
        }
    }
}
