using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions.Tests.Models
{
    public class RangeStub : IRange<int>
    {
        public int From { get; set; }
        public int? To { get; set; }

        public RangeStub(int from, int? to)
        {
            From = from;
            To = to;
        }
    }

    public class RangeInclusiveStub : RangeStub, IRangeInclusive<int>
    {
        public RangeInclusiveStub(int from, int? to) : base(from, to)
        { }
    }

    public class RangeExclusiveStub : RangeStub, IRangeExclusive<int>
    {
        public RangeExclusiveStub(int from, int? to) : base(from, to)
        { }
    }
}
