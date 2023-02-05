using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntervalExtensions.Interfaces;

namespace IntervalExtensions
{
    public class IntervalService<T> : IIntervalService<T>
        where T : struct, IComparable<T>, IComparable
    {
        private readonly IList<IInterval<T>> _intervals;
        private readonly IComparer<IInterval<T>> _overlapComparer;

        public IntervalService(
            IList<IInterval<T>> intervals,
            IComparer<IInterval<T>> overlapComparer)
        {
            _intervals = intervals;
            _overlapComparer = overlapComparer;
        }

        public void Add(IInterval<T> value)
        {
            if (!value.IsValidInterval(true))
            {
                throw new NotSupportedException("End cannot be smaller than start!");
            }

            if (_intervals.Any())
            {
                var start = _intervals.Min(x => x.Start);
                var last = _intervals.GetLastInterval();
                var replaceEnd = value.Start;

                //if (OverlapsWith(start, last!.End ?? replaceEnd, value.Start, value.End))
                //{
                //    throw new NotSupportedException("Collection interval has overlap with value end add!");
                //}

                if (last!.End is null)
                {
                    last.End = (T)replaceEnd;
                }
            }
            _intervals.Add(value);
        }

        public void Update(IInterval<T> interval)
        {
            throw new NotImplementedException();
        }

        public void Remove(IInterval<T> interval)
        {
            throw new NotImplementedException();
        }

        public void RemoveBefore(T before)
        {
            throw new NotImplementedException();
        }

        public void RemoveAfter(T after)
        {
            throw new NotImplementedException();
        }
    }
}
