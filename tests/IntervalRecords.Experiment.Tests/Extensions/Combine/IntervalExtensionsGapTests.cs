using FluentAssertions.Execution;
using IntervalRecords.Experiment.Extensions;
using IntervalRecords.Experiment.Tests.TestData;

namespace IntervalRecords.Experiment.Tests.Extensions.Combine;
public class IntervalExtensionsGapTests
{
    [Theory]
    [ClassData(typeof(Int32ConnectedClassData))]
    public void Connected_intervals_returns_null(string leftInterval, string rightInterval, IntervalRelation _)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Gap(right);

        actual.Should().BeNull();
    }

    [Theory]
    [InlineData("[1, 3]", "[4, 5]", "(3, 4)")] /// <see cref="IntervalRelation.Before"/>
    [InlineData("(1, 3)", "(4, 5)", "[3, 4]")]
    [InlineData("[1, 3)", "[4, 5)", "[3, 4)")]
    [InlineData("(1, 3]", "(4, 5]", "(3, 4]")]
    [InlineData("[4, 5]", "[1, 3]", "(3, 4)")] /// <see cref="IntervalRelation.After"/>
    [InlineData("(4, 5)", "(1, 3)", "[3, 4]")]
    [InlineData("[4, 5)", "[1, 3)", "[3, 4)")]
    [InlineData("(4, 5]", "(1, 3]", "(3, 4]")]
    public void Disjoint_intervals_returns_gap(string leftInterval, string rightInterval, string expected)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Gap(right);

        actual!.ToString().Should().Be(expected);
    }
}
