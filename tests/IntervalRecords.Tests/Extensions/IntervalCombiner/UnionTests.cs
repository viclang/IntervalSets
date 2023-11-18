//using FluentAssertions.Execution;
//using IntervalRecords.Tests.TestData;
//using System.Collections.Generic;
//using System.Linq;

//namespace IntervalRecords.Tests.CombineTests
//{
//    public class UnionTests
//    {
//        [Theory]
//        [ClassData(typeof(Int32ConnectedClassData))]
//        public void GivenTwoConnectedIntervals_WhenCombineUnion_ReturnsMinStartAndMaxEnd(OverlapTestData<int> testData)
//        {
//            var actual = testData.First.Union(testData.Second);

//            using (new AssertionScope())
//            {
//                var array = new Interval<int>[] { testData.First, testData.Second };
//                var minByStart = array.MinBy(i => i.Start)!;
//                var maxByEnd = array.MaxBy(i => i.End)!;
//                actual!.Start.Should().Be(minByStart.Start);
//                actual!.End.Should().Be(maxByEnd.End);
//                actual!.StartInclusive.Should().Be(testData.First.StartInclusive);
//                actual!.EndInclusive.Should().Be(testData.Second.EndInclusive);
//            }
//        }

//        [Theory]
//        [ClassData(typeof(Int32DisjointClassData))]
//        public void GivenTwoDisjointIntervals_WhenCombineUnion_ReturnsNull(OverlapTestData<int> testData)
//        {
//            var actual = testData.First.Union(testData.Second);

//            actual.Should().BeNull();
//        }

//        [Theory]
//        [InlineData(IntervalType.Closed, 4)]
//        [InlineData(IntervalType.ClosedOpen, 4)]
//        [InlineData(IntervalType.OpenClosed, 4)]
//        [InlineData(IntervalType.Open, 6)]
//        public void Union_ShouldBeExpected(IntervalType intervalType, int expectedCount)
//        {
//            // Arrange
//            var list = OverlapList(startingPoint, length, offset, intervalType);

//            // Act
//            var actual = list.UnionAll().ToList();

//            // Assert
//            actual.Should().HaveCount(expectedCount);
//        }


//        [Fact]
//        public void EmptyList_ShouldBeEmpty()
//        {
//            // Arrange
//            var emptyList = Enumerable.Empty<Interval<int>>();

//            // Act
//            var actual = new IEnumerable<Interval<int>>[]
//            {
//                emptyList.UnionAll(),
//                emptyList.ExcludeOverlap(),
//                emptyList.IntersectAll(),
//                emptyList.Complement()
//            };

//            // Assert
//            actual.Should().AllBeEquivalentTo(emptyList);
//        }
//    }
//}
