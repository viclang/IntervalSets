using FluentAssertions;
using IntervalSets.Operations;
using IntervalSets.Types;

namespace IntervalSets.Tests.Operations;
public class IntervalContainsExtensionsTests
{
    [Theory]
    [InlineData("[1, 3]", 1, true)]
    [InlineData("[1, 3]", 3, true)]
    [InlineData("[1, 3]", 0, false)]
    [InlineData("[1, 3]", 4, false)]
    [InlineData("(1, 3)", 1, false)]
    [InlineData("(1, 3)", 3, false)]
    [InlineData("(, 3]", int.MinValue, true)]
    [InlineData("(, 3]", int.MaxValue, false)]
    [InlineData("[1, )", int.MaxValue, true)]
    [InlineData("[1, )", int.MinValue, false)]
    public void Interval_contains(string intervalString, int value, bool expectedResult)
    {
        var interval = Interval<int>.Parse(intervalString);

        var actual = interval.Contains(value);

        actual.Should().Be(expectedResult);
    }
}

public abstract class IntervalContainsExtensionsTests<L, R>
    where L : struct, IBound
    where R : struct, IBound
{
    [Theory]
    [InlineData(1, 3, 1)]
    [InlineData(1, 3, 3)]
    [InlineData(1, 3, 0)]
    [InlineData(1, 3, 4)]
    [InlineData(1, 3, 2)]
    [InlineData(1, 3, int.MinValue)]
    [InlineData(1, 3, int.MaxValue)]
    public void TypedInterval_contains_is_equivalent_to_Interval(int start, int end, int value)
    {
        var interval = new Interval<int, L, R>(start, end);
        var expectedResult = new Interval<int>(start, end, L.Bound, R.Bound).Contains(value);

        var actual = interval switch
        {
            Interval<int, Open, Open> openInterval => openInterval.Contains(value),
            Interval<int, Open, Closed> openClosedInterval => openClosedInterval.Contains(value),
            Interval<int, Closed, Open> ClosedOpenInterval => ClosedOpenInterval.Contains(value),
            Interval<int, Closed, Closed> closedInterval => closedInterval.Contains(value),
            Interval<int, Open, Unbounded> openUnboundedInterval => openUnboundedInterval.Contains(value),
            Interval<int, Closed, Unbounded> closedUnboundedInterval => closedUnboundedInterval.Contains(value),
            Interval<int, Unbounded, Open> unboundedOpenInterval => unboundedOpenInterval.Contains(value),
            Interval<int, Unbounded, Closed> unboundedClosedInterval => unboundedClosedInterval.Contains(value),
            Interval<int, Unbounded, Unbounded> unboundedInterval => unboundedInterval.Contains(value),
            _ => throw new NotImplementedException()
        };

        actual.Should().Be(expectedResult);
    }
}

public class IntervalContainsExtensionsTestsClosed : IntervalContainsExtensionsTests<Closed, Closed> { }

public class IntervalContainsExtensionsTestsOpen : IntervalContainsExtensionsTests<Open, Open> { }

public class IntervalContainsExtensionsTestsOpenClosed : IntervalContainsExtensionsTests<Open, Closed> { }

public class IntervalContainsExtensionsTestsClosedOpen : IntervalContainsExtensionsTests<Closed, Open> { }

public class IntervalContainsExtensionsTestsOpenUnbounded : IntervalContainsExtensionsTests<Open, Unbounded> { }

public class IntervalContainsExtensionsTestsClosedUnbounded : IntervalContainsExtensionsTests<Closed, Unbounded> { }

public class IntervalContainsExtensionsTestsUnboundedOpen : IntervalContainsExtensionsTests<Unbounded, Open> { }

public class IntervalContainsExtensionsTestsUnboundedClosed : IntervalContainsExtensionsTests<Unbounded, Closed> { }

public class IntervalContainsExtensionsTestsUnbounded : IntervalContainsExtensionsTests<Unbounded, Unbounded> { }
