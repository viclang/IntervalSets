using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions.Interfaces
{
    public interface IRangeService<T>
        where T : struct, IComparable<T>, IComparable
    {
        public void Add(IRange<T> range);

        public void Update(IRange<T> range);

        public void Remove(IRange<T> range);

        public void RemoveBefore(T before);
        public void RemoveAfter(T after);
    }
}
