using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData.ClassData;

namespace IntervalRecords.Tests
{
    public class ComparableTests
    {
        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void CompareTo_ShouldBeExpected(string left, string right, IntervalRelation relation)
        {
            byte a = 1;
            byte b = 0;
            var c = a.CompareTo(b);

            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);
            var expected = ExpectedCompareResult(relation);

            var actual = leftInterval.CompareTo(rightInterval);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void GreaterThan_ShouldBeExpected(string left, string right, IntervalRelation relation)
        {
            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);
            var expected = ExpectedCompareResult(relation) > 0;

            var actual = leftInterval > rightInterval;

            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void LessThan_ShouldBeExpected(string left, string right, IntervalRelation relation)
        {
            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);
            var expected = ExpectedCompareResult(relation) < 0;

            var actual = leftInterval < rightInterval;

            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void GreaterOrEqualTo_ShouldBeExpected(string left, string right, IntervalRelation relation)
        {
            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);
            var expected = ExpectedCompareResult(relation) >= 0;

            var actual = leftInterval >= rightInterval;

            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void LessOrEqualTo_ShouldBeExpected(string left, string right, IntervalRelation relation)
        {
            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);
            var expected = ExpectedCompareResult(relation) <= 0;

            var actual = leftInterval <= rightInterval;

            actual.Should().Be(expected);
        }

        private static int ExpectedCompareResult(IntervalRelation relation)
        {
            return ((int)relation).CompareTo((int)IntervalRelation.Equal);
        }
    }
}
