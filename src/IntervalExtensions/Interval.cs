using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions
{
    public record struct Interval<T> : IInterval<T>
        where T : struct, IComparable<T>, IComparable
    {
        public T? Start { get; set; }
        public T? End { get; set; }

        public Interval(T? start, T? end)
        {
            Start = start;
            End = end;
        }
    }
}
