using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions
{
    public interface IRange<T>
    where T : struct, IComparable<T>, IComparable
    {
        T From { get; set; }
        T? To { get; set; }
    }

    public interface IFrom<T>
    {
        T From { get; set; }
    }

    public interface ITo<T>
    {
        T To { get; set; }
    }
}
