using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestDataSets
{
    public class DateTimeIntervalDataSet : IntervalTestDataSet<DateTime>
    {
        public DateTimeIntervalDataSet(Interval<DateTime> reference, TimeSpan offset)
            : base(reference)
        {
            var beforeEnd = reference.Start - offset;
            var beforeStart = beforeEnd - reference.Length();
            var containsStart = reference.Start + offset;
            var containsEnd = reference.End - offset;
            var afterStart = reference.End + offset;
            var afterEnd = afterStart + reference.Length();

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            Contains = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            Generate();
        }
    }
}
