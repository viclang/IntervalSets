using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalSet.Types;
public interface IComplementInterval<T>
{
    public abstract T Start { get; }

    public abstract T End { get; }

    public abstract Bound StartBound { get; }

    public abstract Bound EndBound { get; }

    public abstract bool IsEmpty { get; }
}
