using InfinityComparable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.Measurements
{
    public class DoubleLengthTests
    {
        [Theory]
        [InlineData(1.5, 2, 0.5, 0.5)]
        [InlineData(1.5, 3, 0.5, 1.5)]
        [InlineData(1.5, 4, 0.5, 2.5)]
        [InlineData(1.5, 5, 0.5, 3.5)]
        [InlineData(1.5, 6, 0.5, 4.5)]
        [InlineData(1.5, 2, 1.5, 0.5)]
        [InlineData(1.5, 3, 1.5, 1.5)]
        [InlineData(1.5, 4, 1.5, 2.5)]
        [InlineData(1.5, 5, 1.5, 3.5)]
        [InlineData(1.5, 6, 1.5, 4.5)]
        public void LengthWithStep(double start, double end, double step, double expectedClosedLength)
        {
            // Arrange
            var closed = new Interval<double>(start, end, true, true);
            var closedOpen = new Interval<double>(start, end, true, false);
            var openClosed = new Interval<double>(start, end, false, true);
            var open = new Interval<double>(start, end, false, false);

            // Act
            var actual = new double[]
            {
                closed.Length(step).Value,
                closedOpen.Length(step).Value,
                openClosed.Length(step).Value,
                open.Length(step).Value,
            };

            // Assert
            var expectedLenghtHalfOpen = expectedClosedLength - step;
            var expectedLenghtOpen = expectedClosedLength - step - step;

            actual[0].Should().Be(expectedClosedLength);
            actual[1].Should().Be(expectedLenghtHalfOpen < 0 ? 0 : expectedLenghtHalfOpen);
            actual[2].Should().Be(expectedLenghtHalfOpen < 0 ? 0 : expectedLenghtHalfOpen);
            actual[3].Should().Be(expectedLenghtOpen < 0 ? 0 : expectedLenghtOpen);
        }

        [Theory]
        [InlineData(1.5, 2, 0.5)]
        [InlineData(1.5, 3, 1.5)]
        [InlineData(1.5, 4, 2.5)]
        [InlineData(1.5, 5, 3.5)]
        public void Length(double start, double end, double expectedLength)
        {
            // Arrange
            var closed = new Interval<double>(start, end, true, true);
            var closedOpen = new Interval<double>(start, end, true, false);
            var openClosed = new Interval<double>(start, end, false, true);
            var open = new Interval<double>(start, end, false, false);

            // Act
            var actual = new double[]
            {
                closed.Length().Value,
                closedOpen.Length().Value,
                openClosed.Length().Value,
                open.Length().Value,
            };

            // Assert
            actual.Should().AllBeEquivalentTo(expectedLength);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(0.5, null, null)]
        [InlineData(null, 0.5, null)]
        [InlineData(null, null, 0.5)]
        [InlineData(0.5, null, 0.5)]
        [InlineData(null, 0.5, 0.5)]
        public void LengthInfinity(double? start, double? end, double? step)
        {
            // Arrange
            var closed = new Interval<double>(start, end, true, true);
            var closedOpen = new Interval<double>(start, end, true, false);
            var openClosed = new Interval<double>(start, end, false, true);
            var open = new Interval<double>(start, end, false, false);

            // Act
            Infinity<double>[] actual;
            if(step is not null)
            {
                actual = new Infinity<double>[]
                {
                    closed.Length(step.Value),
                    closedOpen.Length(step.Value),
                    openClosed.Length(step.Value),
                    open.Length(step.Value),
                };
            }
            else
            {
                actual = new Infinity<double>[]
                {
                    closed.Length(),
                    closedOpen.Length(),
                    openClosed.Length(),
                    open.Length(),
                };
            }            

            // Assert
            actual.Should().AllBeEquivalentTo(Infinity<double>.PositiveInfinity);
        }
    }
}
