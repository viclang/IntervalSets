using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions.Tests.Models
{

    public class IntervalInclusiveStub : IInterval<int>
    {
        public int? Start { get; set; }
        public int? End { get; set; }

        public IntervalInclusiveStub(int start, int? end)
        {
            Start = start;
            End = end;
        }
    }

    public class IntervalExclusiveStub : IInterval<int>
    {
        public int? Start { get; set; }
        public int? End { get; set; }

        public IntervalExclusiveStub(int start, int? end)
        {
            Start = start;
            End = end;
        }
    }
}
