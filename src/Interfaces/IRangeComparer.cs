using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions.Interfaces
{
    public interface IRangeComparer<in T> : IComparer<T>
        where T : struct, IComparable<T>, IComparable
    { }

    public interface IRangeOverlapComparer<T> : IComparer<IRange<T>>
        where T : struct, IComparable<T>, IComparable
    { }
}
