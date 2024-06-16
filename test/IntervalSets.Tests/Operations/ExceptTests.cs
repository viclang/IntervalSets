using FluentAssertions;
using FluentAssertions.Execution;
using IntervalSets.Operations;
using IntervalSets.Tests.TestData;
using IntervalSets.Types;

namespace IntervalSets.Tests.Operations;
public class ExceptTests
{
    [Theory]
    [InlineData("(1, 2)")]
    [InlineData("[3, 4)")]
    [InlineData("(5, 6]")]
    [InlineData("[7, 8]")]
    public void Empty_if_intervals_are_equal(string interval)
    {
        var left = Interval<int>.Parse(interval);
        var right = left;

        var actual = left.Except(right);

        actual.Should().BeEmpty();
    }

    [Theory]
    [ClassData(typeof(Int32DisjointClassData))]
    public void List_of_inputs_if_not_overlapping(string left, string right, IntervalRelation _)
    {
        var leftInterval = Interval<int>.Parse(left);
        var rightInterval = Interval<int>.Parse(right);

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
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);
        var expected = Interval<int>.Parse(expectedInterval);

        var actual = left.Except(right);

        using (new AssertionScope())
        {
            actual.Count().Should().Be(1);
            actual.Should().AllBeEquivalentTo(expected);
        }
    }

    [Theory]
    [InlineData("(1, 4]", "(2, 5]", "(1, 2];(4, 5]")]
    [InlineData("(2, 5]", "(1, 4]", "(1, 2];(4, 5]")]
    [InlineData("[1, 4)", "[2, 5)", "[1, 2);[4, 5)")]
    [InlineData("[2, 5)", "[1, 4)", "[1, 2);[4, 5)")]
    [InlineData("[1, 4)", "[3, 5)", "[1, 3);[4, 5)")]
    public void Two_differential_endpoints_returns_two_disconnected_intervals(string leftInterval, string rightInterval, string expectedIntervals)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);
        var expected = expectedIntervals.Split(';').Select(s => Interval<int>.Parse(s));

        var actual = left.Except(right);

        using (new AssertionScope())
        {
            actual.Count().Should().Be(2);
            actual.Should().BeEquivalentTo(expected);
        }

    }
}
