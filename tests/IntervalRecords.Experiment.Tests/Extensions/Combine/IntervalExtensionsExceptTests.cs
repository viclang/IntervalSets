using FluentAssertions.Execution;
using IntervalRecords.Experiment.Extensions;
using IntervalRecords.Experiment.Tests.TestData;

namespace IntervalRecords.Experiment.Tests.Extensions.Combine;
public class IntervalExtensionsExceptTests
{
    [Theory]
    [InlineData("(1, 2)")]
    [InlineData("[3, 4)")]
    [InlineData("(5, 6]")]
    [InlineData("[7, 8]")]
    public void Empty_if_intervals_are_equal(string interval)
    {
        var left = Interval<int>.Parse(interval);
        var right = left with { };

        var actual = left.Except(right);

        actual.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(Int32DisjointClassData))]
    public void Return_left_and_right_if_not_overlapping(string leftInterval, string rightInterval, IntervalRelation _)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Except(right);

        actual.Should().BeEquivalentTo(new[] { left, right });
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
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);
        var expected = Interval<int>.Parse(expectedInterval);

        var actual = left.Except(right).ToList();

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
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);
        var expected = Interval<int>.ParseAll(expectedInterval);

        var actual = left.Except(right);

        using (new AssertionScope())
        {
            actual.Count().Should().Be(2);
            actual.Should().BeEquivalentTo(expected);
        }
    }

    [Theory]
    // Complex case with multiple overlapping and non-overlapping intervals
    [InlineData("(1, 3], (2, 4], (5, 7], (6, 8], (9, 10]", "(1, 2], (4, 5], (7, 8], (9, 10]")]
    // Mixed case with overlapping, non-overlapping, and adjacent intervals
    [InlineData("(1, 2], (2, 3], (4, 6], (5, 7], (8, 9]", "(1, 2], (2, 3], (4, 5], (7, 8], (8, 9]")]
    // Overlapping and adjacent intervals
    [InlineData("(1, 5], (2, 6], (6, 7], (8, 10]", "(1, 2], (6, 7], (8, 10]")]
    public void List_of_overlapping_intervals_returns_except(string intervalString, string expected)
    {
        var intervals = Interval<int>.ParseAll(intervalString);
        
        var actual = intervals.ExceptOverlap();

        string.Join(", ", actual)
            .Should()
            .BeEquivalentTo(expected);
    }
}
