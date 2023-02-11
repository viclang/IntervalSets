using Intervals.Tests.TestData;

namespace Intervals.Tests
{
    public class ComparableTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void CompareTo_ShouldBeExpected(Interval<int> a, Interval<int> b, IntervalOverlapping state)
        {
            // Act
            var actual = a.CompareTo(b);

            // Assert
            if ((int)state < 6)
            {
                actual.Should().Be(-1);
            }
            else if ((int)state == 6)
            {
                actual.Should().Be(0);
            }
            else if ((int)state > 6)
            {
                actual.Should().Be(1);
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void GreaterThan_ShouldBeExpected(Interval<int> a, Interval<int> b, IntervalOverlapping state)
        {
            // Act
            var actual = a > b;

            // Assert
            if ((int)state <= 6)
            {
                actual.Should().BeFalse();
            }
            else if ((int)state > 6)
            {
                actual.Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void LessThan_ShouldBeExpected(Interval<int> a, Interval<int> b, IntervalOverlapping state)
        {
            // Act
            var actual = a < b;

            // Assert
            if ((int)state < 6)
            {
                actual.Should().BeTrue();
            }
            else if ((int)state >= 6)
            {
                actual.Should().BeFalse();
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void GreaterOrEqualTo_ShouldBeExpected(Interval<int> a, Interval<int> b, IntervalOverlapping state)
        {
            // Act
            var actual = a >= b;

            // Assert
            if ((int)state < 6)
            {
                actual.Should().BeFalse();
            }
            else if ((int)state >= 6)
            {
                actual.Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void LessOrEqualTo_ShouldBeExpected(Interval<int> a, Interval<int> b, IntervalOverlapping state)
        {
            // Act
            var actual = a <= b;

            // Assert
            if ((int)state <= 6)
            {
                actual.Should().BeTrue();
            }
            else if ((int)state > 6)
            {
                actual.Should().BeFalse();
            }
        }
    }
}
