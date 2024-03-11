using FluentAssertions.Execution;
using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData.ClassData;

namespace IntervalRecords.Tests.Extensions
{
    public class ExceptTests
    {
        [Theory]
        [InlineData("(1, 2)")]
        [InlineData("[3, 4)")]
        [InlineData("(5, 6]")]
        [InlineData("[7, 8]")]
        public void Empty_if_intervals_are_equal(string interval)
        {
            var left = IntervalParser.Parse<int>(interval);
            var right = left with { };

            var actual = left.Except(right);

            actual.Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(Int32DisjointClassData))]
        public void List_of_inputs_if_not_overlapping(string left, string right, IntervalRelation _)
        {
            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);

            var actual = leftInterval.Except(rightInterval);

            actual.Should().BeEquivalentTo(new[] { leftInterval, rightInterval });
        }

        [Theory]
        [InlineData("(1, 4]", "(2, 4]", "(1, 2]")] // Start is different
        [InlineData("(2, 4]", "(1, 4]", "(1, 2]")]
        [InlineData("[1, 4)", "[2, 4)", "[1, 2)")]
        [InlineData("[2, 4)", "[1, 4)", "[1, 2)")]
        [InlineData("(1, 4]", "(1, 5]", "(4, 5]")] // End is different
        [InlineData("(1, 5]", "(1, 4]", "(4, 5]")]
        [InlineData("[1, 4)", "[1, 5)", "[4, 5)")]
        [InlineData("[1, 5)", "[1, 4)", "[4, 5)")]
        public void One_differential_endpoint_returns_difference(string leftInterval, string rightInterval, string expectedInterval)
        {
            var left = IntervalParser.Parse<int>(leftInterval);
            var right = IntervalParser.Parse<int>(rightInterval);
            var expected = IntervalParser.Parse<int>(expectedInterval);

            var actual = left.Except(right);

            using (new AssertionScope())
            {
                actual.Count().Should().Be(1);
                actual.Should().AllBeEquivalentTo(expected);
            }
        }

        [Theory]
        [InlineData("(1, 4]", "(2, 5]", "(1, 2], (4, 5]")]
        [InlineData("(2, 5]", "(1, 4]", "(1, 2], (4, 5]")]
        [InlineData("[1, 4)", "[2, 5)", "[1, 2), [4, 5)")]
        [InlineData("[2, 5)", "[1, 4)", "[1, 2), [4, 5)")]
        [InlineData("[1, 4)", "[3, 5)", "[1, 3), [4, 5)")]
        public void Two_differential_endpoints_returns_two_disconnected_intervals(string leftInterval, string rightInterval, string expectedInterval)
        {
            var left = IntervalParser.Parse<int>(leftInterval);
            var right = IntervalParser.Parse<int>(rightInterval);
            var expected = IntervalParser.ParseAll<int>(expectedInterval);

            var actual = left.Except(right);

            using (new AssertionScope())
            {
                actual.Count().Should().Be(2);
                actual.Should().BeEquivalentTo(expected);
            }

        }
    }
}
