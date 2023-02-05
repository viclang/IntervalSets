using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestDataSets
{
    public class DateOnlyIntervalDataSet : IntervalTestDataSet<DateOnly>
    {
        public DateOnlyIntervalDataSet(Interval<DateOnly> reference, int offset)
            : base(reference)
        {
            var beforeEnd = reference.Start!.Value.AddDays(-offset);
            var beforeStart = beforeEnd.AddDays(-reference.Length());
            var containsStart = reference.Start!.Value.AddDays(offset);
            var containsEnd = reference.End!.Value.AddDays(-offset);
            var afterStart = reference.End!.Value.AddDays(offset);
            var afterEnd = afterStart.AddDays(reference.Length());

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            Contains = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            Generate();
        }
    }
}
