namespace IntervalRecords.Experiment.Tests.Extensions.Combine;
public class IntervalExtensionsHullTests
{
    [Theory]
    [InlineData("[1, 3]", "[4, 5]", "[1, 5]")] /// <see cref="IntervalRelation.Before"/>
    [InlineData("(1, 3)", "(4, 5)", "(1, 5)")]
    [InlineData("[1, 3)", "[4, 5)", "[1, 5)")]
    [InlineData("(1, 3]", "(4, 5]", "(1, 5]")]
    [InlineData("[4, 5]", "[1, 3]", "[1, 5]")] /// <see cref="IntervalRelation.After"/>
    [InlineData("(4, 5)", "(1, 3)", "(1, 5)")]
    [InlineData("[4, 5)", "[1, 3)", "[1, 5)")]
    [InlineData("(4, 5]", "(1, 3]", "(1, 5]")]
    public void Hull_of_disjoint_intervals(string leftInterval, string rightInterval, string expected)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Hull(right);

        actual!.ToString().Should().Be(expected);
    }
}
