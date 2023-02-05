using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public static class IntervalTypeMapper
    {
        public static IntervalType ToType(bool startInclusive, bool endInclusive) => (startInclusive, endInclusive) switch
        {
            (false, false) => IntervalType.Open,
            (true, true) => IntervalType.Closed,
            (false, true) => IntervalType.OpenClosed,
            (true, false) => IntervalType.ClosedOpen,
        };

        public static (bool, bool) ToTuple(IntervalType intervalType) => intervalType switch
        {
            IntervalType.Open => (false, false),
            IntervalType.Closed => (true, true),
            IntervalType.OpenClosed => (false, true),
            IntervalType.ClosedOpen => (true, false),
            _ => throw new NotSupportedException()
        };
    }
}
