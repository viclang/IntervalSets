//using FluentAssertions.Execution;
//using IntervalRecords.Extensions;
//using IntervalRecords.Tests.TestData;
//using System.Linq;

//namespace IntervalRecords.Tests.CombineTests
//{
//    public sealed class HullTests
//    {
//        [Theory]
//        [InlineData("[1, 2]", "[3, 4]", "[1, 4]")]
//        public void Hull_ShouldBeExpected(string leftInterval, string rightInterval, string expectedInterval)
//        {
//            var left = IntervalParser.Parse<int>(leftInterval);
//            var right = IntervalParser.Parse<int>(rightInterval);
//            var expected = IntervalParser.Parse<int>(expectedInterval);

//            var actual = left.Hull(right);

//            actual.Should().Be(expected);
//        }

//        [Theory]
//        [InlineData(IntervalType.Closed)]
//        [InlineData(IntervalType.ClosedOpen)]
//        [InlineData(IntervalType.OpenClosed)]
//        [InlineData(IntervalType.Open)]
//        public void ListHull_ShouldBeExpected(IntervalType intervalType)
//        {
//            // Arrange
//            var list = OverlapList(startingPoint, length, offset, intervalType);
//            var (startInclusive, endInclusive) = intervalType.ToTuple();
//            var expected = Interval<int>.Create(list.MinBy(i => i.Start)!.Start, list.MaxBy(i => i.End)!.End, startInclusive, endInclusive);

//            // Act
//            var actual = list.Hull();

//            // Assert
//            actual.Should().Be(expected);
//        }

//        //[Fact]
//        //public void Hull_EmptyList_ShouldBeNull()
//        //{
//        //    // Arrange
//        //    var list = Enumerable.Empty<Interval<int>>();

//        //    // Act
//        //    var actual = list.Hull();

//        //    // Assert
//        //    actual.Should().BeNull();
//        //}
//    }
//}
