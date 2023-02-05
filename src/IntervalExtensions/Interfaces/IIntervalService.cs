using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions.Interfaces
{
    public interface IIntervalService<T>
        where T : struct, IComparable<T>, IComparable
    {
        public void Add(IInterval<T> interval);

        public void Update(IInterval<T> interval);

        public void Remove(IInterval<T> interval);

        public void RemoveBefore(T before);
        public void RemoveAfter(T after);
    }
}
