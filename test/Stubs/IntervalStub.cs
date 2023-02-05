using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.Models
{

    public class IntervalInclusiveStub : IHasInterval<int>
    {
        public int Start { get; set; }

        public int End { get; set; }

        public Interval<int> Interval => IntervalRecord.Interval.Closed(Start, End);
    }
}
