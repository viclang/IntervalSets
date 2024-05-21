using IntervalSet.Types;

namespace IntervalSet.Tests.Types;
public class IntervalParseTests
{
    [Theory]
    [InlineData("[1, 2]", 1, 2, IntervalType.Closed)]
    [InlineData("[, 1]", 0, 1, IntervalType.Closed)]
    [InlineData("(3, 4)", 3, 4, IntervalType.Open)]
    [InlineData("(3, 4]", 3, 4, IntervalType.OpenClosed)]
    [InlineData("[3, 4  )", 3, 4, IntervalType.ClosedOpen)]
    public void Given_When_Then(string testData, int expectedStart, int expectedEnd, IntervalType expectedIntervalType)
    {

        var test = OpenInterval<int>.TryParse("(1,)", null, out var result);
    }
}
