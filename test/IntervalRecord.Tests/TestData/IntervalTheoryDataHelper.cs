using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestData
{
    public static class IntervalTheoryDataHelper
    {
        public static TheoryData<Interval<int>> GenerateTheoryData(int referencePoint, int maxRadius)
            => GenerateTheoryData(referencePoint, 0, maxRadius);

        public static TheoryData<Interval<int>> GenerateTheoryData(int referencePoint, int minRadius, int maxRadius)
        {
            var data = new TheoryData<Interval<int>>();
            if (minRadius == 0)
            {
                data.Add(new Interval<int>(referencePoint, referencePoint, true, true));
                minRadius++;
            }

            for (int i = minRadius; i <= maxRadius; i++)
            {
                data.Add(new Interval<int>(referencePoint - i, referencePoint + i, true, true));
                data.Add(new Interval<int>(referencePoint - i, referencePoint + i, true, false));
                data.Add(new Interval<int>(referencePoint - i, referencePoint + i, false, true));
                data.Add(new Interval<int>(referencePoint - i, referencePoint + i, false, false));
            }
            return data;
        }
    }
}
