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
        [InlineData(1, 1, IntervalType.Closed, true)]
        [InlineData(1, 1, IntervalType.ClosedOpen, false)]
        [InlineData(1, 1, IntervalType.OpenClosed, false)]
        [InlineData(1, 1, IntervalType.Open, false)]
        [InlineData(1, 2, IntervalType.Closed, false)]
        [InlineData(1, 2, IntervalType.ClosedOpen, false)]
        [InlineData(1, 2, IntervalType.OpenClosed, false)]
        [InlineData(1, 2, IntervalType.Open, false)]
        public void IsSingleton_ShouldBeExpected(int start, int end, IntervalType boundaryType, bool expected)
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
