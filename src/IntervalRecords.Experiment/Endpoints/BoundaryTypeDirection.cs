using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Endpoints;

[Flags]
internal enum BoundaryTypeDirection
{
    Unbounded = 0,
    Open = 1,
    Closed = 2,
    Left = 4,
    Right = 8,
    BoundaryType = Open | Closed,  // Mask to extract BoundaryType information
    Direction = Left | Right  // Mask to extract Direction information
}
