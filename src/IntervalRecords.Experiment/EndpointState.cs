using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Endpoints;
public enum EndpointState : byte
{
    NegativeInfinity = 1,
    Finite = 2,
    PositiveInfinity = 3
}
