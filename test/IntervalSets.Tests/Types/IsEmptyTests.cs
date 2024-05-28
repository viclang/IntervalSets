using FluentAssertions;
using IntervalSets.Types;

namespace IntervalSets.Tests.Types;
public class IsEmptyTests
{

    [Theory]
    [InlineData("(2, 1)")]
    [InlineData("(2, 1]")]
    [InlineData("[2, 1)")]
    [InlineData("[2, 1]")]
    [InlineData("(0, 0)")]
    [InlineData("(0, 0]")]
    [InlineData("[0, 0)")]
    public void Interval_is_empty(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        interval.IsEmpty.Should().BeTrue();
    }
}
