using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntervalExtensions.Interfaces;

namespace IntervalExtensions.Comparers
{
    public class StartComparer<T> : IIntervalEndComparer<T>
        where T : struct, IComparable<T>, IComparable
    {
        public int Compare(IInterval<T>? x, IInterval<T>? y)
        {
            if (x is null || y is null)
            {
                throw new ArgumentNullException();
            }

            return x.CompareStart(y);
        }
    }
}
