using FluentAssertions.Execution;
using IntervalRecords.Tests.TestData;
using System.Linq;

namespace IntervalRecords.Tests.CombineTests
{
    public class IntersectTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Intersect_ShouldBeExpectedOrNull(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
        {
            // Act
            var actual = a.Intersect(b);

            // Assert
            var array = new Interval<int>[] { a, b };
            var maxByStart = array.MaxBy(i => i.Start)!;
            var minByEnd = array.MinBy(i => i.End)!;


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive && b.StartInclusive
                : maxByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive && b.EndInclusive
                : minByEnd.EndInclusive;

            using (new AssertionScope())
            {
                if (overlappingState != IntervalOverlapping.Before && overlappingState != IntervalOverlapping.After)
                {
                    actual!.Start.Should().Be(maxByStart.Start);
                    actual!.End.Should().Be(minByEnd.End);
                    actual!.StartInclusive.Should().Be(!actual!.Start.IsInfinity && expectedStartInclusive);
                    actual!.EndInclusive.Should().Be(!actual!.End.IsInfinity && expectedEndInclusive);
                }
                else
                {
                    actual.Should().BeNull();
                }
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Intersect_ShouldBeExpectedOrEmpty(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
        {
            // Act
            var actual = a.IntersectOrEmpty(b);

            // Assert
            var array = new Interval<int>[] { a, b };
            var maxByStart = array.MaxBy(i => i.Start)!;
            var minByEnd = array.MinBy(i => i.End)!;


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive && b.StartInclusive
                : maxByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive && b.EndInclusive
                : minByEnd.EndInclusive;

            using (new AssertionScope())
            {
                if (overlappingState != IntervalOverlapping.Before && overlappingState != IntervalOverlapping.After)
                {
                    actual.Start.Should().Be(maxByStart.Start);
                    actual.End.Should().Be(minByEnd.End);
                    actual.StartInclusive.Should().Be(!actual.Start.IsInfinity && expectedStartInclusive);
                    actual.EndInclusive.Should().Be(!actual.End.IsInfinity && expectedEndInclusive);
                }
                else
                {
                    actual.Should().Be(Interval.Empty<int>());
                }
            }
        }

        [Theory]
        [InlineData(IntervalType.Closed, 6)]
        [InlineData(IntervalType.ClosedOpen, 5)]
        [InlineData(IntervalType.OpenClosed, 5)]
        [InlineData(IntervalType.Open, 5)]
        public void IntersectAll_ShouldBeExpected(IntervalType intervalType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, intervalType).ToList();

            // Act
            var actual = list.IntersectAll().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }
    }
}
