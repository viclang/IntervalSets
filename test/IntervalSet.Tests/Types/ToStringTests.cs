using FluentAssertions;
using IntervalSet.Types;

namespace IntervalSet.Tests.Types;
public class ToStringTests
{
    [Theory]
    [InlineData("(-Infinity, Infinity)")]
    [InlineData("[1, 2]")]
    [InlineData("[3, 4]")]
    [InlineData("[1, 2)")]
    [InlineData("[3, 4)")]
    [InlineData("(1, 2]")]
    [InlineData("(3, 4]")]
    [InlineData("(1, 2)")]
    [InlineData("(3, 4)")]
    public void ToString_ShouldBeExpected(string intervalString)
    {
        // Arrange
        var interval = Interval<int>.Parse(intervalString);

        // Act
        var actual = interval.ToString();

        // Assert
        actual.Should().Be(intervalString);
    }
}
