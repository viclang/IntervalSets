using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests
{
    public class ComparableTests : DataSetTestsBase
    {
        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), false)]
        public void CompareTo_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
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
        public void GreaterThan_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
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
        public void LessThan_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
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
        public void GreaterOrEqualTo_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
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
        public void LessOrEqualTo_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
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

        [Theory]
        [InlineData(null, 0, int.MinValue, 0)]
        [InlineData(0, null, 0, int.MaxValue)]
        [InlineData(null, null, int.MinValue, int.MaxValue)]
        public void Infinity_ShouldBeGreaterThanValue(int? leftStart, int? leftEnd, int rightStart, int rightEnd)
        {
            var left = new Interval<int>(leftStart, leftEnd, true, true);
            var right = new Interval<int>(rightStart, rightEnd, false, true);

            left.Should().BeGreaterThan(right);
        }

        [Fact]
        public void InfiniteStartWithSameEnd_ShouldBeGreaterThanMinStart()
        {
            var minValue = new Interval<int>(int.MinValue, 0, true, true);
            var negativeInfinite = new Interval<int>(null, 0, false, true);

            negativeInfinite.Should().BeGreaterThan(minValue);
        }

        [Fact]
        public void InfiniteEndWithSameStart_ShouldBeGreaterThanMaxEnd()
        {
            var maxValue = new Interval<int>(0, int.MaxValue, true, true);
            var positiveInfinite = new Interval<int>(0, null, true, false);

            positiveInfinite.Should().BeGreaterThan(maxValue);
        }

        [Fact]
        public void InfiniteEndInfiniteStart_ShouldBeGreaterThanMinMax()
        {
            var minMaxValue = new Interval<int>(int.MinValue, int.MaxValue, true, true);
            var positiveInfinite = new Interval<int>(null, null, false, false);

            positiveInfinite.Should().BeGreaterThan(minMaxValue);
        }

        [Fact]
        public void PositiveInfinite_ShouldBeGreaterThanNegativeInfinite()
        {
            var negativeInfinite = new Interval<int>(null, 0, false, true);
            var positiveInfinite = new Interval<int>(0, null, true, false);

            positiveInfinite.Should().BeGreaterThan(negativeInfinite);
        }
    }
}
