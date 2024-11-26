using FluentAssertions;
using IntervalSets.Operations;
using IntervalSets.Tests.TestData;
using IntervalSets.Types;

namespace IntervalSets.Tests.Operations;
public class IntervalOverlapsExtensionsTests
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

    public abstract class BaseIntervalOverlapsExtensionsTests<L1, R1, L2, R2>
        where L1 : struct, IBound
        where R1 : struct, IBound
        where L2 : struct, IBound
        where R2 : struct, IBound
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
            var leftInterval = new Interval<int, L1, R1>(leftStart, leftEnd);
            var rightInterval = new Interval<int, L2, R2>(rightStart, rightEnd);

            var expectedResult = leftInterval.Overlaps(rightInterval);

            var actual = leftInterval switch
            {
                Interval<int, Open, Open> openLeftInterval => openLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                Interval<int, Open, Closed> openClosedLeftInterval => openClosedLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                Interval<int, Closed, Open> closedOpenLeftInterval => closedOpenLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                Interval<int, Closed, Closed> closedLeftInterval => closedLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                Interval<int, Open, Unbounded> openUnboundedLeftInterval => openUnboundedLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                Interval<int, Closed, Unbounded> closedUnboundedLeftInterval => closedUnboundedLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                Interval<int, Unbounded, Open> unboundedOpenLeftInterval => unboundedOpenLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                Interval<int, Unbounded, Closed> unboundedClosedLeftInterval => unboundedClosedLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                Interval<int, Unbounded, Unbounded> unboundedLeftInterval => unboundedLeftInterval.Overlaps((Interval<int, L2, R2>)(object)rightInterval),
                _ => throw new NotImplementedException()
            };

            actual.Should().Be(expectedResult);
        }
    }

    public class IntervalClosed : BaseIntervalOverlapsExtensionsTests<Closed, Closed, Closed, Closed> { }

    public class IntervalOpen : BaseIntervalOverlapsExtensionsTests<Open, Open, Open, Open> { }

    public class IntervalOpenClosed : BaseIntervalOverlapsExtensionsTests<Open, Closed, Open, Closed> { }

    public class IntervalOpenClosedClosedOpen : BaseIntervalOverlapsExtensionsTests<Open, Closed, Closed, Open> { }

    public class IntervalClosedOpen : BaseIntervalOverlapsExtensionsTests<Closed, Open, Closed, Open> { }

    public class IntervalClosedOpenOpenClosed : BaseIntervalOverlapsExtensionsTests<Closed, Open, Open, Closed> { }

    public class IntervalOpenUnbounded : BaseIntervalOverlapsExtensionsTests<Open, Unbounded, Open, Unbounded> { }

    public class IntervalOpenUnboundedUnboundedOpen : BaseIntervalOverlapsExtensionsTests<Open, Unbounded, Unbounded, Open> { }

    public class IntervalClosedUnbounded : BaseIntervalOverlapsExtensionsTests<Closed, Unbounded, Closed, Unbounded> { }

    public class IntervalClosedUnboundedUnboundedClosed : BaseIntervalOverlapsExtensionsTests<Closed, Unbounded, Unbounded, Closed> { }

    public class IntervalUnboundedOpen : BaseIntervalOverlapsExtensionsTests<Unbounded, Open, Unbounded, Open> { }

    public class IntervalUnboundedOpenOpenUnbounded : BaseIntervalOverlapsExtensionsTests<Unbounded, Open, Open, Unbounded> { }

    public class IntervalUnboundedClosedClosedUnbounded : BaseIntervalOverlapsExtensionsTests<Unbounded, Closed, Unbounded, Closed> { }
    
    public class IntervalUnboundedClosed : BaseIntervalOverlapsExtensionsTests<Unbounded, Closed, Closed, Unbounded> { }

    public class IntervalUnbounded : BaseIntervalOverlapsExtensionsTests<Unbounded, Unbounded, Unbounded, Unbounded> { }
}
