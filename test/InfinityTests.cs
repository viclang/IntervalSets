using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests
{
    public class InfinityTests
    {
        [Fact]
        public void NegativeInfinity_ShouldBeLessThanMin()
        {
            var minValue = new Interval<int>(int.MinValue, 0, true, true);
            var negativeInfinite = new Interval<int>(null, 0, false, true);

            negativeInfinite.Should().BeLessThan(minValue);
        }

        [Fact]
        public void PositiveInfinity_ShouldBeGreaterThanMax()
        {
            var maxValue = new Interval<int>(0, int.MaxValue, true, true);
            var positiveInfinite = new Interval<int>(0, null, true, false);

            positiveInfinite.Should().BeGreaterThan(maxValue);
        }

        [Fact]
        public void Infinity_ShouldBeGreaterThanMinMax()
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

        [Fact]
        public void Infinity_ShouldBeOpen()
        {
            var infinity = new Interval<int>(null, null, true, true);

            infinity.StartInclusive.Should().BeFalse();
            infinity.EndInclusive.Should().BeFalse();
        }

        [Fact]
        public void Test()
        {
            int? i = 1;
            var positive = -i.OrInfinite();
            var negative = -new Infinity<int>();

            var val = positive.IsFinite();

            var greaterThan = positive > negative;
            var lessThan = negative < positive;

            greaterThan.Should().BeTrue();
            positive.IsFinite().Should().BeTrue();
        }
    }
}
