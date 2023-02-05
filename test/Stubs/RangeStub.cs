using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions.Tests.Models
{

    public class RangeInclusiveStub : IRangeInclusive<int>
    {
        public int From { get; set; }
        public int? To { get; set; }

        public RangeInclusiveStub(int from, int? to)
        {
            From = from;
            To = to;
        }
    }

    public class RangeExclusiveStub : IRangeExclusive<int>
    {
        public int From { get; set; }
        public int? To { get; set; }

        public RangeExclusiveStub(int from, int? to)
        {
            From = from;
            To = to;
        }
    }
}
