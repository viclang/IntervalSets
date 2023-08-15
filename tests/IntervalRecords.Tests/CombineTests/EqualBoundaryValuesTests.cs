using IntervalRecords.Extensions;
using IntervalRecords.Types;

namespace IntervalRecords.Tests.CombineTests
{
    public class EqualBoundaryValuesTests
    {
        private const int start = 6;
        private const int end = 10;

        [Theory]
        [InlineData(IntervalType.Closed, IntervalType.Closed, IntervalType.Closed)]
        [InlineData(IntervalType.Closed, IntervalType.ClosedOpen, IntervalType.Closed)]
        [InlineData(IntervalType.Closed, IntervalType.OpenClosed, IntervalType.Closed)]
        [InlineData(IntervalType.Closed, IntervalType.Open, IntervalType.Closed)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.Closed, IntervalType.Closed)]
        [InlineData(IntervalType.OpenClosed, IntervalType.Closed, IntervalType.Closed)]
        [InlineData(IntervalType.Open, IntervalType.Closed, IntervalType.Closed)]
        [InlineData(IntervalType.Open, IntervalType.Open, IntervalType.Open)]
        [InlineData(IntervalType.Open, IntervalType.ClosedOpen, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.Open, IntervalType.OpenClosed, IntervalType.OpenClosed)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.Open, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed, IntervalType.Open, IntervalType.OpenClosed)]
        public void Except_Union_Hull_EqualBoundaryValues_ShouldBeMaxIntervalType(IntervalType leftIntervalType, IntervalType rightIntervalType, IntervalType expectedIntervalType)
        {
            // Arrange
            var (leftStartInclusive, leftEndInclusive) = leftIntervalType.ToTuple();
            var leftInterval = Interval.Create<int>(start, end, leftStartInclusive, leftEndInclusive);

            var (rightStartInclusive, rigthEndInclusive) = rightIntervalType.ToTuple();
            var rigthInterval = Interval.Create<int>(start, end, rightStartInclusive, rigthEndInclusive);

            // Act
            var actual = new IntervalType[]
            {
                leftInterval.Except(rigthInterval)!.IntervalType,
                leftInterval.Union(rigthInterval)!.IntervalType,
                leftInterval.Hull(rigthInterval).IntervalType
            };

            // Assert
            actual.Should().AllBeEquivalentTo(expectedIntervalType);
        }


        [Theory]
        [InlineData(IntervalType.Closed, IntervalType.Closed, IntervalType.Closed)]
        [InlineData(IntervalType.Closed, IntervalType.ClosedOpen, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.Closed, IntervalType.OpenClosed, IntervalType.OpenClosed)]
        [InlineData(IntervalType.Closed, IntervalType.Open, IntervalType.Open)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.Closed, IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed, IntervalType.Closed, IntervalType.OpenClosed)]
        [InlineData(IntervalType.Open, IntervalType.Closed, IntervalType.Open)]
        [InlineData(IntervalType.Open, IntervalType.Open, IntervalType.Open)]
        [InlineData(IntervalType.Open, IntervalType.ClosedOpen, IntervalType.Open)]
        [InlineData(IntervalType.Open, IntervalType.OpenClosed, IntervalType.Open)]
        [InlineData(IntervalType.ClosedOpen, IntervalType.Open, IntervalType.Open)]
        [InlineData(IntervalType.OpenClosed, IntervalType.Open, IntervalType.Open)]
        public void Intersect_EqualBoundaryValues_ShouldBeMinIntervalType(IntervalType leftIntervalType, IntervalType rightIntervalType, IntervalType expectedIntervalType)
        {
            // Arrange
            var (leftStartInclusive, leftEndInclusive) = leftIntervalType.ToTuple();
            var leftInterval =  Interval.Create<int>(start, end, leftStartInclusive, leftEndInclusive);

            var (rightStartInclusive, rigthEndInclusive) = rightIntervalType.ToTuple();
            var rigthInterval = Interval.Create<int>(start, end, rightStartInclusive, rigthEndInclusive);

            // Act
            var actual = leftInterval.Intersect(rigthInterval)!.IntervalType;

            // Assert
            actual.Should().Be(expectedIntervalType);
        }
    }
}
