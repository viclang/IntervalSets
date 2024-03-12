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

    [Theory]
    [InlineData("[1, 3]", "[3, 5]", "[1, 5]")] /// <see cref="IntervalRelation.Meets"/>
    [InlineData("(1, 3)", "(2, 5)", "(1, 5)")] /// <see cref="IntervalRelation.Overlaps"/>
    [InlineData("(1, 3)", "(1, 5)", "(1, 5)")] /// <see cref="IntervalRelation.Starts"/>
    [InlineData("(2, 3)", "(1, 5)", "(1, 5)")] /// <see cref="IntervalRelation.ContainedBy"/>
    [InlineData("(2, 5)", "(1, 5)", "(1, 5)")] /// <see cref="IntervalRelation.Finishes"/>
    [InlineData("(2, 5)", "(2, 5)", "(2, 5)")] /// <see cref="IntervalRelation.Equal"/>
    [InlineData("(1, 5)", "(2, 5)", "(1, 5)")] /// <see cref="IntervalRelation.FinishedBy"/>
    [InlineData("(1, 5)", "(2, 3)", "(1, 5)")] /// <see cref="IntervalRelation.Contains"/>
    [InlineData("(1, 5)", "(1, 3)", "(1, 5)")] /// <see cref="IntervalRelation.StartedBy"/>
    [InlineData("(2, 5)", "(1, 3)", "(1, 5)")] /// <see cref="IntervalRelation.OverlappedBy"/>
    [InlineData("[3, 5]", "[1, 3]", "[1, 5]")] /// <see cref="IntervalRelation.MetBy"/>
    public void Connected_intervals_returns_hull(string leftInterval, string rightInterval, string expected)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Hull(right);

        actual!.ToString().Should().Be(expected);
    }
}
