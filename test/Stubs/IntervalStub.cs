using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions.Tests.Models
{

    public class IntervalInclusiveStub : IHasInterval<int>
    {
        public Interval<int> Interval { get; }

        public IntervalInclusiveStub(Interval<int> interval)
        {
            Interval = interval;
        }
    }
}
