using FluentAssertions;
using IntervalSet.Operations;
using IntervalSet.Types;

namespace IntervalSet.Tests.Operations.Canonicalize;
public class BoundedCanonicalizeExtensionsTests
{
    [Fact]
    public void Given_When_Then()
    {
        var interval = new Interval<int>(1, 3, IntervalType.Open);

        var closedInterval = interval.Canonicalize<int, Closed, Closed>();

        closedInterval.Start.Should().Be(2);
        closedInterval.End.Should().Be(2);
        closedInterval.StartBound.Should().Be(Bound.Closed);
        closedInterval.EndBound.Should().Be(Bound.Closed);
    }
}
