//using IntervalRecords.Tests.TestData;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace IntervalRecords.Tests.CombineTests
//{
//    public class PairwiseTests
//    {
//        private const int _offset = 1;
//        private readonly static Interval<int> _reference = new ClosedInterval<int>(0, 4);

//        public static IEnumerable<object[]> OverLapListWithExpectedGapCount()
//        {
//            var testData = new List<object[]>();
//            foreach (var intervalType in (IntervalType[])Enum.GetValues(typeof(IntervalType)))
//            {
//                testData.Add(new object[]
//                {
//                GetList(
//                    _reference.Canonicalize(intervalType, 0),
//                    _offset),
//                intervalType == IntervalType.Open ? 5 : 3
//                });
//            }
//            return testData;
//        }

//        private static IEnumerable<Interval<int>> GetList(Interval<int> lastValue, int offset)
//        {
//            yield return lastValue;
//            yield return lastValue = OverlapFactory.MetBy(lastValue); // Meets
//            yield return lastValue = OverlapFactory.After(lastValue, offset); // Before
//            yield return lastValue = OverlapFactory.StartedBy(lastValue, offset); // Starts
//            yield return lastValue = OverlapFactory.After(lastValue, offset); // Before
//            yield return lastValue = OverlapFactory.Finishes(lastValue, offset); // FinishedBy
//            yield return lastValue = OverlapFactory.After(lastValue, offset); // Before
//            yield return lastValue = OverlapFactory.Contains(lastValue, offset); // Contains
//            yield return lastValue = OverlapFactory.After(lastValue, offset); // Before
//            yield return lastValue = OverlapFactory.OverlappedBy(lastValue, offset); // Overlaps
//            yield return lastValue; // Equal
//        }


//        [Theory]
//        [MemberData(nameof(OverLapListWithExpectedGapCount))]
//        public void PairwiseGap_ShouldHaveExpectedCount(IEnumerable<Interval<int>> list, int expectedCount)
//        {
//            var actual = list.Pairwise((a, b) => a.Gap(b)).ToList();

//            actual.Should().HaveCount(expectedCount);
//        }
//    }
//}
