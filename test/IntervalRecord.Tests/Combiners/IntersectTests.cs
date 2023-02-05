using IntervalRecord.Tests.TestData;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace IntervalRecord.Tests.Combiners
{
    public class IntersectTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Intersect_ShouldBeExpectedOrNull(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.Intersect(b);

            // Assert
            var array = new Interval<int>[] { a, b };
            var maxByStart = array.MaxBy(x => x.Start);
            var minByEnd = array.MinBy(x => x.End);


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive && b.StartInclusive
                : maxByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive && b.EndInclusive
                : minByEnd.EndInclusive;

            if (overlappingState != OverlappingState.Before && overlappingState != OverlappingState.After)
            {
                actual!.Value.Start.Should().Be(maxByStart.Start);
                actual!.Value.End.Should().Be(minByEnd.End);
                actual!.Value.StartInclusive.Should().Be(actual!.Value.Start.IsInfinite ? false : expectedStartInclusive);
                actual!.Value.EndInclusive.Should().Be(actual!.Value.End.IsInfinite ? false : expectedEndInclusive);
            }
            else
            {
                actual.Should().BeNull();
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Intersect_ShouldBeExpectedOrDefault(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.IntersectOrDefault(b, a);

            // Assert
            var array = new Interval<int>[] { a, b };
            var maxByStart = array.MaxBy(x => x.Start);
            var minByEnd = array.MinBy(x => x.End);


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive && b.StartInclusive
                : maxByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive && b.EndInclusive
                : minByEnd.EndInclusive;

            if (overlappingState != OverlappingState.Before && overlappingState != OverlappingState.After)
            {
                actual.Start.Should().Be(maxByStart.Start);
                actual.End.Should().Be(minByEnd.End);
                actual.StartInclusive.Should().Be(actual.Start.IsInfinite ? false : expectedStartInclusive);
                actual.EndInclusive.Should().Be(actual.End.IsInfinite ? false : expectedEndInclusive);
            }
            else
            {
                actual.Should().Be(a);
            }
        }

        [Theory]
        [InlineData(BoundaryType.Closed, 6)]
        [InlineData(BoundaryType.ClosedOpen, 5)]
        [InlineData(BoundaryType.OpenClosed, 5)]
        [InlineData(BoundaryType.Open, 5)]
        public void Intersect_ShouldBeExpected(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType).ToList();

            // Act
            var actual = list.Intersect().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }
    }
}
