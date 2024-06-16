using FluentAssertions;
using FluentAssertions.Execution;
using IntervalSets.Types;

namespace IntervalSets.Tests.Types;
public class EmptyIntervalTests
{
    [Fact]
    public void Typed_closed_EmptyInterval_is_empty()
    {
        var emptyInterval = Interval<int, Closed, Closed>.Empty;

        using (new AssertionScope())
        {
            emptyInterval.IsEmpty.Should().BeTrue();
            emptyInterval.IsSingleton.Should().BeFalse();
            emptyInterval.StartBound.Should().Be(Bound.Closed);
            emptyInterval.EndBound.Should().Be(Bound.Closed);
        }
    }

    [Fact]
    public void Closed_EmptyInterval_is_empty()
    {
        var emptyInterval = new EmptyInterval<int>(IntervalType.Closed);

        using (new AssertionScope())
        {
            emptyInterval.IsEmpty.Should().BeTrue();
            emptyInterval.IsSingleton.Should().BeFalse();
            emptyInterval.StartBound.Should().Be(Bound.Closed);
            emptyInterval.EndBound.Should().Be(Bound.Closed);
        }
    }
}
