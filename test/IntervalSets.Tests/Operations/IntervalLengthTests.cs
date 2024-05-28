using FluentAssertions;
using IntervalSets.Operations;
using IntervalSets.Types;

namespace IntervalSets.Tests.Operations;
public class IntervalLengthTests
{
    [Theory]
    [InlineData("[1, 3]", 2)]
    [InlineData("(1, 3]", 1)]
    [InlineData("[1, 3)", 1)]
    [InlineData("(1, 3)", 0)]
    [InlineData("[4, 6]", 2)]
    [InlineData("(4, 6]", 1)]
    [InlineData("[4, 6)", 1)]
    [InlineData("(4, 6)", 0)]
    public void Interval_length(string intervalString, int expectedLength)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Length();

        actual.Should().Be(expectedLength);
    }

    [Theory]
    [InlineData("(, 3]")]
    [InlineData("(, 3)")]
    [InlineData("[1, )")]
    [InlineData("(1, )")]
    [InlineData("(, )")]
    public void HalfBounded_or_unbounded_interval_length_throws_exception(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = () => interval.Length();

        actual.Should().ThrowExactly<InvalidOperationException>();
    }
}
