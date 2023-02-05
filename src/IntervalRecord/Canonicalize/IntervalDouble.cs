using InfinityComparable;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<double> Canonicalize(this Interval<double> source, IntervalType boundaryType, double step)
            => Canonicalize(source, boundaryType, b => b.Add(step), b => b.Substract(step));

        [Pure]
        public static Interval<double> Closure(this Interval<double> source, double step)
            => ToClosed(source, start => start.Add(step), end => end.Substract(step));

        [Pure]
        public static Interval<double> Interior(this Interval<double> source, double step)
            => ToOpen(source, end => end.Add(step), start => start.Substract(step));
    }
}
