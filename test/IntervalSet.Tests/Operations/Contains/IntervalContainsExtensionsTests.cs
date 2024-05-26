using FluentAssertions;
using IntervalSet.Types;
using IntervalSet.Operations;
using Newtonsoft.Json.Linq;

namespace IntervalSet.Tests.Operations.Contains;
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

    [Theory]
    [InlineData(1, 3, 1, false)]
    [InlineData(1, 3, 3, false)]
    [InlineData(1, 3, 2, true)]
    public void OpenInterval_contains(int start, int end, int value, bool expectedResult)
    {
        var interval = new Interval<int, Open, Open>(start, end);

        var actual = interval.Contains(value);

        actual.Should().Be(expectedResult);
    }


    [Theory]
    [InlineData(1, 3, 1, true)]
    [InlineData(1, 3, 3, true)]
    [InlineData(1, 3, 0, false)]
    [InlineData(1, 3, 4, false)]
    public void ClosedInterval_contains(int start, int end, int value, bool expectedResult)
    {
        var interval = new Interval<int, Closed, Closed>(start, end);

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
    public void StronglyTypedInterval_contains_is_equivalent_to_Interval(int start, int end, int value)
    {
        var interval = new Interval<int, L, R>(start, end);
        var expectedResult = new Interval<int>(start, end, L.Bound, R.Bound).Contains(value);

        var actual = interval.Contains(value);

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
