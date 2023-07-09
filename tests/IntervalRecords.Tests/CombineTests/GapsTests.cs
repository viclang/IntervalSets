using IntervalRecords.Tests.TestData;
using IntervalRecords.Types;
using System.Linq;

namespace IntervalRecords.Tests.CombineTests
{
    public class GapsTests : DataSetTestsBase
    {
        public static TheoryData<Interval<int>, Interval<int>> NoGapBetweenTwoIntervals = new TheoryData<Interval<int>, Interval<int>>
        {
            { new ClosedInterval<int>(1, 3), new ClosedInterval<int>(3, 4) },
            { new ClosedInterval<int>(1, 3), new ClosedOpenInterval<int>(3, 4) },
            { new ClosedInterval<int>(1, 3), new OpenClosedInterval<int>(3, 4) },
            { new ClosedInterval<int>(1, 3), new OpenInterval<int>(3, 4) },
            { new ClosedOpenInterval<int>(1, 3), new ClosedInterval<int>(3, 4) },
            { new ClosedOpenInterval<int>(1, 3), new ClosedOpenInterval<int>(3, 4) },
            { new ClosedOpenInterval<int>(1, 4), new OpenClosedInterval<int>(3, 4) },
            { new ClosedOpenInterval<int>(1, 4), new OpenInterval<int>(3, 4) },
            { new OpenClosedInterval<int>(1, 3), new ClosedInterval<int>(3, 4) },
            { new OpenClosedInterval<int>(1, 3), new ClosedOpenInterval<int>(3, 4) },
            { new OpenClosedInterval<int>(1, 3), new OpenClosedInterval<int>(3, 4) },
            { new OpenClosedInterval<int>(1, 3), new OpenInterval<int>(3, 4) },
            { new OpenInterval<int>(1, 3), new ClosedInterval<int>(3, 4) },
            { new OpenInterval<int>(1, 3), new ClosedOpenInterval<int>(3, 4) },
            { new OpenInterval<int>(1, 4), new OpenClosedInterval<int>(3, 4) },
            { new OpenInterval<int>(1, 4), new OpenInterval<int>(3, 4) },
        };

        [Theory]
        [MemberData(nameof(NoGapBetweenTwoIntervals))]
        public void GivenNoGapBetweenTwoAscendingIntervals_WhenGapCalculated_ReturnsNull(Interval<int> before, Interval<int> after)
        {
            var actual = before.Gap(after);

            actual.Should().BeNull();
        }


        [Theory]
        [MemberData(nameof(NoGapBetweenTwoIntervals))]
        public void GivenNoGapBetweenTwoDescendingIntervals_WhenGapCalculated_ReturnsNull(Interval<int> before, Interval<int> after)
        {
            var actual = after.Gap(before);

            actual.Should().BeNull();
        }


        public static TheoryData<Interval<int>, Interval<int>, Interval<int>> GapBetweenTwoIntervals = new()
        {
            //{ new ClosedInterval<int>(1, 2), new ClosedInterval<int>(3, 4), new OpenInterval<int>(2, 3) },
            //{ new ClosedInterval<int>(1, 2), new ClosedOpenInterval<int>(3, 4), new OpenInterval<int>(2, 3) },
            //{ new ClosedInterval<int>(1, 2), new OpenClosedInterval<int>(3, 4), new OpenClosedInterval<int>(2, 3) },
            //{ new ClosedInterval<int>(1, 2), new OpenInterval<int>(3, 4), new OpenClosedInterval<int>(2, 3) },
            //{ new ClosedOpenInterval<int>(1, 2), new ClosedInterval<int>(3, 4), new ClosedOpenInterval<int>(2, 3) },
            //{ new ClosedOpenInterval<int>(1, 2), new ClosedOpenInterval<int>(3, 4), new ClosedOpenInterval<int>(2, 3) },
            //{ new ClosedOpenInterval<int>(1, 3), new OpenClosedInterval<int>(3, 4), new ClosedInterval<int>(3, 3) },
            //{ new ClosedOpenInterval<int>(1, 3), new OpenInterval<int>(3, 4), new ClosedInterval<int>(3, 3) },
            //{ new OpenClosedInterval<int>(1, 2), new ClosedInterval<int>(3, 4), new OpenInterval<int>(2, 3) },
            //{ new OpenClosedInterval<int>(1, 2), new ClosedOpenInterval<int>(3, 4), new OpenInterval<int>(2, 3) },
            //{ new OpenClosedInterval<int>(1, 2), new OpenClosedInterval<int>(3, 4), new OpenClosedInterval<int>(2, 3) },
            //{ new OpenClosedInterval<int>(1, 2), new OpenInterval<int>(3, 4), new OpenClosedInterval<int>(2, 3) },
            //{ new OpenInterval<int>(1, 2), new ClosedInterval<int>(3, 4), new ClosedOpenInterval<int>(2, 3) },
            //{ new OpenInterval<int>(1, 2), new ClosedOpenInterval<int>(3, 4), new ClosedOpenInterval<int>(2, 3) },
            //{ new OpenInterval<int>(1, 3), new OpenClosedInterval<int>(3, 4), new ClosedInterval<int>(3, 3) },
            { new OpenInterval<int>(1, 3), new OpenInterval<int>(3, 4), new ClosedInterval<int>(3, 3) },
        };

        [Theory]
        [MemberData(nameof(GapBetweenTwoIntervals))]
        public void GivenGapBetweenTwoAscendingIntervals_WhenGapCalculated_ReturnsGapInterval(Interval<int> before, Interval<int> after, Interval<int> expectedGap)
        {
            var actual = before.Gap(after);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expectedGap);
        }

        [Theory]
        [MemberData(nameof(GapBetweenTwoIntervals))]
        public void GivenGapBetweenTwoDescendingIntervals_WhenGapCalculated_ReturnsGapInterval(Interval<int> before, Interval<int> after, Interval<int> expectedGap)
        {
            var actual = after.Gap(before);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expectedGap);
        }
    }
}
