using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions.Interfaces
{
    internal interface IIntervalStartComparer<T> : IComparer<IInterval<T>>
        where T : struct, IComparable<T>, IComparable
    { }

    internal interface IIntervalEndComparer<T> : IComparer<IInterval<T>>
        where T : struct, IComparable<T>, IComparable
    { }

    internal interface IIntervalOverlapComparer<T> : IComparer<IInterval<T>>
        where T : struct, IComparable<T>, IComparable
    { }
}
