using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions
{
    public interface IRangeInclusive<T> : IRange<T>
    where T : struct, IComparable<T>, IComparable
    { }

    public interface IRangeExclusive<T> : IRange<T>
    where T : struct, IComparable<T>, IComparable
    { }

    public interface IRange<T>
    where T : struct, IComparable<T>, IComparable
    {
        T From { get; set; }
        T? To { get; set; }
    }


}
