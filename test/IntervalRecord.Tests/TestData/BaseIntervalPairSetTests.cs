using System.Collections;
using System.Collections.Generic;

namespace IntervalRecord.Tests.TestData
{
    public abstract class BaseIntervalPairSetTests
    {
        public static IEnumerable<object[]> IntervalPairsWithOverlappingState(
            int start,
            int end,
            BoundaryType boundaryType,
            int offset,
            bool halfOpenIncluded)
            => new IntOverlappingDataSet(start, end, boundaryType, offset).GetIntervalPairsWithOverlappingState(halfOpenIncluded);

        public static IEnumerable<object[]> IntervalPairs(
            int start,
            int end,
            BoundaryType boundaryType,
            int offset)
            => new IntOverlappingDataSet(start, end, boundaryType, offset).GetIntervalPairs();
    }
}
