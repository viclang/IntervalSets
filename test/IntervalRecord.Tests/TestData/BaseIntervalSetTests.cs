using System.Collections.Generic;

namespace IntervalRecord.Tests.TestData
{
    public abstract class BaseIntervalSetTests
    {
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

        public static List<Interval<int>> OverlapList(
            int startingPoint,
            int length,
            int offset,
            BoundaryType boundaryType)
        {
            var dataset = new IntOverlappingDataSet(startingPoint, length, offset, boundaryType);
            return new List<Interval<int>>
            {
                dataset.Before,
                dataset.Starts,
                dataset.Reference,
                dataset.ContainedBy,
                dataset.Finishes,
                dataset.After
            };
        }
    }
}
