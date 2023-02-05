using IntervalRecord.Tests.TestData;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using Xunit;

namespace IntervalRecord.Tests.ExtensionsTests
{
    public class EnumerableTests : BaseIntervalSetTests
    {
        private const int startingPoint = 0;
        private const int length = 4;
        private const int offset = 2;

        [Theory]
        [InlineData(BoundaryType.Closed)]
        [InlineData(BoundaryType.ClosedOpen)]
        [InlineData(BoundaryType.OpenClosed)]
        [InlineData(BoundaryType.Open)]
        public void Hull_ShouldBeExpected(BoundaryType boundaryType)
        {
            // Arrange
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            var list = GetShiftList(new Interval<int>(1, 4, startInclusive, endInclusive), 5, 2).ToList();

            // Act
            var actual = list.Hull();

            // Assert
            actual.Should().Be(new Interval<int>(1, list.Last().End, startInclusive, endInclusive));
        }

        [Fact]
        public void UnionAll_ShouldBeExpected()
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, BoundaryType.Closed);

            // Act
            var actual = list.UnionAll().ToList();

            // Assert
            actual.Should().HaveCount(4);
        }


        [Fact]
        public void ExceptAll_ShouldBeExpected()
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, BoundaryType.Closed).ToList();

            // Act
            var actual = list.ExceptAll().ToList();

            // Assert
            actual.Should().HaveCount(5);
        }

        [Fact]
        public void IntersectAll_ShouldBeExpected()
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, BoundaryType.Closed).ToList();

            // Act
            var actual = list.IntersectAll().ToList();

            // Assert
            actual.Should().HaveCount(6);
        }


        [Fact]
        public void Complement_ShouldBeExpected()
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, BoundaryType.Closed).ToList();

            // Act
            var actual = list.Complement(x => !x.Closure(1).IsEmpty()).ToList();

            // Assert
            actual.Should().HaveCount(4);
        }
    }
}
