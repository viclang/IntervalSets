using FluentAssertions;

namespace IntervalSets.Types;
public class IsSingletonTests
{
    [Theory]
    [InlineData("[1,1]", true)]
    [InlineData("[1,1)", false)]
    [InlineData("(1,1]", false)]
    [InlineData("(1,1)", false)]
    [InlineData("[1,2]", false)]
    [InlineData("[1,2)", false)]
    [InlineData("(1,2]", false)]
    [InlineData("(1,2)", false)]
    public void IsSingleton_ShouldBeExpected(string intervalString, bool expected)
    {
        // Arrange
        var interval = Interval<int>.Parse(intervalString);

        // Act
        var actual = interval.IsSingleton;

        // Assert
        actual.Should().Be(expected);
    }
}
