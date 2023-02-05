using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RangeExtensions.Interfaces;

namespace RangeExtensions.Comparers
{
    public class OverlapComparer<T> : IRangeOverlapComparer<T>
        where T : struct, IComparable<T>, IComparable
    {
        private readonly IRangeComparer<T> _rangeComparer;

        public OverlapComparer(IRangeComparer<T> rangeComparer)
        {
            _rangeComparer = rangeComparer;
        }

        public int Compare(IRange<T>? x, IRange<T>? y)
        {
            if (x is null || y is null)
            {
                throw new ArgumentNullException();
            }

            if (x.To is null
                && y.To is null)
            {
                return 0;
            }

            if (y.To is not null
                && _rangeComparer.Compare(x.From, y.To.Value) is 1)
            {
                return 1;
            }

            if (x.To is not null
                && _rangeComparer.Compare(x.To!.Value, y.From) is -1)
            {
                return -1;
            }

            return 0;
        }
    }
}
