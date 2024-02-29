using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Tests;
public class IntervalContainsTests
{

    [Theory]
    [InlineData("[1, 3]", 1)]
    [InlineData("[1, 3]", 3)]
    [InlineData("(1, 3]", 2)]
    [InlineData("(1, 3]", 3)]
    [InlineData("[1, 3)", 2)]
    [InlineData("[1, 3)", 1)]
    [InlineData("(1, 3)", 2)]
    public void Interval_Contains_Value(string intervalString, int value)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Contains(value);        

        actual.Should().BeTrue();
    }


    [Theory]
    [InlineData("[1, 3]", 0)]
    [InlineData("[1, 3]", 4)]
    [InlineData("(1, 3]", 1)]
    [InlineData("(1, 3]", 4)]
    [InlineData("[1, 3)", 3)]
    [InlineData("[1, 3)", 0)]
    [InlineData("(1, 3)", 3)]
    [InlineData("(1, 3)", 1)]
    public void Interval_does_not_contains_value(string intervalString, int value)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Contains(value);

        actual.Should().BeFalse();
    }

}
