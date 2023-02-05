
namespace IntervalRecord.Tests.ExtensionsTests.BinaryOperations
{
    public class EqualBoundaryValuesTests
    {
        private const int start = 6;
        private const int end = 10;

        [Theory]
        [InlineData(BoundaryType.Closed, BoundaryType.Closed, BoundaryType.Closed)]
        [InlineData(BoundaryType.Closed, BoundaryType.ClosedOpen, BoundaryType.Closed)]
        [InlineData(BoundaryType.Closed, BoundaryType.OpenClosed, BoundaryType.Closed)]
        [InlineData(BoundaryType.Closed, BoundaryType.Open, BoundaryType.Closed)]
        [InlineData(BoundaryType.ClosedOpen, BoundaryType.Closed, BoundaryType.Closed)]
        [InlineData(BoundaryType.OpenClosed, BoundaryType.Closed, BoundaryType.Closed)]
        [InlineData(BoundaryType.Open, BoundaryType.Closed, BoundaryType.Closed)]
        [InlineData(BoundaryType.Open, BoundaryType.Open, BoundaryType.Open)]
        [InlineData(BoundaryType.Open, BoundaryType.ClosedOpen, BoundaryType.ClosedOpen)]
        [InlineData(BoundaryType.Open, BoundaryType.OpenClosed, BoundaryType.OpenClosed)]
        [InlineData(BoundaryType.ClosedOpen, BoundaryType.Open, BoundaryType.ClosedOpen)]
        [InlineData(BoundaryType.OpenClosed, BoundaryType.Open, BoundaryType.OpenClosed)]
        public void Except_Union_Hull_EqualBoundaryValues_ShouldBeMaxBoundaryType(BoundaryType leftBoundaryType, BoundaryType rightBoundaryType, BoundaryType expectedBoundaryType)
        {
            // Arrange
            var (leftStartInclusive, leftEndInclusive) = leftBoundaryType.ToTuple();
            var leftInterval = new Interval<int>(start, end, leftStartInclusive, leftEndInclusive);

            var (rightStartInclusive, rigthEndInclusive) = rightBoundaryType.ToTuple();
            var rigthInterval = new Interval<int>(start, end, rightStartInclusive, rigthEndInclusive);

            // Act
            var actual = new BoundaryType[]
            {
                leftInterval.Except(rigthInterval)!.Value.GetBoundaryType(),
                leftInterval.Union(rigthInterval)!.Value.GetBoundaryType(),
                leftInterval.Hull(rigthInterval).GetBoundaryType()
            };

            // Assert
            actual.Should().AllBeEquivalentTo(expectedBoundaryType);
        }


        [Theory]
        [InlineData(BoundaryType.Closed, BoundaryType.Closed, BoundaryType.Closed)]
        [InlineData(BoundaryType.Closed, BoundaryType.ClosedOpen, BoundaryType.ClosedOpen)]
        [InlineData(BoundaryType.Closed, BoundaryType.OpenClosed, BoundaryType.OpenClosed)]
        [InlineData(BoundaryType.Closed, BoundaryType.Open, BoundaryType.Open)]
        [InlineData(BoundaryType.ClosedOpen, BoundaryType.Closed, BoundaryType.ClosedOpen)]
        [InlineData(BoundaryType.OpenClosed, BoundaryType.Closed, BoundaryType.OpenClosed)]
        [InlineData(BoundaryType.Open, BoundaryType.Closed, BoundaryType.Open)]
        [InlineData(BoundaryType.Open, BoundaryType.Open, BoundaryType.Open)]
        [InlineData(BoundaryType.Open, BoundaryType.ClosedOpen, BoundaryType.Open)]
        [InlineData(BoundaryType.Open, BoundaryType.OpenClosed, BoundaryType.Open)]
        [InlineData(BoundaryType.ClosedOpen, BoundaryType.Open, BoundaryType.Open)]
        [InlineData(BoundaryType.OpenClosed, BoundaryType.Open, BoundaryType.Open)]
        public void Intersect_EqualBoundaryValues_ShouldBeMinBoundaryType(BoundaryType leftBoundaryType, BoundaryType rightBoundaryType, BoundaryType expectedBoundaryType)
        {
            // Arrange
            var (leftStartInclusive, leftEndInclusive) = leftBoundaryType.ToTuple();
            var leftInterval = new Interval<int>(start, end, leftStartInclusive, leftEndInclusive);

            var (rightStartInclusive, rigthEndInclusive) = rightBoundaryType.ToTuple();
            var rigthInterval = new Interval<int>(start, end, rightStartInclusive, rigthEndInclusive);

            // Act
            var actual = leftInterval.Intersect(rigthInterval)!.Value.GetBoundaryType();

            // Assert
            actual.Should().Be(expectedBoundaryType);
        }
    }
}
