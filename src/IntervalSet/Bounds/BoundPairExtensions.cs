using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalSet.Bounds;
public static class BoundPairExtensions
{
    private const int ExtractBound = 1;

    public static Bound StartBound(this BoundPair intervalType)
        => (Bound)(DecodeStartBound(intervalType) & ExtractBound);

    public static Bound EndBound(this BoundPair intervalType)
        => (Bound)((byte)intervalType & ExtractBound);

    private static int DecodeStartBound(BoundPair intervalType) => (byte)intervalType >> 1;
}
