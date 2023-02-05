using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RangeExtensions.Interfaces;

namespace RangeExtensions.Comparers
{
    public class RangeComparer<T> : IRangeComparer<T>
        where T : struct, IComparable<T>, IComparable
    {
        private readonly Func<T, T> _toInclusive;

        public RangeComparer()
        {
            _toInclusive = x => x;
        }

        public RangeComparer(Func<T, T> toInclusive)
        {
            _toInclusive = toInclusive;
        }

        public int Compare(T from, T to)
        {
            return from.CompareTo(_toInclusive(to));
        }
    }
}
