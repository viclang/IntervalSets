using IntervalRecord.Tests.TestData;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace IntervalRecord.Tests.ExtensionsTests.BinaryOperations
{
    public class IntersectTests : BaseIntervalSetTests
    {
        private const int startingPoint = 0;
        private const int length = 4;
        private const int offset = 1;

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, true })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, true })]
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
    }
}
