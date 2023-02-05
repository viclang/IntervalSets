using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Enums
{
    public enum OverlapState
    {
        Before = 0,
        Meets = 1,
        Overlaps = 2,
        Starts = 3,
        ContainedBy = 4,
        Finishes = 5,
        Equal = 6,
        FinishedBy = 7,
        Contains = 8,
        StartedBy = 9,
        OverlappedBy = 10,
        MetBy = 11,
        After = 12
    }
}
