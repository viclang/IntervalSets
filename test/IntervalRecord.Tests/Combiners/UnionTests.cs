using System.Collections.Generic;
using System.Linq;
using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests.Combiners
{
    public class UnionTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Union_ShouldBeExpectedOrNull(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.Union(b);

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

            if (overlappingState != OverlappingState.Before && overlappingState != OverlappingState.After)
            {
                actual!.Value.Start.Should().Be(minByStart.Start);
                actual!.Value.End.Should().Be(maxByEnd.End);
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
        public void Union_ShouldBeExpectedOrDefault(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.UnionOrDefault(b, a);

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

            if (overlappingState != OverlappingState.Before && overlappingState != OverlappingState.After)
            {
                actual!.Start.Should().Be(minByStart.Start);
                actual!.End.Should().Be(maxByEnd.End);
                actual!.StartInclusive.Should().Be(actual!.Start.IsInfinite ? false : expectedStartInclusive);
                actual!.EndInclusive.Should().Be(actual!.End.IsInfinite ? false : expectedEndInclusive);
            }
            else
            {
                actual.Should().Be(a);
            }
        }

        [Theory]
        [InlineData(BoundaryType.Closed, 4)]
        [InlineData(BoundaryType.ClosedOpen, 4)]
        [InlineData(BoundaryType.OpenClosed, 4)]
        [InlineData(BoundaryType.Open, 6)]
        public void Union_ShouldBeExpected(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);

            // Act
            var actual = list.Union().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }


        [Fact]
        public void EmptyList_ShouldBeEmpty()
        {
            // Arrange
            var emptyList = Enumerable.Empty<Interval<int>>();

            // Act
            var actual = new IEnumerable<Interval<int>>[]
            {
                emptyList.Union(),
                emptyList.Except(),
                emptyList.Intersect(),
                emptyList.Complement()
            };

            // Assert
            actual.Should().AllBeEquivalentTo(emptyList);
        }
    }
}
