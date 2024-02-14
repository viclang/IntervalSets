using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment;
internal static class IntervalConstants
{
    internal static readonly Regex IntervalRegex = new(@"(?:\[|\()(?:[^[\](),]*,[^,()[\]]*)(?:\)|\])");

    internal const string IntervalNotFoundMessage = "Interval not found in string. Please provide an interval string in correct format";
}
