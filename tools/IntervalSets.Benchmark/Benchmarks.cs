using BenchmarkDotNet.Attributes;
using IntervalSets.Operations;
using IntervalSets.Types;

namespace IntervalSets.Benchmark;

[MemoryDiagnoser]
public partial class Benchmarks
{
    private static Interval<int> intervalA = new(1, 5, IntervalType.OpenClosed);
    private static Interval<int> intervalB= new(5, 7, IntervalType.ClosedOpen);

    private static Interval<int, Open, Closed> typedIntervalA = new(1, 5);
    private static Interval<int, Closed, Open> typedIntervalB = new(5, 7);

    [Benchmark]
    public bool Overlaps() => intervalA.Overlaps(intervalB);

    [Benchmark]
    public bool TypedOverlaps() => typedIntervalA.Overlaps(typedIntervalB);
}
