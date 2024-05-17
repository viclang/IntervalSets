// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using IntervalSet.Benchmark;

//var benchmark = new Benchmarks();
//var result = benchmark.ParseWithoutCaptureGroups();
//Console.WriteLine(result);
//result = benchmark.ParseWithCaptureGroups();
//Console.WriteLine(result);
var summary = BenchmarkRunner.Run<Benchmarks>();
