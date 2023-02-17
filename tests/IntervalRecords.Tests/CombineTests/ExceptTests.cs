using FluentAssertions.Execution;
using IntervalRecords.Tests.TestData;
using System.Linq;

namespace IntervalRecords.Tests.CombineTests
{
    public class ExceptTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void Except_ShouldBeExpectedOrNull(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
        {
            // Act
            var actual = a.Except(b);

            // Assert
            var array = new Interval<int>[] { a, b };
            var minByStart = array.MinBy(i => i.Start);
            var maxByStart = array.MaxBy(i => i.Start);

            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive || b.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive || b.EndInclusive
                : maxByStart.EndInclusive;

            using (new AssertionScope())
            {
                if (overlappingState != IntervalOverlapping.Before && overlappingState != IntervalOverlapping.After)
                {
                    actual!.Value.Start.Should().Be(minByStart.Start);
                    actual!.Value.End.Should().Be(+maxByStart.Start);
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
        public void Except_ShouldBeExpectedOrEmpty(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
        {
            // Act
            var actual = a.ExceptOrEmpty(b);

            // Assert
            var array = new Interval<int>[] { a, b };
            var minByStart = array.MinBy(i => i.Start);
            var maxByStart = array.MaxBy(i => i.Start);

            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive || b.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive || b.EndInclusive
                : maxByStart.EndInclusive;

            using (new AssertionScope())
            {
                if (overlappingState != IntervalOverlapping.Before && overlappingState != IntervalOverlapping.After)
                {
                    actual.Start.Should().Be(minByStart.Start);
                    actual.End.Should().Be(+maxByStart.Start);
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
        [InlineData(IntervalType.ClosedOpen, 4)]
        [InlineData(IntervalType.OpenClosed, 4)]
        [InlineData(IntervalType.Open, 3)]
        public void Except_ShouldBeExpected(IntervalType intervalType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, intervalType).ToList();

            // Act
            var actual = list.ExcludeOverlap().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }
    }
}
