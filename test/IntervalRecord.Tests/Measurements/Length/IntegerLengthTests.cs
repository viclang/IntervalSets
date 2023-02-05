using InfinityComparable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.Measurements
{
    public class IntegerLengthTests
    {
        [Theory]
        [InlineData(1, 2, 1, 1)]
        [InlineData(1, 3, 1, 2)]
        [InlineData(1, 4, 1, 3)]
        [InlineData(1, 5, 1, 4)]
        [InlineData(1, 6, 1, 5)]
        [InlineData(1, 2, 2, 1)]
        [InlineData(1, 3, 2, 2)]
        [InlineData(1, 4, 2, 3)]
        [InlineData(1, 5, 2, 4)]
        [InlineData(1, 6, 2, 5)]
        public void LengthWithStep(int start, int end, int step, int expectedClosedLength)
        {
            // Arrange
            var closed = new Interval<int>(start, end, true, true);
            var closedOpen = new Interval<int>(start, end, true, false);
            var openClosed = new Interval<int>(start, end, false, true);
            var open = new Interval<int>(start, end, false, false);

            // Act
            var actual = new int[]
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
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 2)]
        [InlineData(1, 4, 3)]
        [InlineData(1, 5, 4)]
        public void Length(int start, int end, int expectedLength)
        {
            // Arrange
            var closed = new Interval<int>(start, end, true, true);
            var closedOpen = new Interval<int>(start, end, true, false);
            var openClosed = new Interval<int>(start, end, false, true);
            var open = new Interval<int>(start, end, false, false);

            // Act
            var actual = new int[]
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
        [InlineData(1, null, null)]
        [InlineData(null, 1, null)]
        [InlineData(null, null, 1)]
        [InlineData(1, null, 1)]
        [InlineData(null, 1, 1)]
        public void LengthInfinity(int? start, int? end, int? step)
        {
            // Arrange
            var closed = new Interval<int>(start, end, true, true);
            var closedOpen = new Interval<int>(start, end, true, false);
            var openClosed = new Interval<int>(start, end, false, true);
            var open = new Interval<int>(start, end, false, false);

            // Act
            Infinity<int>[] actual;
            if(step is not null)
            {
                actual = new Infinity<int>[]
                {
                    closed.Length(step.Value),
                    closedOpen.Length(step.Value),
                    openClosed.Length(step.Value),
                    open.Length(step.Value),
                };
            }
            else
            {
                actual = new Infinity<int>[]
                {
                    closed.Length(),
                    closedOpen.Length(),
                    openClosed.Length(),
                    open.Length(),
                };
            }            

            // Assert
            actual.Should().AllBeEquivalentTo(Infinity<int>.PositiveInfinity);
        }
    }
}
