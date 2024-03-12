namespace IntervalRecords.Experiment.Tests.Extensions.Combine;
public class IntervalExtensionsUnionTests
{
    [Theory]
    [InlineData("[1, 3]", "[4, 5]")] /// <see cref="IntervalRelation.Before"/>
    [InlineData("(1, 3)", "(4, 5)")]
    [InlineData("[1, 3)", "[4, 5)")]
    [InlineData("(1, 3]", "(4, 5]")]
    [InlineData("[4, 5]", "[1, 3]")] /// <see cref="IntervalRelation.After"/>
    [InlineData("(4, 5)", "(1, 3)")]
    [InlineData("[4, 5)", "[1, 3)")]
    [InlineData("(4, 5]", "(1, 3]")]
    public void Disjoint_intervals_returns_null(string leftInterval, string rightInterval)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Union(right);

        actual.Should().BeNull();
    }

    [Theory]
    [InlineData("[1, 3]", "[3, 5]")] /// <see cref="IntervalRelation.Meets"/>
    [InlineData("(1, 3)", "(2, 5)")] /// <see cref="IntervalRelation.Overlaps"/>
    [InlineData("(1, 3)", "(1, 5)")] /// <see cref="IntervalRelation.Starts"/>
    [InlineData("(2, 3)", "(1, 5)")] /// <see cref="IntervalRelation.ContainedBy"/>
    [InlineData("(2, 5)", "(1, 5)")] /// <see cref="IntervalRelation.Finishes"/>
    [InlineData("(2, 5)", "(2, 5)")] /// <see cref="IntervalRelation.Equal"/>
    [InlineData("(1, 5)", "(2, 5)")] /// <see cref="IntervalRelation.FinishedBy"/>
    [InlineData("(1, 5)", "(2, 3)")] /// <see cref="IntervalRelation.Contains"/>
    [InlineData("(1, 5)", "(1, 3)")] /// <see cref="IntervalRelation.StartedBy"/>
    [InlineData("(2, 5)", "(1, 3)")] /// <see cref="IntervalRelation.OverlappedBy"/>
    [InlineData("[3, 5]", "[1, 3]")] /// <see cref="IntervalRelation.MetBy"/>
    public void Union_and_hull_are_equal_for_connected_intervals(string leftInterval, string rightInterval)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);
        var expected = left.Hull(right);

        var actual = left.Union(right);

        actual.Should().Be(expected);
    }
}
