using FluentAssertions.Execution;
using IntervalRecords.Extensions;
using IntervalRecords.Linq;
using IntervalRecords.Tests.TestData;
using System.Linq;

namespace IntervalRecords.Tests.Extensions
{
    public sealed class HullTests
    {
        [Theory]
        [InlineData("[1, 2]", "[3, 4]", "[1, 4]")]
        public void Hull_ShouldBeExpected(string leftInterval, string rightInterval, string expectedInterval)
        {
            var left = IntervalParser.Parse<int>(leftInterval);
            var right = IntervalParser.Parse<int>(rightInterval);

            var actual = left.Hull(right);

            actual.ToString()
                .Should().BeEquivalentTo(expectedInterval);
        }

        [Theory]
        [InlineData("[1, 2] [3, 4]", "[1, 4]")]
        public void ListHull_ShouldBeExpected(string intervals, string expectedInterval)
        {
            // Arrange
            var list = IntervalParser.ParseAll<int>(intervals);
            var expected = IntervalParser.Parse<int>(expectedInterval);

            // Act
            var actual = list.Hull();

            // Assert
            actual.Should().Be(expected);
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
    }
}
