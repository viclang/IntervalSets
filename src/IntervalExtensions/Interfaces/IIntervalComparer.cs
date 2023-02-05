using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions.Interfaces
{
    public interface IIntervalComparer<in T> : IComparer<T>
        where T : struct, IComparable<T>, IComparable
    { }

    public interface IIntervalOverlapComparer<T> : IComparer<IInterval<T>>
        where T : struct, IComparable<T>, IComparable
    { }
}
