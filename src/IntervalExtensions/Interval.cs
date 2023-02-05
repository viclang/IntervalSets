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
        public T? Start { get; init; }
        public T? End { get; init; }
        public IntervalType IntervalType { get; init; }

        public Interval(T? start, T? end, IntervalType intervalType)
        {
            Start = start;
            End = end;
            IntervalType = intervalType;
        }
    }
}
