using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntervalRecords.Experiment.Tests.TestData;

namespace IntervalRecords.Experiment.Tests;
public class IntervalIsDisjointTests
{
    [Theory]
    [ClassData(typeof(Int32ConnectedClassData))]
    public void GivenTwoConnectedIntervals_WhenComparing_ReturnsFalse(string left, string right, IntervalRelation _)
    {
        var leftInterval = Interval<int>.Parse(left);
        var rightInterval = Interval<int>.Parse(right);

        var actual = leftInterval.IsDisjoint(rightInterval);

        actual.Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(Int32DisjointClassData))]
    public void GivenTwoDisjointIntervals_WhenComparing_ReturnsTrue(string left, string right, IntervalRelation _)
    {
        var leftInterval = Interval<int>.Parse(left);
        var rightInterval = Interval<int>.Parse(right);

        leftInterval.Compare(rightInterval);

        var actual = leftInterval.IsDisjoint(rightInterval);

        actual.Should().BeTrue();
    }
}
