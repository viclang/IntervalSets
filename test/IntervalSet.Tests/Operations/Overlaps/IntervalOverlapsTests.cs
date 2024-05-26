using FluentAssertions;
using IntervalSet.Operations;
using IntervalSet.Tests.TestData;
using IntervalSet.Types;

namespace IntervalSet.Tests.Operations.Overlaps;
public class IntervalOverlapsTests
{

    [Theory]
    [ClassData(typeof(Int32OverlappingClassData))]
    public void GivenTwoOverlappingIntervals_WhenComparing_ReturnsTrue(string left, string right, IntervalRelation _)
    {
        var leftInterval = Interval<int>.Parse(left);
        var rightInterval = Interval<int>.Parse(right);
        var actual = leftInterval.Overlaps(rightInterval);

        actual.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(Int32NonOverlappingClassData))]
    public void GivenTwoNonOverlappingIntervals_WhenComparing_ReturnsFalse(string left, string right, IntervalRelation _)
    {
        var leftInterval = Interval<int>.Parse(left);
        var rightInterval = Interval<int>.Parse(right);

        var actual = leftInterval.Overlaps(rightInterval);

        actual.Should().BeFalse();
    }
}
