using FluentAssertions.Execution;
using IntervalRecords.Tests.TestData;
using IntervalRecords.Extensions;
using System.Linq;

namespace IntervalRecords.Tests.CombineTests
{
    public class ExceptTests
    {
        [Theory]
        [ClassData(typeof(Int32OverlapClassData))]
        public void Except_ShouldBeExpectedOrNull(OverlapTestData<int> testData)
        {
            // Act
            var actual = testData.First.Except(testData.Second);

            // Assert
            var array = new Interval<int>[] { testData.First, testData.Second };
            var minByStart = array.MinBy(i => i.Start)!;
            var maxByStart = array.MaxBy(i => i.Start)!;

            var expectedStartInclusive = testData.First.Start == testData.Second.Start
                ? testData.First.StartInclusive || testData.Second.StartInclusive
                : minByStart.StartInclusive;

            var expectedEndInclusive = testData.First.End == testData.Second.End
                ? testData.First.EndInclusive || testData.Second.EndInclusive
                : maxByStart.EndInclusive;

            using (new AssertionScope())
            {
                actual!.Start.Should().Be(minByStart.Start);
                actual!.End.Should().Be(+maxByStart.Start);
                actual!.StartInclusive.Should().Be(expectedStartInclusive);
                actual!.EndInclusive.Should().Be(expectedEndInclusive);
            }
        }

        //[Theory]
        //[MemberData(nameof(IntervalPairsWithOverlappingState), true)]
        //public void Except_ShouldBeExpectedOrEmpty(Interval<int> a, Interval<int> b, IntervalOverlapping overlappingState)
        //{
        //    // Act
        //    var actual = a.ExceptOrEmpty(b);

        //    // Assert
        //    var array = new Interval<int>[] { a, b };
        //    var minByStart = array.MinBy(i => i.Start)!;
        //    var maxByStart = array.MaxBy(i => i.Start)!;

        //    var expectedStartInclusive = a.Start == b.Start
        //        ? a.StartInclusive || b.StartInclusive
        //        : minByStart!.StartInclusive;

        //    var expectedEndInclusive = a.End == b.End
        //        ? a.EndInclusive || b.EndInclusive
        //        : maxByStart!.EndInclusive;

        //    using (new AssertionScope())
        //    {
        //        if (overlappingState != IntervalOverlapping.Before && overlappingState != IntervalOverlapping.After)
        //        {
        //            actual.Start.Should().Be(minByStart.Start);
        //            actual.End.Should().Be(+maxByStart.Start);
        //            actual.StartInclusive.Should().Be(!actual.Start.IsNegativeInfinity && expectedStartInclusive);
        //            actual.EndInclusive.Should().Be(!actual.End.IsPositiveInfinity && expectedEndInclusive);
        //        }
        //        else
        //        {
        //            actual.Should().Be(Interval<int>.Empty(a.IntervalType));
        //        }
        //    }
        //}

        //[Theory]
        //[InlineData(IntervalType.Closed, 6)]
        //[InlineData(IntervalType.ClosedOpen, 4)]
        //[InlineData(IntervalType.OpenClosed, 4)]
        //[InlineData(IntervalType.Open, 3)]
        //public void Except_ShouldBeExpected(IntervalType intervalType, int expectedCount)
        //{
        //     Arrange
        //    var list = OverlapList(startingPoint, length, offset, intervalType).ToList();

        //     Act
        //    var actual = list.ExcludeOverlap().ToList();

        //     Assert
        //    actual.Should().HaveCount(expectedCount);
        //}
    }
}
