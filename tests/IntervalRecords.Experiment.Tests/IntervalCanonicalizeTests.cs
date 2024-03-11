using IntervalRecords.Experiment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Tests;
public class IntervalCanonicalizeTests
{
    [Theory]
    [InlineData("[1, 2]", BoundaryType.Open, "(0, 3)")]
    [InlineData("[1, 2]", BoundaryType.ClosedOpen, "[1, 3)")]
    [InlineData("[1, 2]", BoundaryType.OpenClosed, "(0, 2]")]
    [InlineData("[1, 3)", BoundaryType.Open, "(0, 3)")]
    [InlineData("[1, 3)", BoundaryType.OpenClosed, "(0, 2]")]
    [InlineData("[1, 3)", BoundaryType.Closed, "[1, 2]")]
    [InlineData("(0, 2]", BoundaryType.Open, "(0, 3)")]
    [InlineData("(0, 2]", BoundaryType.ClosedOpen, "[1, 3)")]
    [InlineData("(0, 2]", BoundaryType.Closed, "[1, 2]")]
    [InlineData("(0, 3)", BoundaryType.ClosedOpen, "[1, 3)")]
    [InlineData("(0, 3)", BoundaryType.OpenClosed, "(0, 2]")]
    [InlineData("(0, 3)", BoundaryType.Closed, "[1, 2]")]
    public void Bounded_interval_returns_canonicalized_interval(string intervalString, BoundaryType boundaryType, string expectedIntervalString)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize(boundaryType, 1);

        actual.ToString().Should().Be(expectedIntervalString);
    }

    [Theory]
    [InlineData("[1, 2]", BoundaryType.Closed)]
    [InlineData("(0, 3)", BoundaryType.Open)]
    [InlineData("[1, 3)", BoundaryType.ClosedOpen)]
    [InlineData("(0, 2]", BoundaryType.OpenClosed)]
    public void Equal_boundaryType_returns_Equal_interval(string intervalString, BoundaryType boundaryType)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize(boundaryType, 1);

        actual.Should().Be(interval);
    }

    [Theory]
    [InlineData("[1,2)")]
    [InlineData("(1,2]")]
    [InlineData("(0,3)")]
    public void Closure_and_canonicalize_closed_are_equal(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);
        var expected = interval.Canonicalize(BoundaryType.Closed, 1);

        var actual = interval.Closure(1);

        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("[1,2]")]
    [InlineData("[1,2)")]
    [InlineData("(1,2]")]
    public void Interior_and_canonicalize_open_are_equal(string intervalString)
    {
        var interval = Interval<int>.Parse(intervalString);
        var expected = interval.Canonicalize(BoundaryType.Open, 1);

        var actual = interval.Interior(1);

        actual.Should().Be(expected);

    }
}
