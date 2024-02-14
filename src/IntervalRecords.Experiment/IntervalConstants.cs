using System.Text.RegularExpressions;

namespace IntervalRecords.Experiment;
internal static class IntervalConstants
{
    internal static readonly Regex IntervalRegex = new(@"(?:\[|\()(?:[^[\](),]*,[^,()[\]]*)(?:\)|\])", RegexOptions.Compiled);

    internal const string IntervalNotFoundMessage = "Interval not found in string. Please provide an interval string in correct format";
}
