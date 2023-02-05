using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace IntervalRecord.Tests.BinaryOperations
{
    public class UnionTests
    {
        [Theory]
        [InlineData(1, 6, 6, 10, BoundaryType.Closed)]
        [InlineData(6, 10, 1, 6, BoundaryType.Closed)]
        [InlineData(1, 6, 6, 10, BoundaryType.ClosedOpen)]
        [InlineData(6, 10, 1, 6, BoundaryType.ClosedOpen)]
        [InlineData(1, 6, 6, 10, BoundaryType.OpenClosed)]
        [InlineData(6, 10, 1, 6, BoundaryType.OpenClosed)]
        [InlineData(1, 6, 5, 10, BoundaryType.Open)]
        [InlineData(5, 10, 1, 6, BoundaryType.Open)]
        public void Union_ShouldBeExpected(int startA, int endA, int startB, int endB, BoundaryType boundaryType)
        {
            // Arrange
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var a = new Interval<int>(startA, endA, startInclusive, endInclusive);
            var b = new Interval<int>(startB, endB, startInclusive, endInclusive);

            // Act
            var actual = a.Union(b)!.Value;

            // Assert
            var array = new Interval<int>[] { a, b };
            var minByStart = array.MinBy(x => x.Start);
            var maxByEnd = array.MaxBy(x => x.End);


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive || b.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive || b.EndInclusive
                : maxByEnd.EndInclusive;

            actual.Start.Should().Be(minByStart.Start);
            actual.End.Should().Be(maxByEnd.End);
            actual.StartInclusive.Should().Be(expectedStartInclusive);
            actual.EndInclusive.Should().Be(expectedEndInclusive);
        }

        [Theory]
        [InlineData(1, 5, 6, 10, BoundaryType.Closed)]
        [InlineData(6, 10, 1, 5, BoundaryType.Closed)]
        [InlineData(1, 5, 6, 10, BoundaryType.ClosedOpen)]
        [InlineData(6, 10, 1, 5, BoundaryType.ClosedOpen)]
        [InlineData(1, 5, 6, 10, BoundaryType.OpenClosed)]
        [InlineData(6, 10, 1, 5, BoundaryType.OpenClosed)]
        [InlineData(1, 6, 6, 10, BoundaryType.Open)]
        [InlineData(6, 10, 1, 6, BoundaryType.Open)]
        public void Union_ShouldBeNull(int startA, int endA, int startB, int endB, BoundaryType boundaryType)
        {
            // Arrange
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var a = new Interval<int>(startA, endA, startInclusive, endInclusive);
            var b = new Interval<int>(startB, endB, startInclusive, endInclusive);

            // Act
            var actual = a.Union(b);

            // Assert
            actual.Should().BeNull();
        }
    }
}
