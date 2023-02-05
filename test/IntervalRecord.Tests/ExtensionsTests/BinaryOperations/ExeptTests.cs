using IntervalRecord.Tests.TestData;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace IntervalRecord.Tests.ExtensionsTests.BinaryOperations
{
    public class ExceptTests : BaseIntervalPairSetTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 2;
        
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, true })]
        public void Except_ShouldBeExpectedOrNull(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.Except(b);

            // Assert
            var array = new Interval<int>[] { a, b };
            var minByStart = array.MinBy(x => x.Start);
            var maxByStart = array.MaxBy(x => x.Start);

            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive || b.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive || b.EndInclusive
                : maxByStart.EndInclusive;

            if (overlappingState != OverlappingState.Before && overlappingState != OverlappingState.After)
            {
                actual!.Value.Start.Should().Be(minByStart.Start);
                actual!.Value.End.Should().Be(+maxByStart.Start);
                actual!.Value.StartInclusive.Should().Be(actual!.Value.Start.IsInfinite ? false : expectedStartInclusive);
                actual!.Value.EndInclusive.Should().Be(actual!.Value.End.IsInfinite ? false : expectedEndInclusive);
            }
            else
            {
                actual.Should().BeNull();
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, true })]
        public void Except_ShouldBeExpectedOrDefault(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.ExceptOrDefault(b, a);

            // Assert
            var array = new Interval<int>[] { a, b };
            var minByStart = array.MinBy(x => x.Start);
            var maxByStart = array.MaxBy(x => x.Start);

            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive || b.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive || b.EndInclusive
                : maxByStart.EndInclusive;

            if (overlappingState != OverlappingState.Before && overlappingState != OverlappingState.After)
            {
                actual.Start.Should().Be(minByStart.Start);
                actual.End.Should().Be(+maxByStart.Start);
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
