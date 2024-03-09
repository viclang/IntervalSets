using IntervalRecords.Experiment.Extensions;
using IntervalRecords.Experiment.Tests.TestData;

namespace IntervalRecords.Experiment.Tests;
public class IntervalRelationTests
{
    [Theory]
    [ClassData(typeof(Int32IntervalRelationClassData))]
    public void GivenTwoIntervals_WhenComparing_ReturnsRelation(string left, string right, IntervalRelation expected)
    {
        var leftInterval = Interval<int>.Parse(left);
        var rightInterval = Interval<int>.Parse(right);
        var actual = leftInterval.GetRelation(rightInterval);

        actual.Should().Be(expected);
    }

}
