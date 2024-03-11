using IntervalRecords.Experiment.Extensions;

namespace IntervalRecords.Experiment.Tests;
public class IntervalLengthTests
{

    [Theory]
    [InlineData("[1, 2]", 1)]
    [InlineData("[1, 2)", 1)]
    [InlineData("(1, 2]", 1)]
    [InlineData("(1, 2)", 1)]
    [InlineData("[1, 3]", 2)]
    [InlineData("[1, 3)", 2)]
    [InlineData("(1, 3]", 2)]
    [InlineData("(1, 3)", 2)]
    public void Bounded_interval_returns_length(string intervalString, int expected)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Length();

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("(0,0)")]
    [InlineData("(1,1)")]
    public void Empty_interval_returns_zero(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Length();

        actual.Should().Be(0);

    }

    [Theory]
    [InlineData("(,)")]
    [InlineData("[1,)")]
    [InlineData("(,1]")]
    public void Unbounded_interval_returns_null(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Length();

        actual.Should().BeNull();
    }
}
