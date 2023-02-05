using IntervalRecord.Tests.DataSets;
using IntervalRecord.Tests.TestData;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecord.Tests.BinaryOperations
{
    public class GapsTests
    {

        private const int start = 6;
        private const int end = 10;
        private const int offset = 2;
        private static IOverlappingDataSet<int> _dataSet = new IntOverlappingDataSet(start, end, BoundaryType.Closed, offset);

        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> IntervalOverlaps(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlappingState(false);

        [Theory]
        [InlineData(6, 10, BoundaryType.Closed)]
        [InlineData(6, 10, BoundaryType.ClosedOpen)]
        [InlineData(6, 10, BoundaryType.OpenClosed)]
        [InlineData(6, 10, BoundaryType.Open)]

        [InlineData(7, 12, BoundaryType.Closed)]
        [InlineData(7, 12, BoundaryType.ClosedOpen)]
        [InlineData(7, 12, BoundaryType.OpenClosed)]
        [InlineData(7, 12, BoundaryType.Open)]

        [InlineData(8, 11, BoundaryType.Closed)]
        [InlineData(8, 11, BoundaryType.ClosedOpen)]
        [InlineData(8, 11, BoundaryType.OpenClosed)]
        [InlineData(8, 11, BoundaryType.Open)]
        public void BeforeAndAfterGap_ShouldBeExpected(int start, int end, BoundaryType boundaryType)
        {
            // Arrange
            var dataSet = new IntOverlappingDataSet(start, end, boundaryType, 2);

            // Act
            var actualBefore = dataSet.Before.Gap(dataSet.Reference);
            var actualAfter = dataSet.After.Gap(dataSet.Reference);

            // Assert
            actualBefore.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    new Interval<int>(
                        dataSet.Before.End,
                        dataSet.Reference.Start,
                        !dataSet.Before.EndInclusive,
                        !dataSet.Reference.StartInclusive)
                    );


            actualAfter.Should()
                .NotBeNull()
                .And.BeEquivalentTo(
                    new Interval<int>(
                        dataSet.Reference.End,
                        dataSet.After.Start,
                        !dataSet.Reference.EndInclusive,
                        !dataSet.After.StartInclusive)
                    );
        }

        [Theory]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Open)]
        public void OverlappingIntervalsGap_ShouldBeNull(Interval<int> a, Interval<int> b, OverlappingState overlappingState)
        {
            // Act
            var actual = a.Gap(b);

            // Assert
            if (overlappingState != OverlappingState.Before
                && overlappingState != OverlappingState.After)
            {
                actual.Should().BeNull();
            }
            else
            {
                actual.Should().NotBeNull();
            }
        }
    }
}
