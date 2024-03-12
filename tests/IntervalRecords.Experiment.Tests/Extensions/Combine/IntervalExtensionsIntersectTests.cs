using IntervalRecords.Experiment.Extensions;
using System.Diagnostics;

namespace IntervalRecords.Experiment.Tests.Extensions.Combine;
public class IntervalExtensionsIntersectTests
{
    [Theory]
    [InlineData("[1, 3]", "[3, 5]", "[3, 3]")] /// <see cref="IntervalRelation.Meets"/>
    [InlineData("(1, 3)", "(2, 5)", "[2, 3]")] /// <see cref="IntervalRelation.Overlaps"/>
    [InlineData("(1, 3)", "(1, 5)", "(1, 3]")] /// <see cref="IntervalRelation.Starts"/>
    [InlineData("(2, 3)", "(1, 5)", "[2, 3]")] /// <see cref="IntervalRelation.ContainedBy"/>
    [InlineData("(2, 5)", "(1, 5)", "[2, 5)")] /// <see cref="IntervalRelation.Finishes"/>
    [InlineData("(1, 5)", "(2, 5)", "[2, 5)")] /// <see cref="IntervalRelation.FinishedBy"/>
    [InlineData("(1, 5)", "(2, 3)", "[2, 3]")] /// <see cref="IntervalRelation.Contains"/>
    [InlineData("(1, 5)", "(1, 3)", "(1, 3]")] /// <see cref="IntervalRelation.StartedBy"/>
    [InlineData("(2, 5)", "(1, 3)", "[2, 3]")] /// <see cref="IntervalRelation.OverlappedBy"/>
    [InlineData("[3, 5]", "[1, 3]", "[3, 3]")] /// <see cref="IntervalRelation.MetBy"/>
    public void Overlapping_intervals_return_intersect(string leftInterval, string rightInterval, string expected)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Intersect(right);

        actual!.ToString().Should().Be(expected);
    }
}
