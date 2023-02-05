using System;
using System.Collections.Generic;
using System.Linq;
using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests.Extensions
{
    public abstract class OverlappingStateTestsBase
    {
        public static IEnumerable<object[]> IntervalPairsWithOverlappingState(
            int startingPoint,
            int length,
            int offset,
            BoundaryType boundaryType,
            bool halfOpenIncluded)
            => new IntOverlappingDataSet(startingPoint, length, offset, boundaryType).GetIntervalPairsWithOverlappingState(halfOpenIncluded);

        public static IEnumerable<object[]> IntervalPairs(
            int startingPoint,
            int length,
            int offset,
            BoundaryType boundaryType)
            => new IntOverlappingDataSet(startingPoint, length, offset, boundaryType).GetIntervalPairs();

        public static IEnumerable<Interval<int>> GetSingletonList(int start, int end)
        {
            for (var point = start; point <= end; point++)
            {
                yield return new Interval<int>(point, point, true, true);
            }
        }

        public static IEnumerable<Interval<int>> GetShiftList(Interval<int> value, int length, int offset)
        {
            yield return value;
            for (var i = 0; i < length; i++)
            {
                yield return value = value with { Start = value.Start + offset, End = value.End + offset };
            }
        }

        public static TheoryData<Interval<int>> IncrementalLengthSet(int referencePoint, int maxRadius)
            => IncrementalLengthSet(referencePoint, 0, maxRadius);

        public static TheoryData<Interval<int>> IncrementalLengthSet(int start, int minLength, int maxLength)
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
    }
}
