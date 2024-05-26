using FluentAssertions;
using IntervalSet.Operations;
using IntervalSet.Tests.TestData;
using IntervalSet.Types;

namespace IntervalSet.Tests.Types
{
    public class ComparableTests
    {
        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void CompareTo_ShouldBeExpected(string left, string right, IntervalRelation relation)
        {
            var leftInterval = Interval<int>.Parse(left);
            var rightInterval = Interval<int>.Parse(right);
            var expected = ExpectedCompareResult(relation);

            var actual = leftInterval.CompareTo(rightInterval);

            // Assert
            actual.Should().Be(expected);
        }

        //[Theory]
        //[ClassData(typeof(Int32IntervalRelationClassData))]
        //public void GreaterThan_ShouldBeExpected(string left, string right, IntervalRelation relation)
        //{
        //    var leftInterval = Interval<int>.Parse(left);
        //    var rightInterval = Interval<int>.Parse(right);
        //    var expected = ExpectedCompareResult(relation) > 0;

        //    var actual = leftInterval > rightInterval;

        //    actual.Should().Be(expected);
        //}

        //[Theory]
        //[ClassData(typeof(Int32IntervalRelationClassData))]
        //public void LessThan_ShouldBeExpected(string left, string right, IntervalRelation relation)
        //{
        //    var leftInterval = Interval<int>.Parse(left);
        //    var rightInterval = Interval<int>.Parse(right);
        //    var expected = ExpectedCompareResult(relation) < 0;

        //    var actual = leftInterval < rightInterval;

        //    actual.Should().Be(expected);
        //}

        //[Theory]
        //[ClassData(typeof(Int32IntervalRelationClassData))]
        //public void GreaterOrEqualTo_ShouldBeExpected(string left, string right, IntervalRelation relation)
        //{
        //    var leftInterval = Interval<int>.Parse(left);
        //    var rightInterval = Interval<int>.Parse(right);
        //    var expected = ExpectedCompareResult(relation) >= 0;

        //    var actual = leftInterval >= rightInterval;

        //    actual.Should().Be(expected);
        //}

        //[Theory]
        //[ClassData(typeof(Int32IntervalRelationClassData))]
        //public void LessOrEqualTo_ShouldBeExpected(string left, string right, IntervalRelation relation)
        //{
        //    var leftInterval = Interval<int>.Parse(left);
        //    var rightInterval = Interval<int>.Parse(right);
        //    var expected = ExpectedCompareResult(relation) <= 0;

        //    var actual = leftInterval <= rightInterval;

        //    actual.Should().Be(expected);
        //}

        private static int ExpectedCompareResult(IntervalRelation relation)
        {
            return ((int)relation).CompareTo((int)IntervalRelation.Equals);
        }
    }
}
