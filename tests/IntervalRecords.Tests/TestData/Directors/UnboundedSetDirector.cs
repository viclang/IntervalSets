using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Tests.TestData;
public static class UnboundedSetDirector<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static void WithUnBoundedSet(OverlapBuilderBase<T> builder)
    {
        builder.WithLeftboundedFinishes();
        builder.WithLeftBoundedEqual();
        builder.WithLeftBoundedFinishedBy();
        builder.WithRightboundedStarts();
        builder.WithRightBoundedEqual();
        builder.WithRightboundedStartedBy();
        builder.WithUnBoundedEqual();
    }
}
