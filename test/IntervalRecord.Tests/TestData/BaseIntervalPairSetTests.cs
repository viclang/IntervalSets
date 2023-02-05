using System.Collections;
using System.Collections.Generic;

namespace IntervalRecord.Tests.TestData
{
    public abstract class BaseIntervalPairSetTests
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
    }
}
