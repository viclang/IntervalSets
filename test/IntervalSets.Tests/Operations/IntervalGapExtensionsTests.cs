using IntervalSets.Types;
using IntervalSets.Operations;
using FluentAssertions;

namespace IntervalSets.Tests.Operations;
public class IntervalGapExtensionsTests
{
    [Theory]
    [InlineData("[1, 3]", "[3, 4]")] // Ascending
    [InlineData("[1, 3)", "[3, 4)")]
    [InlineData("(1, 3]", "(3, 4]")]
    [InlineData("(1, 4)", "(3, 4)")]
    [InlineData("[3, 4]", "[1, 3]")] // Descending
    [InlineData("[3, 4)", "[1, 3)")]
    [InlineData("(3, 4]", "(1, 3]")]
    [InlineData("(3, 4)", "(1, 4)")]
    public void Connected_intervals_should_return_null(string leftInterval, string rightInterval)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);

        var actual = left.Gap(right);

        actual.IsEmpty.Should().BeTrue();
    }

    [Theory]
    [InlineData("[1, 2]", "[3, 4]", "(2, 3)")] // Ascending
    [InlineData("[1, 2)", "[3, 4)", "[2, 3)")]
    [InlineData("(1, 2]", "(3, 4]", "(2, 3]")]
    [InlineData("(1, 3)", "(3, 4)", "[3, 3]")]
    [InlineData("[3, 4]", "[1, 2]", "(2, 3)")] // Descending
    [InlineData("[3, 4)", "[1, 2)", "[2, 3)")]
    [InlineData("(3, 4]", "(1, 2]", "(2, 3]")]
    [InlineData("(3, 4)", "(1, 3)", "[3, 3]")]
    public void Disjoint_intervals_should_return_expected_gap(string leftInterval, string rightInterval, string expectedInterval)
    {
        var left = Interval<int>.Parse(leftInterval);
        var right = Interval<int>.Parse(rightInterval);
        var expected = Interval<int>.Parse(expectedInterval);

        var actual = left.Gap(right);

        actual.Should()
            .NotBeNull()
            .And
            .Be(expected);
    }
}

public abstract class IntervalGapExtensionsTests<L, R>
    where L : struct, IBound
    where R : struct, IBound
{

    [Theory]
    [InlineData(1, 2, 3, 4)] // Ascending
    [InlineData(1, 3, 3, 4)]
    [InlineData(3, 4, 1, 2)] // Descending
    [InlineData(3, 4, 1, 3)]
    public void TypedInterval_gap_is_equivalent_to_Interval(int leftStart, int leftEnd, int rightStart, int rightEnd)
    {
        var leftInterval = new Interval<int, L, R>(leftStart, leftEnd);
        var rightInterval = new Interval<int, L, R>(rightStart, rightEnd);

        var expectedResult = leftInterval.Gap(rightInterval);

        var actual = leftInterval switch
        {
            Interval<int, Open, Open> openLeftInterval => openLeftInterval.Gap((Interval<int, Open, Open>)(object)rightInterval),
            Interval<int, Open, Closed> openClosedLeftInterval => openClosedLeftInterval.Gap((Interval<int, Open, Closed>)(object)rightInterval),
            Interval<int, Closed, Open> closedOpenLeftInterval => closedOpenLeftInterval.Gap((Interval<int, Closed, Open>)(object)rightInterval),
            Interval<int, Closed, Closed> closedLeftInterval => closedLeftInterval.Gap((Interval<int, Closed, Closed>)(object)rightInterval),
            Interval<int, Open, Unbounded> openUnboundedLeftInterval => openUnboundedLeftInterval.Gap((Interval<int, Open, Unbounded>)(object)rightInterval),
            Interval<int, Closed, Unbounded> closedUnboundedLeftInterval => closedUnboundedLeftInterval.Gap((Interval<int, Closed, Unbounded>)(object)rightInterval),
            Interval<int, Unbounded, Open> unboundedOpenLeftInterval => unboundedOpenLeftInterval.Gap((Interval<int, Unbounded, Open>)(object)rightInterval),
            Interval<int, Unbounded, Closed> unboundedClosedLeftInterval => unboundedClosedLeftInterval.Gap((Interval<int, Unbounded, Closed>)(object)rightInterval),
            Interval<int, Unbounded, Unbounded> unboundedLeftInterval => unboundedLeftInterval.Gap((Interval<int, Unbounded, Unbounded>)(object)rightInterval),
            _ => throw new NotSupportedException()
        };

        actual.Should()
            .NotBeNull()
            .And
            .Be(expectedResult);
    }
}

public class IntervalGapExtensionsTestsClosed : IntervalGapExtensionsTests<Closed, Closed> { }

public class IntervalGapExtensionsTestsOpen : IntervalGapExtensionsTests<Open, Open> { }

public class IntervalGapExtensionsTestsOpenClosed : IntervalGapExtensionsTests<Open, Closed> { }

public class IntervalGapExtensionsTestsClosedOpen : IntervalGapExtensionsTests<Closed, Open> { }

public class IntervalGapExtensionsTestsOpenUnbounded : IntervalGapExtensionsTests<Open, Unbounded> { }

public class IntervalGapExtensionsTestsClosedUnbounded : IntervalGapExtensionsTests<Closed, Unbounded> { }

public class IntervalGapExtensionsTestsUnboundedOpen : IntervalGapExtensionsTests<Unbounded, Open> { }

public class IntervalGapExtensionsTestsUnboundedClosed : IntervalGapExtensionsTests<Unbounded, Closed> { }

public class IntervalGapExtensionsTestsUnbounded : IntervalGapExtensionsTests<Unbounded, Unbounded> { }