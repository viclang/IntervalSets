using IntervalRecord.Tests.TestData;
using System.Linq;

namespace IntervalRecord.Tests.ExtensionsTests.BinaryOperations
{
    public sealed class HullTests : BaseIntervalPairSetTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 2;

        [Theory]
        [MemberData(nameof(IntervalPairs), new object[] { start, end, BoundaryType.Closed, offset })]
        [MemberData(nameof(IntervalPairs), new object[] { start, end, BoundaryType.ClosedOpen, offset })]
        [MemberData(nameof(IntervalPairs), new object[] { start, end, BoundaryType.OpenClosed, offset })]
        [MemberData(nameof(IntervalPairs), new object[] { start, end, BoundaryType.Open, offset })]
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
