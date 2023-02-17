using FluentAssertions.Execution;
using IntervalRecords.Tests.TestData;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecords.Tests.CombineTests
{
    public class UnionTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Union_ShouldBeExpectedOrNull(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
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

            using (new AssertionScope())
            {
                if (overlappingState != IntervalOverlapping.Before && overlappingState != IntervalOverlapping.After)
                {
                    actual!.Value.Start.Should().Be(minByStart.Start);
                    actual!.Value.End.Should().Be(maxByEnd.End);
                    actual!.Value.StartInclusive.Should().Be(!actual!.Value.Start.IsInfinity && expectedStartInclusive);
                    actual!.Value.EndInclusive.Should().Be(!actual!.Value.End.IsInfinity && expectedEndInclusive);
                }
                else
                {
                    actual.Should().BeNull();
                }
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Union_ShouldBeExpectedOrEmpty(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
        {
            // Act
            var actual = a.UnionOrEmpty(b);

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

            using (new AssertionScope())
            {
                if (overlappingState != IntervalOverlapping.Before && overlappingState != IntervalOverlapping.After)
                {
                    actual!.Start.Should().Be(minByStart.Start);
                    actual!.End.Should().Be(maxByEnd.End);
                    actual!.StartInclusive.Should().Be(!actual!.Start.IsInfinity && expectedStartInclusive);
                    actual!.EndInclusive.Should().Be(!actual!.End.IsInfinity && expectedEndInclusive);
                }
                else
                {
                    actual.Should().Be(Interval.Empty<int>());
                }
            }
        }

        [Theory]
        [InlineData(IntervalType.Closed, 4)]
        [InlineData(IntervalType.ClosedOpen, 4)]
        [InlineData(IntervalType.OpenClosed, 4)]
        [InlineData(IntervalType.Open, 6)]
        public void Union_ShouldBeExpected(IntervalType intervalType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, intervalType);

            // Act
            var actual = list.UnionAll().ToList();

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
                emptyList.UnionAll(),
                emptyList.ExcludeOverlap(),
                emptyList.IntersectAll(),
                emptyList.Complement()
            };

            // Assert
            actual.Should().AllBeEquivalentTo(emptyList);
        }
    }
}
