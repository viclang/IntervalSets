using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntervalExtensions.Interfaces;

namespace IntervalExtensions.Comparers
{
    public class OverlapComparer<T> : IIntervalOverlapComparer<T>
        where T : struct, IComparable<T>, IComparable
    {
        private readonly bool _isInclusive;

        public OverlapComparer(bool isInclusive)
        {
            _isInclusive = isInclusive;
        }

        public int Compare(IInterval<T>? x, IInterval<T>? y)
        {
            if (x is null || y is null)
            {
                throw new ArgumentNullException();
            }

            if (x.End is null
                && y.End is null)
            {
                return 0;
            }

            if (y.End is not null
                && x.Start.IsGreaterThan(y.End.Value, _isInclusive))
            {
                return 1;
            }

            if (x.End is not null
                && x.End!.Value.IsSmallerThan(y.Start, _isInclusive))
            {
                return -1;
            }

            return 0;
        }
    }
}
