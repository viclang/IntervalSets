using IntervalRecords.Experiment.Extensions;

namespace IntervalRecords.Experiment.Tests.Extensions.Calculate;
public class IntervalExtensionsRadiusTests
{
    [Theory]
    [InlineData("[1, 3]", 1)]
    [InlineData("[1, 3)", 1)]
    [InlineData("(1, 3]", 1)]
    [InlineData("(1, 3)", 1)]
    [InlineData("[1, 5]", 2)]
    [InlineData("[1, 5)", 2)]
    [InlineData("(1, 5]", 2)]
    [InlineData("(1, 5)", 2)]
    public void Bounded_interval_returns_radius(string intervalString, int expected)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Radius();

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("(0,0)")]
    [InlineData("(1,1)")]
    public void Empty_interval_returns_null(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Radius();

        actual.Should().BeNull();

    }

    [Theory]
    [InlineData("(,)")]
    [InlineData("[1,)")]
    [InlineData("(,1]")]
    public void Unbounded_interval_returns_null(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Radius();

        actual.Should().BeNull();
    }
}
