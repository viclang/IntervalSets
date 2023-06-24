using FluentAssertions.Execution;
using IntervalRecords.Tests.TestData;
using System.Linq;

namespace IntervalRecords.Tests.CombineTests
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
            var minByStart = array.MinBy(i => i.Start)!;
            var maxByEnd = array.MaxBy(i => i.End)!;


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive || b.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive || b.EndInclusive
                : maxByEnd.EndInclusive;

            using (new AssertionScope())
            {
                actual.Start.Should().Be(minByStart.Start);
                actual.End.Should().Be(maxByEnd.End);
                actual.StartInclusive.Should().Be(!actual.Start.IsNegativeInfinity && expectedStartInclusive);
                actual.EndInclusive.Should().Be(!actual.End.IsPositiveInfinity && expectedEndInclusive);
            }
        }

        [Theory]
        [InlineData(IntervalType.Closed)]
        [InlineData(IntervalType.ClosedOpen)]
        [InlineData(IntervalType.OpenClosed)]
        [InlineData(IntervalType.Open)]
        public void ListHull_ShouldBeExpected(IntervalType intervalType)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, intervalType);
            var (startInclusive, endInclusive) = intervalType.ToTuple();
            var expected = Interval<int>.Create(list.MinBy(i => i.Start)!.Start, list.MaxBy(i => i.End)!.End, startInclusive, endInclusive);

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
