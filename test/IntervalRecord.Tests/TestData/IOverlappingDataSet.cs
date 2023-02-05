using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestData
{
    public interface IOverlappingDataSet<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        Interval<T> Reference { get; init; }
        Interval<T> Before { get; }
        Interval<T> After { get; }
        Interval<T> ContainedBy { get; }
        Interval<T> Meets { get; }
        Interval<T> OverlappedBy { get; }
        Interval<T> StartedBy { get; }
        Interval<T> Contains { get; }
        Interval<T> Finishes { get; }
        Interval<T> Equal { get; }
        Interval<T> FinishedBy { get; }
        Interval<T> Starts { get; }
        Interval<T> Overlaps { get; }
        Interval<T> MetBy { get; }

        IOverlappingDataSet<T> CopyWith(BoundaryType boundaryType);
        TheoryData<Interval<T>, Interval<T>, OverlappingState> GetOverlappingState(bool includeHalfOpen);
    }
}
