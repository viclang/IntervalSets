using IntervalRecord.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.IntervalExtensionsTests
{
    public abstract class EnumerableTestsBase
    {
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
