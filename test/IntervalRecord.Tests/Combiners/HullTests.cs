using System.Linq;
using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests.Combiners
{
    public sealed class HullTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairs))]
        public void Hull_ShouldBeExpected(Interval<int> a, Interval<int> b)
        {
            // Act
            var actual = a.Hull(b);

            // Assert
            var array = new Interval<int>[] { a, b };
            var minByStart = array.MinBy(i => i.Start);
            var maxByEnd = array.MaxBy(i => i.End);


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive || b.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive || b.EndInclusive
                : maxByEnd.EndInclusive;

            actual.Start.Should().Be(minByStart.Start);
            actual.End.Should().Be(maxByEnd.End);
            actual.StartInclusive.Should().Be(!actual.Start.IsInfinite && expectedStartInclusive);
            actual.EndInclusive.Should().Be(!actual.End.IsInfinite && expectedEndInclusive);
        }

        [Theory]
        [InlineData(BoundaryType.Closed)]
        [InlineData(BoundaryType.ClosedOpen)]
        [InlineData(BoundaryType.OpenClosed)]
        [InlineData(BoundaryType.Open)]
        public void ListHull_ShouldBeExpected(BoundaryType boundaryType)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var expected = new Interval<int>(list.MinBy(i => i.Start).Start, list.MaxBy(i => i.End).End, startInclusive, endInclusive);

            // Act
            var actual = list.Hull();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Hull_EmptyList_ShouldBeNull()
        {
            // Arrange
            var list = Enumerable.Empty<Interval<int>>();

            // Act
            var actual = list.Hull();

            // Assert
            actual.Should().BeNull();
        }
    }
}
