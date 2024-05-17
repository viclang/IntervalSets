using FluentAssertions;
using IntervalSet.Types;
using System.Diagnostics;

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
        //var interval = IntervalParse.ParseBounded<int>(testData);

        //interval.Start.Should().Be(expectedStart);
        //interval.End.Should().Be(expectedEnd);
        //interval.IntervalType.Should().Be(expectedIntervalType);
    }
}
