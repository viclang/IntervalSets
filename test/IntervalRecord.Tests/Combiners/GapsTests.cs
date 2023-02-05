using System.Data;
using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests.Combiners
{
    public class GapsTests : OverlappingStateTestsBase
    {
        private const int startingPoint = 0;
        private const int length = 4;
        private const int offset = 1;

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, false })]
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
