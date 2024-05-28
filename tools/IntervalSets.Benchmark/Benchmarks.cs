using BenchmarkDotNet.Attributes;
using IntervalSets.Types;
using System.Text.RegularExpressions;

namespace IntervalSets.Benchmark;

[MemoryDiagnoser]
public partial class Benchmarks
{
    private const string interval = "[1111111, 9999999)";

    //[Benchmark]
    //public Interval<int> ParseBoundInvert() => IntervalParse.ParseBoundInvert<int>(
    //    interval);

    //[Benchmark]
    //public Interval<int> ParseBound() => IntervalParse.Pa<int>(
    //    interval);
}
