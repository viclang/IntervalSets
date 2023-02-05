using IntervalRecord.Tests.TestData;
using System.Data;

namespace IntervalRecord.Tests.ExtensionsTests.BinaryOperations
{
    public class GapsTests : BaseIntervalPairSetTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 2;

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, false })]
        public void OverlappingIntervalsGap_ShouldBeExpectedOrNull(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.Gap(b);

            // Assert
            if (overlappingState == OverlappingState.Before)
            {
                actual.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    new Interval<int>(
                        a.End,
                        b.Start,
                        !a.EndInclusive,
                        !b.StartInclusive)
                    );
            }
            else if (overlappingState == OverlappingState.After)
            {
                actual.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    new Interval<int>(
                        b.End,
                        a.Start,
                        !b.EndInclusive,
                        !a.StartInclusive)
                    );
            }
            else
            {
                actual.Should().BeNull();
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, false })]
        public void OverlappingIntervalsGap_ShouldBeExpectedOrDefault(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.GapOrDefault(b, a);

            // Assert
            if (overlappingState == OverlappingState.Before)
            {
                actual.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    new Interval<int>(
                        a.End,
                        b.Start,
                        !a.EndInclusive,
                        !b.StartInclusive)
                    );
            }
            else if (overlappingState == OverlappingState.After)
            {
                actual.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    new Interval<int>(
                        b.End,
                        a.Start,
                        !b.EndInclusive,
                        !a.StartInclusive)
                    );
            }
            else
            {
                actual.Should().Be(a);
            }
        }
    }
}
