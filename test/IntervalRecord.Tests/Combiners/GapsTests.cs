using System.Data;
using System.Linq;
using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests.Combiners
{
    public class GapsTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
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

        [Theory]
        [InlineData(IntervalType.Closed, 4)]
        [InlineData(IntervalType.ClosedOpen, 4)]
        [InlineData(IntervalType.OpenClosed, 4)]
        [InlineData(IntervalType.Open, 5)]
        public void Complement_ShouldHaveExpectedCount(IntervalType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType).ToList();

            // Act
            var actual = list.Complement().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }
    }
}
