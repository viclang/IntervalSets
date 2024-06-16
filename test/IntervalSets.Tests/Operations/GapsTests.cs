using IntervalSets.Types;
using IntervalSets.Operations;
using FluentAssertions;

namespace IntervalSets.Tests.Operations;
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
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Gap(right);

        actual.IsEmpty.Should().BeTrue();
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
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);
        var expected = Interval<int>.Parse(expectedInterval);

        var actual = left.Gap(right);

        actual.Should()
            .NotBeNull()
            .And
            .Be(expected);
    }
}
