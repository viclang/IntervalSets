
using IntervalRecord.Tests.DataSets;
using System;

namespace IntervalRecord.Tests.OverlapsTests
{
    public class DateOnlyOverlapsTests
    {
        //arrange
        private static readonly DateOnly _start = new DateOnly(2022, 7, 30);
        private static readonly DateOnly _end = _start.AddDays(4);
        private const int offset = 1;

        private static IntervalDataSet<DateOnly> _dataSet = new IntervalDataSet<DateOnly>(_start, _end, BoundaryType.Closed, offset);

        public static TheoryData<Interval<DateOnly>, Interval<DateOnly>, bool> IntervalOverlaps(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlapsWithData(false);

        public static TheoryData<Interval<DateOnly>, Interval<DateOnly>, bool> IntervalOverlaps_HalfOpen(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlapsWithData(true);

        [Theory]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps), BoundaryType.Open)]
        public void IsConnected(Interval<DateOnly> a, Interval<DateOnly> b, bool expectedResult)
        {
            //act
            var result = a.IsConnected(b);

            //assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(IntervalOverlaps_HalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(IntervalOverlaps_HalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(IntervalOverlaps_HalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(IntervalOverlaps_HalfOpen), BoundaryType.Open)]
        public void IsConnected_IncludeHalfOpen(Interval<DateOnly> a, Interval<DateOnly> b, bool expectedResult)
        {
            //act
            var result = a.IsConnected(b, true);

            //assert
            result.Should().Be(expectedResult);
        }
    }
}
