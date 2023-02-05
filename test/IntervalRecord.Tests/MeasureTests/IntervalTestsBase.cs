using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.MeasurementsTests
{
    public abstract class IntervalTestsBase<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
    }
}
