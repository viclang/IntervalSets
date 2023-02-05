using IntervalRecord.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public static class BoundaryTypeMapper
    {
        public static BoundaryType ToType(bool startInclusive, bool endInclusive) => (startInclusive, endInclusive) switch
        {
            (false, false) => BoundaryType.Open,
            (true, true) => BoundaryType.Closed,
            (false, true) => BoundaryType.OpenClosed,
            (true, false) => BoundaryType.ClosedOpen,
        };

        public static (bool, bool) ToTuple(BoundaryType intervalType) => intervalType switch
        {
            BoundaryType.Open => (false, false),
            BoundaryType.Closed => (true, true),
            BoundaryType.OpenClosed => (false, true),
            BoundaryType.ClosedOpen => (true, false),
            _ => throw new NotSupportedException()
        };
    }
}
