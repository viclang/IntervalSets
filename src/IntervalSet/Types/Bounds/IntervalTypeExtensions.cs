using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalSet.Types;
public static class IntervalTypeExtensions
{
    private const int ExtractBound = 1;

    public static Bound StartBound(this IntervalType intervalType)
        => (Bound)(DecodeStartBound(intervalType) & ExtractBound);

    public static Bound EndBound(this IntervalType intervalType)
        => (Bound)((byte)intervalType & ExtractBound);

    private static int DecodeStartBound(IntervalType intervalType) => (byte)intervalType >> 1;
}
