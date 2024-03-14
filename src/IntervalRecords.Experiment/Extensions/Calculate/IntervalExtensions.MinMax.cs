using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Extensions.Calculate;
public static partial class IntervalExtensions
{
    public static T Minimum<T>(this Interval<T> interval) where T : struct, IComparable<T>, ISpanParsable<T>, IMinMaxValue<T>
    {
        return interval.Start is null ? T.MinValue : interval.Start.Value;
    }

    public static T Maximum<T>(this Interval<T> interval) where T : struct, IComparable<T>, ISpanParsable<T>, IMinMaxValue<T>
    {
        return interval.End is null ? T.MaxValue : interval.End.Value;
    }
}
