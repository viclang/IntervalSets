// See https://aka.ms/new-console-template for more information

using IntervalRecords;
using static IntervalRecords.TestData.IntervalRelationFactory<int>;

var reference = new ClosedInterval<int>(5, 9);
var after = After(reference, 1);

