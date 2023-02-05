using System.Linq;

namespace IntervalRecord.Tests.Extensions
{
    public sealed class HullTests : OverlappingStateTestsBase
    {
        private const int startingPoint = 0;
        private const int length = 4;
        private const int offset = 1;

        [Theory]
        [MemberData(nameof(IntervalPairs), new object[] { startingPoint, length, offset, BoundaryType.Closed })]
        [MemberData(nameof(IntervalPairs), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen })]
        [MemberData(nameof(IntervalPairs), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed })]
        [MemberData(nameof(IntervalPairs), new object[] { startingPoint, length, offset, BoundaryType.Open })]
        public void Hull_ShouldBeExpected(Interval<int> a, Interval<int> b)
        {
            // Act
            var actual = a.Hull(b);

            // Assert
            var array = new Interval<int>[] { a, b };
            var minByStart = array.MinBy(x => x.Start);
            var maxByEnd = array.MaxBy(x => x.End);


            var expectedStartInclusive = a.Start == b.Start
                ? a.StartInclusive || b.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = a.End == b.End
                ? a.EndInclusive || b.EndInclusive
                : maxByEnd.EndInclusive;

            actual.Start.Should().Be(minByStart.Start);
            actual.End.Should().Be(maxByEnd.End);
            actual.StartInclusive.Should().Be(actual.Start.IsInfinite ? false : expectedStartInclusive);
            actual.EndInclusive.Should().Be(actual.End.IsInfinite ? false : expectedEndInclusive);
        }
    }
}
