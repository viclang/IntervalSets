﻿using IntervalRecord.Tests.IntervalExtensionsTests;
using IntervalRecord.Tests.TestData;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using Xunit;

namespace IntervalRecord.Tests.Combiners
{
    public sealed class EnumerableTests : EnumerableTestsBase
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
            var list = OverlapList(startingPoint, length, offset, boundaryType);

            // Act
            var actual = list.Hull();

            // Assert
            var (startInclusive, endInclusive) = boundaryType.ToTuple();
            actual.Should().Be(new Interval<int>(startingPoint, list.Last().End, startInclusive, endInclusive));
        }

        [Theory]
        [InlineData(BoundaryType.Closed, 4)]
        [InlineData(BoundaryType.ClosedOpen, 4)]
        [InlineData(BoundaryType.OpenClosed, 4)]
        [InlineData(BoundaryType.Open, 6)]
        public void UnionAll_ShouldBeExpected(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);

            // Act
            var actual = list.Union().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(BoundaryType.Closed, 6)]
        [InlineData(BoundaryType.ClosedOpen, 4)]
        [InlineData(BoundaryType.OpenClosed, 4)]
        [InlineData(BoundaryType.Open, 3)]
        public void ExceptAll_ShouldBeExpected(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType).ToList();

            // Act
            var actual = list.Except().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(BoundaryType.Closed, 6)]
        [InlineData(BoundaryType.ClosedOpen, 3)]
        [InlineData(BoundaryType.OpenClosed, 3)]
        [InlineData(BoundaryType.Open, 3)]
        public void IntersectAll_ShouldBeExpected(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType).ToList();

            // Act
            var actual = list.Intersect().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }


        [Theory]
        [InlineData(BoundaryType.Closed, 4)]
        [InlineData(BoundaryType.ClosedOpen, 4)]
        [InlineData(BoundaryType.OpenClosed, 4)]
        [InlineData(BoundaryType.Open, 5)]
        public void Complement_ShouldHaveExpectedCount(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType).ToList();

            // Act
            var actual = list.Complement().ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Hull_EmptyList_ShouldBeNull()
        {
            // Arrange
            var list = Enumerable.Empty<Interval<int>>();

            // Act
            var actual = list.Hull();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void EmptyList_ShouldBeEmpty()
        {
            // Arrange
            var emptyList = Enumerable.Empty<Interval<int>>();

            // Act
            var actual = new IEnumerable<Interval<int>>[]
            {
                emptyList.Union(),
                emptyList.Except(),
                emptyList.Intersect(),
                emptyList.Complement()
            };

            // Assert
            actual.Should().AllBeEquivalentTo(emptyList);
        }

        [Theory]
        [InlineData(BoundaryType.Closed, 4)]
        [InlineData(BoundaryType.ClosedOpen, 5)]
        [InlineData(BoundaryType.OpenClosed, 5)]
        [InlineData(BoundaryType.Open, 5)]
        public void PairwiseGap_ShouldHaveExpectedCount(BoundaryType boundaryType, int expectedCount)
        {
            // Arrange
            var list = OverlapList(startingPoint, length, offset, boundaryType);

            // Act
            var actual = list.Pairwise((a, b) => a.Gap(b)).ToList();

            // Assert
            actual.Should().HaveCount(expectedCount);
        }
    }
}