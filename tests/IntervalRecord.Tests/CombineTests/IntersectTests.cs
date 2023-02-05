using FluentAssertions.Execution;
using IntervalRecord.Tests.TestData;
using System.Linq;

namespace IntervalRecord.Tests.CombineTests
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
            var maxByStart = array.MaxBy(i => i.Start);
            var minByEnd = array.MinBy(i => i.End);


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive && b.StartInclusive
                : maxByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive && b.EndInclusive
                : minByEnd.EndInclusive;

            using (new AssertionScope())
            {
                if (overlappingState != OverlappingState.Before && overlappingState != OverlappingState.After)
                {
                    actual!.Value.Start.Should().Be(maxByStart.Start);
                    actual!.Value.End.Should().Be(minByEnd.End);
                    actual!.Value.StartInclusive.Should().Be(actual!.Value.Start.IsInfinity ? false : expectedStartInclusive);
                    actual!.Value.EndInclusive.Should().Be(actual!.Value.End.IsInfinity ? false : expectedEndInclusive);
                }
                else
                {
                    actual.Should().BeNull();
                }
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
            var maxByStart = array.MaxBy(i => i.Start);
            var minByEnd = array.MinBy(i => i.End);


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive && b.StartInclusive
                : maxByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive && b.EndInclusive
                : minByEnd.EndInclusive;

            using (new AssertionScope())
            {
                if (overlappingState != OverlappingState.Before && overlappingState != OverlappingState.After)
                {
                    actual.Start.Should().Be(maxByStart.Start);
                    actual.End.Should().Be(minByEnd.End);
                    actual.StartInclusive.Should().Be(actual.Start.IsInfinity ? false : expectedStartInclusive);
                    actual.EndInclusive.Should().Be(actual.End.IsInfinity ? false : expectedEndInclusive);
                }
                else
                {
                    actual.Should().Be(a);
                }
            }
        }

        [Theory]
        [InlineData(IntervalType.Closed, 6)]
        [InlineData(IntervalType.ClosedOpen, 5)]
        [InlineData(IntervalType.OpenClosed, 5)]
        [InlineData(IntervalType.Open, 5)]
        public void Intersect_ShouldBeExpected(IntervalType intervalType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, intervalType).ToList();

            // Act
            var actual = list.Intersect().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }
    }
}
