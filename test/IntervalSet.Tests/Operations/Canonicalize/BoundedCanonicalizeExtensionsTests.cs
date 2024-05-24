using FluentAssertions;
using FluentAssertions.Execution;
using IntervalSet.Operations;
using IntervalSet.Tests.Types;
using IntervalSet.Types;

namespace IntervalSet.Tests.Operations.Canonicalize;
public class BoundedCanonicalizeExtensionsTests
{
    [Theory]
    [InlineData("(1, 3)", "(1, 3)")]
    [InlineData("(2, 4)", "(2, 4)")]
    [InlineData("(1, 3]", "(1, 4)")]
    [InlineData("(2, 4]", "(2, 5)")]
    [InlineData("[1, 3)", "(0, 3)")]
    [InlineData("[2, 4)", "(1, 4)")]
    [InlineData("[1, 3]", "(0, 4)")]
    [InlineData("[2, 4]", "(1, 5)")]
    public void Bounded_to_Open(string intervalString, string expectedInterval)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize<int, Open, Open>();

        actual.ToString()
            .Should()
            .Be(expectedInterval);
    }


    [Theory]
    [InlineData("(1, 3)", "(1, 2]")]
    [InlineData("(2, 4)", "(2, 3]")]
    [InlineData("(1, 3]", "(1, 3]")]
    [InlineData("(2, 4]", "(2, 4]")]
    [InlineData("[1, 3)", "(0, 2]")]
    [InlineData("[2, 4)", "(1, 3]")]
    [InlineData("[1, 3]", "(0, 3]")]
    [InlineData("[2, 4]", "(1, 4]")]
    public void Bounded_to_OpenClosed(string intervalString, string expectedInterval)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize<int, Open, Closed>();

        actual.ToString()
            .Should()
            .Be(expectedInterval);
    }

    [Theory]
    [InlineData("(1, 3)", "[2, 3)")]
    [InlineData("(2, 4)", "[3, 4)")]
    [InlineData("(1, 3]", "[2, 4)")]
    [InlineData("(2, 4]", "[3, 5)")]
    [InlineData("[1, 3)", "[1, 3)")]
    [InlineData("[2, 4)", "[2, 4)")]
    [InlineData("[1, 3]", "[1, 4)")]
    [InlineData("[2, 4]", "[2, 5)")]
    public void Bounded_to_ClosedOpen(string intervalString, string expectedInterval)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize<int, Closed, Open>();

        actual.ToString()
            .Should()
            .Be(expectedInterval);
    }

    [Theory]
    [InlineData("(1, 3)", "[2, 2]")]
    [InlineData("(2, 4)", "[3, 3]")]
    [InlineData("(1, 3]", "[2, 3]")]
    [InlineData("(2, 4]", "[3, 4]")]
    [InlineData("[1, 3)", "[1, 2]")]
    [InlineData("[2, 4)", "[2, 3]")]
    [InlineData("[1, 3]", "[1, 3]")]
    [InlineData("[2, 4]", "[2, 4]")]
    public void Bounded_to_Closed(string intervalString, string expectedInterval)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize<int, Closed, Closed>();

        actual.ToString()
            .Should()
            .Be(expectedInterval);
    }
    
    [Theory]
    [InlineData("(-Infinity, 1)", "(-Infinity, 0]")]
    [InlineData("(-Infinity, 2)", "(-Infinity, 1]")]
    [InlineData("(-Infinity, 1]", "(-Infinity, 1]")]
    [InlineData("(-Infinity, 2]", "(-Infinity, 2]")]
    public void RightBounded_to_Closed(string intervalString, string expectedInterval)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize<int, Unbounded, Closed>();

        actual.ToString()
            .Should()
            .Be(expectedInterval);
    }

    [Theory]
    [InlineData("(-Infinity, 1)", "(-Infinity, 1)")]
    [InlineData("(-Infinity, 2)", "(-Infinity, 2)")]
    [InlineData("(-Infinity, 1]", "(-Infinity, 2)")]
    [InlineData("(-Infinity, 2]", "(-Infinity, 3)")]
    public void RightBounded_to_Open(string intervalString, string expectedInterval)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize<int, Unbounded, Open>();

        actual.ToString()
            .Should()
            .Be(expectedInterval);
    }

    [Theory]
    [InlineData("(-Infinity, 1)", "(-Infinity, 0]")]
    [InlineData("(-Infinity, 2)", "(-Infinity, 1]")]
    [InlineData("(-Infinity, 1]", "(-Infinity, 1]")]
    [InlineData("(-Infinity, 2]", "(-Infinity, 2]")]
    public void LeftBounded_to_Closed(string intervalString, string expectedInterval)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize<int, Unbounded, Closed>();

        actual.ToString()
            .Should()
            .Be(expectedInterval);
    }

    [Theory]
    [InlineData("(1, Infinity)", "(1, Infinity)")]
    [InlineData("(2, Infinity)", "(2, Infinity)")]
    [InlineData("[1, Infinity)", "(0, Infinity)")]
    [InlineData("[2, Infinity)", "(1, Infinity)")]
    public void LeftBounded_to_Open(string intervalString, string expectedInterval)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Canonicalize<int, Open, Unbounded>();

        actual.ToString()
            .Should()
            .Be(expectedInterval);
    }

    [Fact]
    public void Unbounded_to_unbounded_is_Equal()
    {
        var interval = new Interval<int>(0, 0, Bound.Unbounded, Bound.Unbounded);

        var actual = interval.Canonicalize<int, Unbounded, Unbounded>();

        actual.Should().Be(interval);
    }

    [Theory]
    [InlineData(Bound.Open, Bound.Unbounded)]
    [InlineData(Bound.Unbounded, Bound.Open)]
    [InlineData(Bound.Unbounded, Bound.Unbounded)]
    public void Unbounded_to_closed_is_not_allowed(Bound startBound, Bound endBound)
    {
        var interval = new Interval<int>(0, 0, startBound, endBound);

        var actual = () => interval.Canonicalize<int, Closed, Closed>();

        actual.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("Unable to canonicalize interval * to type Interval*.");
    }

    [Theory]
    [InlineData(Bound.Closed, Bound.Unbounded)]
    [InlineData(Bound.Unbounded, Bound.Closed)]
    [InlineData(Bound.Unbounded, Bound.Unbounded)]
    public void Unbounded_to_open_is_not_allowed(Bound startBound, Bound endBound)
    {
        var interval = new Interval<int>(0, 0, startBound, endBound);

        var actual = () => interval.Canonicalize<int, Open, Open>();

        actual.Should().ThrowExactly<InvalidOperationException>()
            .WithMessage("Unable to canonicalize interval * to type Interval*");
    }
}
