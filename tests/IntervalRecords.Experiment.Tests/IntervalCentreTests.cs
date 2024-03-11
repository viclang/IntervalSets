using IntervalRecords.Experiment.Extensions;

namespace IntervalRecords.Experiment.Tests;
public class IntervalCentreTests
{
    [Theory]
    [InlineData("[1, 3]", 2)]
    [InlineData("[1, 3)", 2)]
    [InlineData("(1, 3]", 2)]
    [InlineData("(1, 3)", 2)]
    [InlineData("[1, 5]", 3)]
    [InlineData("[1, 5)", 3)]
    [InlineData("(1, 5]", 3)]
    [InlineData("(1, 5)", 3)]
    public void Bounded_interval_returns_centre(string intervalString, int expected)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Centre();

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("(0,0)")]
    [InlineData("(1,1)")]
    public void Empty_interval_returns_null(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Centre();

        actual.Should().BeNull();

    }

    [Theory]
    [InlineData("(,)")]
    [InlineData("[1,)")]
    [InlineData("(,1]")]
    public void Unbounded_interval_returns_null(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Centre();

        actual.Should().BeNull();
    }
}
