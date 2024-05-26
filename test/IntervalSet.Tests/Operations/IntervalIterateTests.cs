using FluentAssertions;
using IntervalSet.Operations;
using IntervalSet.Types;

namespace IntervalSet.Tests.Operations;
public class IntervalIterateTests
{
    [Theory]
    [InlineData("[1,10]", 2, new int[] { 1, 3, 5, 7, 9 })]
    [InlineData("[1,5]", 1, new int[] { 1, 2, 3, 4, 5 })]
    [InlineData("[1,5)", 1, new int[] { 1, 2, 3, 4 })]
    [InlineData("(1,5]", 1, new int[] { 2, 3, 4, 5 })]
    [InlineData("(1,5)", 1, new int[] { 2, 3, 4 })]
    public void Interval_iterate_returns_list(string intervalString, int step, int[] expectedResult)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Iterate(i => i+step);

        actual.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData("(,1]")]
    [InlineData("[1,)")]
    [InlineData("(, )")]
    [InlineData("(0, 0)")]
    public void Empty_half_bounded_or_unbounded_interval_returns_empty_list(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Iterate(i => i + 1);

        actual.Should().BeEmpty();
    }
}
