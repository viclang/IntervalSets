using IntervalRecords.Extensions;

namespace IntervalRecords.Tests.Extensions
{
    public class GapsTests
    {
        [Theory]
        [InlineData("[1, 3]", "[3, 4]")] // Ascending
        [InlineData("[1, 3)", "[3, 4)")]
        [InlineData("(1, 3]", "(3, 4]")]
        [InlineData("(1, 4)", "(3, 4)")]
        [InlineData("[3, 4]", "[1, 3]")] // Descending
        [InlineData("[3, 4)", "[1, 3)")]
        [InlineData("(3, 4]", "(1, 3]")]
        [InlineData("(3, 4)", "(1, 4)")]
        public void Connected_intervals_should_return_null(string leftInterval, string rightInterval)
        {
            var left = IntervalParser.Parse<int>(leftInterval);
            var right = IntervalParser.Parse<int>(rightInterval);

            var actual = left.Gap(right);

            actual.Should().BeNull();
        }

        [Theory]
        [InlineData("[1, 2]", "[3, 4]", "(2, 3)")] // Ascending
        [InlineData("[1, 2)", "[3, 4)", "[2, 3)")]
        [InlineData("(1, 2]", "(3, 4]", "(2, 3]")]
        [InlineData("(1, 3)", "(3, 4)", "[3, 3]")]
        [InlineData("[3, 4]", "[1, 2]", "(2, 3)")] // Descending
        [InlineData("[3, 4)", "[1, 2)", "[2, 3)")]
        [InlineData("(3, 4]", "(1, 2]", "(2, 3]")]
        [InlineData("(3, 4)", "(1, 3)", "[3, 3]")]
        public void Disjoint_intervals_should_return_expected_gap(string leftInterval, string rightInterval, string expectedInterval)
        {
            var left = IntervalParser.Parse<int>(leftInterval);
            var right = IntervalParser.Parse<int>(rightInterval);
            var expected = IntervalParser.Parse<int>(expectedInterval);

            var actual = left.Gap(right);

            actual.Should()
                .NotBeNull()
                .And
                .Be(expected);
        }
    }
}
