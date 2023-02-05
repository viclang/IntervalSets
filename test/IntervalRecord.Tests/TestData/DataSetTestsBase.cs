using System;
using System.Collections.Generic;

namespace IntervalRecord.Tests.TestData
{
    public abstract class DataSetTestsBase
    {
        public const int startingPoint = 0;
        public const int length = 4;
        public const int offset = 1;
        public static readonly OverlappingTestDataBuilder OverlappingTestDataBuilder =
            new(startingPoint, length, offset, Enum.GetValues<BoundaryType>());

        public static IEnumerable<object[]> IntervalPairsWithOverlappingState(bool includeHalfOpen)
            => (OverlappingTestDataBuilder with { IncludeHalfOpen = includeHalfOpen }).Build();
        public static IEnumerable<object[]> IntervalPairs
            => (OverlappingTestDataBuilder with { WithOverlappingState = false }).Build();

        protected static IEnumerable<Interval<int>> GetSingletonList(int start, int end)
        {
            for (var point = start; point <= end; point++)
            {
                yield return new Interval<int>(point, point, true, true);
            }
        }

        protected static IEnumerable<Interval<int>> GetShiftList(Interval<int> value, int length, int offset)
        {
            yield return value;
            for (var i = 0; i < length; i++)
            {
                yield return value = value with { Start = value.Start + offset, End = value.End + offset };
            }
        }

        protected static TheoryData<Interval<int>> IncrementalLengthSet(int referencePoint, int maxRadius)
            => IncrementalLengthSet(referencePoint, 0, maxRadius);

        protected static TheoryData<Interval<int>> IncrementalLengthSet(int start, int minLength, int maxLength)
        {
            var data = new TheoryData<Interval<int>>();
            if (minLength == 0)
            {
                data.Add(new Interval<int>(start, start, true, true));
                minLength++;
            }

            for (int i = minLength; i <= maxLength; i++)
            {
                data.Add(new Interval<int>(start, start + i, true, true));
                data.Add(new Interval<int>(start, start + i, true, false));
                data.Add(new Interval<int>(start, start + i, false, true));
                data.Add(new Interval<int>(start, start + i, false, false));
            }
            return data;
        }

        protected static IEnumerable<Interval<int>> OverlapList(
            int startingPoint,
            int length,
            int offset,
            BoundaryType boundaryType)
        {
            var (startInclusinve, endInclusive) = boundaryType.ToTuple();
            var lastValue = new Interval<int>(
                startingPoint,
                startingPoint + length,
                startInclusinve,
                endInclusive);

            yield return lastValue;
            yield return lastValue = lastValue.GetMetBy(); // Meets
            yield return lastValue = lastValue.GetAfter(offset); // Before
            yield return lastValue = lastValue.GetStartedBy(offset); // Starts
            yield return lastValue = lastValue.GetAfter(offset); // Before
            yield return lastValue = lastValue.GetFinishes(offset); // FinishedBy
            yield return lastValue = lastValue.GetAfter(offset); // Before
            yield return lastValue = lastValue.GetContainedBy(offset); // Contains
            yield return lastValue = lastValue.GetAfter(offset); // Before
            yield return lastValue = lastValue.GetOverlappedBy(offset); // Overlaps
            yield return lastValue; // Equal
        }
    }
}
