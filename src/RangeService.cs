using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RangeExtensions.Interfaces;

namespace RangeExtensions
{
    public class RangeService<T> : IRangeService<T>
        where T : struct, IComparable<T>, IComparable
    {
        private readonly IList<IRange<T>> _ranges;
        private readonly IComparer<IRange<T>> _overlapComparer;

        public RangeService(
            IList<IRange<T>> ranges,
            IComparer<IRange<T>> overlapComparer)
        {
            _ranges = ranges;
            _overlapComparer = overlapComparer;
        }

        public void Add(IRange<T> value)
        {
            if (!value.HasValidRange())
            {
                throw new NotSupportedException("To cannot be smaller than from!");
            }

            if (_ranges.Any())
            {
                var from = _ranges.Min(x => x.From);
                var last = _ranges.GetLastRange();
                var replaceTo = value.From - 1;

                //if (OverlapsWith(from, last!.To ?? replaceTo, value.From, value.To))
                //{
                //    throw new NotSupportedException("Collection range has overlap with value to add!");
                //}

                if (last!.To is null)
                {
                    last.To = replaceTo;
                }
            }
            _ranges.Add(value);
        }

        public void Update(IRange<T> range)
        {
            throw new NotImplementedException();
        }

        public void Remove(IRange<T> range)
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
