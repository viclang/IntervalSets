using FluentAssertions;
using IntervalSets.Operations;
using IntervalSets.Tests.TestData;
using IntervalSets.Types;

namespace IntervalSets.Tests.Operations.Overlaps;
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

public abstract class IntervalOverlapsTests<L, R>
    where L : struct, IBound
    where R : struct, IBound
{
    [Theory]
    [InlineData(1, 2, 3, 4)]
    [InlineData(1, 5, 4, 6)]
    [InlineData(1, 5, 5, 6)]
    [InlineData(1, 5, 0, 3)]
    [InlineData(1, 5, 3, 4)]
    [InlineData(1, 5, 1, 5)]
    [InlineData(1, 5, 0, 6)]
    [InlineData(1, 5, 2, 4)]
    public void TypedInterval_overlaps_is_equivalent_to_Interval(int leftStart, int leftEnd, int rightStart, int rightEnd)
    {
        var leftInterval = new Interval<int, L, R>(leftStart, leftEnd);
        var rightInterval = new Interval<int, L, R>(rightStart, rightEnd);

        var expectedResult = ((Interval<int>)leftInterval).Overlaps(rightInterval);

        var actual = leftInterval switch
        {
            Interval<int, Open, Open> openLeftInterval => openLeftInterval.Overlaps((Interval<int, Open, Open>)(object)rightInterval),
            Interval<int, Open, Closed> openClosedLeftInterval => openClosedLeftInterval.Overlaps((Interval<int, Open, Closed>)(object)rightInterval),
            Interval<int, Closed, Open> ClosedOpenLeftInterval => ClosedOpenLeftInterval.Overlaps((Interval<int, Closed, Open>)(object)rightInterval),
            Interval<int, Closed, Closed> closedLeftInterval => closedLeftInterval.Overlaps((Interval<int, Closed, Closed>)(object)rightInterval),
            Interval<int, Open, Unbounded> openUnboundedLeftInterval => openUnboundedLeftInterval.Overlaps((Interval<int, Open, Unbounded>)(object)rightInterval),
            Interval<int, Closed, Unbounded> closedUnboundedLeftInterval => closedUnboundedLeftInterval.Overlaps((Interval<int, Closed, Unbounded>)(object)rightInterval),
            Interval<int, Unbounded, Open> unboundedOpenLeftInterval => unboundedOpenLeftInterval.Overlaps((Interval<int, Unbounded, Open>)(object)rightInterval),
            Interval<int, Unbounded, Closed> unboundedClosedLeftInterval => unboundedClosedLeftInterval.Overlaps((Interval<int, Unbounded, Closed>)(object)rightInterval),
            Interval<int, Unbounded, Unbounded> unboundedLeftInterval => unboundedLeftInterval.Overlaps((Interval<int, Unbounded, Unbounded>)(object)rightInterval),
            _ => throw new NotImplementedException()
        };

        actual.Should().Be(expectedResult);
    }
}

public class IntervalOverlapsTestsClosed : IntervalOverlapsTests<Closed, Closed> { }

public class IntervalOverlapsTestsTestsOpen : IntervalOverlapsTests<Open, Open> { }

public class IntervalOverlapsTestsOpenClosed : IntervalOverlapsTests<Open, Closed> { }

public class IntervalOverlapsTestsClosedOpen : IntervalOverlapsTests<Closed, Open> { }

public class IntervalOverlapsTestsOpenUnbounded : IntervalOverlapsTests<Open, Unbounded> { }

public class IntervalOverlapsTestsClosedUnbounded : IntervalOverlapsTests<Closed, Unbounded> { }

public class IntervalOverlapsTestsUnboundedOpen : IntervalOverlapsTests<Unbounded, Open> { }

public class IntervalOverlapsTestsUnboundedClosed : IntervalOverlapsTests<Unbounded, Closed> { }

public class IntervalOverlapsTestsUnbounded : IntervalOverlapsTests<Unbounded, Unbounded> { }

