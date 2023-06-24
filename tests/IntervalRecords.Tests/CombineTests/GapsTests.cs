using IntervalRecords.Tests.TestData;
using System.Linq;

namespace IntervalRecords.Tests.CombineTests
{
    public class GapsTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        public void OverlappingIntervalsGap_ShouldBeExpectedOrNull(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
        {
            // Act
            var actual = a.Gap(b);

            // Assert
            if (overlappingState == IntervalOverlapping.Before)
            {
                actual.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    Interval<int>.Create(
                        a.End,
                        b.Start,
                        !a.EndInclusive,
                        !b.StartInclusive)
                    );
            }
            else if (overlappingState == IntervalOverlapping.After)
            {
                actual.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    Interval<int>.Create(
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
        public void OverlappingIntervalsGap_ShouldBeExpectedOrEmpty(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
        {
            // Act
            var actual = a.GapOrEmpty(b);

            // Assert
            if (overlappingState == IntervalOverlapping.Before)
            {
                actual.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    Interval<int>.Create(
                        a.End,
                        b.Start,
                        !a.EndInclusive,
                        !b.StartInclusive)
                    );
            }
            else if (overlappingState == IntervalOverlapping.After)
            {
                actual.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    Interval<int>.Create(
                        b.End,
                        a.Start,
                        !b.EndInclusive,
                        !a.StartInclusive)
                    );
            }
            else
            {
                actual.Should().Be(Interval<int>.Empty(a.IntervalType));
            }
        }

        [Theory]
        [InlineData(IntervalType.Closed, 4)]
        [InlineData(IntervalType.ClosedOpen, 4)]
        [InlineData(IntervalType.OpenClosed, 4)]
        [InlineData(IntervalType.Open, 5)]
        public void Complement_ShouldHaveExpectedCount(IntervalType intervalType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, intervalType).ToList();

            // Act
            var actual = list.Complement().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }
    }
}
