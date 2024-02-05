using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntervalRecords.Experiment.Tests.TestData;

namespace IntervalRecords.Experiment.Tests;
public class IntervalConnectedTests
{
    [Theory]
    [ClassData(typeof(Int32ConnectedClassData))]
    public void GivenTwoConnectedIntervals_WhenComparing_ReturnsTrue(string left, string right, IntervalRelation _)
    {
        var leftInterval = Interval<int>.Parse(left);
        var rightInterval = Interval<int>.Parse(right);

        var actual = leftInterval.IsConnected(rightInterval);

        actual.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(Int32DisjointClassData))]
    public void GivenTwoDisjointIntervals_WhenComparing_ReturnsTrue(string left, string right, IntervalRelation _)
    {
        var leftInterval = Interval<int>.Parse(left);
        var rightInterval = Interval<int>.Parse(right);

        var actual = leftInterval.IsConnected(rightInterval);

        actual.Should().BeFalse();
    }
}
