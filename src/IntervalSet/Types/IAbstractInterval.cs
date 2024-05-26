using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalSet.Types;
public interface IAbstractInterval<T>
{
    T Start { get; }

    T End { get; }

    Bound StartBound { get; }

    Bound EndBound { get; }

    bool IsEmpty { get; }
}
