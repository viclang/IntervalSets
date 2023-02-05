using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntervalConverterTests
    {
        private const int start = 6;
        private const int end = 10;

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void Closure(bool startInclusive, bool endInclusive)
        {
            // Arrange
            var interval = new Interval<int>(start, end, startInclusive, endInclusive);

            // Act
            var actual = interval.Closure(1);

            // Assert
            var expectedStart = startInclusive ? start : start + 1;
            var expectedEnd = endInclusive ? end : end - 1;

            actual.Start.Should().Be(expectedStart);
            actual.End.Should().Be(expectedEnd);
            actual.StartInclusive.Should().BeTrue();
            actual.EndInclusive.Should().BeTrue();
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void Interior(bool startInclusive, bool endInclusive)
        {
            // Arrange
            var interval = new Interval<int>(start, end, startInclusive, endInclusive);

            // Act
            var actual = interval.Interior(1);

            // Assert
            var expectedStart = startInclusive ? start - 1 : start;
            var expectedEnd = endInclusive ? end + 1 : end;

            actual.Start.Should().Be(expectedStart);
            actual.End.Should().Be(expectedEnd);
            actual.StartInclusive.Should().BeFalse();
            actual.EndInclusive.Should().BeFalse();
        }

        [Theory]
        [InlineData(BoundaryType.Closed, false, false)]
        [InlineData(BoundaryType.Closed, true, false)]
        [InlineData(BoundaryType.Closed, false, true)]
        [InlineData(BoundaryType.Closed, true, true)]
        [InlineData(BoundaryType.ClosedOpen, false, false)]
        [InlineData(BoundaryType.ClosedOpen, true, false)]
        [InlineData(BoundaryType.ClosedOpen, false, true)]
        [InlineData(BoundaryType.ClosedOpen, true, true)]
        [InlineData(BoundaryType.OpenClosed, false, false)]
        [InlineData(BoundaryType.OpenClosed, true, false)]
        [InlineData(BoundaryType.OpenClosed, false, true)]
        [InlineData(BoundaryType.OpenClosed, true, true)]
        [InlineData(BoundaryType.Open, false, false)]
        [InlineData(BoundaryType.Open, true, false)]
        [InlineData(BoundaryType.Open, false, true)]
        [InlineData(BoundaryType.Open, true, true)]
        public void Canonicalize_ShouldBeExpected(BoundaryType intervalType, bool startInclusive, bool endInclusive)
        {
            // Arrange
            var interval = new Interval<int>(start, end, startInclusive, endInclusive);

            // Act
            var actual = interval.Canonicalize(intervalType, 1);

            // Assert
            var (expectedStartInclusive, expectedEndInclusive) = intervalType.ToTuple();

            var expectedStart = expectedStartInclusive
                ? startInclusive ? start : start + 1
                : startInclusive ? start - 1 : start;

            var expectedEnd = expectedEndInclusive
                ? endInclusive ? end : end - 1
                : endInclusive ? end + 1 : end;

            actual.Start.Should().Be(expectedStart);
            actual.End.Should().Be(expectedEnd);
            actual.StartInclusive.Should().Be(expectedStartInclusive);
            actual.EndInclusive.Should().Be(expectedEndInclusive);
        }

        [Fact]
        public void Closure_ShouldBe_CanonicalizeClosed()
        {
            // Arrange
            var open = Interval.Open(start, end);

            // Act
            var actual = open.Closure(1);
            var actualCanonicalize = open.Canonicalize(BoundaryType.Closed, 1);

            // Assert
            actual.Should().Be(actualCanonicalize);
        }

        [Fact]
        public void Interior_ShouldBe_CanonicalizeOpen()
        {
            // Arrange
            var closed = Interval.Closed(start, end);

            // Act
            var actualInterior = closed.Interior(1);
            var actualCanonicalize = closed.Canonicalize(BoundaryType.Open, 1);

            // Assert
            actualInterior.Should().Be(actualCanonicalize);
        }

        [Fact]
        public void UnboundedInterval_ShouldNeverBeClosed()
        {
            // Arrange
            var unbounded = new Interval<int>();

            // Act
            var actual = unbounded.Closure(1);

            // Assert
            unbounded.GetBoundaryType().Should().Be(BoundaryType.Open);
            actual.Should().Be(unbounded);
        }

        [Fact]
        public void EmptyInterval_ShouldNeverBeClosed()
        {
            // Arrange
            var empty = Interval.Empty<int>();

            // Act
            var actual = empty.Closure(1);

            // Assert
            empty.GetBoundaryType().Should().Be(BoundaryType.Open);
            actual.Should().Be(empty);
        }
    }
}
