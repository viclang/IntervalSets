using IntervalRecords.Experiment.Bounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment;
public record class BareInterval<T> : AbstractInterval<T, T>
    where T : IComparable<T>, ISpanParsable<T>
{
    public T Start { get => LeftEndpoint; init => LeftEndpoint = value; }

    public T End { get => RightEndpoint; init => RightEndpoint = value; }
    
    public BareInterval(T start, T end) : base(start, end)
    {
    }
}
