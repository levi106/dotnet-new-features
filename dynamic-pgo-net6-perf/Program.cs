using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Collections;
using System.Collections.Generic;
using System.Text;

BenchmarkRunner.Run<PgoBenchmarks>();

[Config(typeof(MyEnvVars))]
public class PgoBenchmarks
{
    class MyEnvVars : ManualConfig
    {
        public MyEnvVars()
        {
            AddJob(Job.Default.WithId("Default mode"));

            AddJob(Job.Default.WithId("Dynamic PGO")
                .WithEnvironmentVariables(
                    new EnvironmentVariable("DOTNET_TieredPGO", "1"),
                    new EnvironmentVariable("DOTNET_TC_QuickJitForLoops", "1"),
                    new EnvironmentVariable("DOTNET_ReadyToRun", "0")));
        }
    }

    public IEnumerable<object> TestData()
    {
        yield return new List<int>();
    }

    [Benchmark]
    [ArgumentsSource(nameof(TestData))]
    public void GuardedDevirtualization(ICollection<int> collection)
    {
        collection.Clear();
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);
    }

    [Benchmark]
    public StringBuilder ProfileDrivingInlining()
    {
        StringBuilder sb = new();
        for (int i = 0; i < 1000; i++)
            sb.Append("hi");
        return sb;
    }

    [Benchmark]
    [Arguments(42)]
    public string HotColdBlockReordering(int a)
    {
        if (a == 1)
            return "a is 1";
        if (a == 2)
            return "a is 2";
        if (a == 3)
            return "a is 3";
        if (a == 4)
            return "a is 4";
        if (a == 5)
            return "a is 5";
        return "a is too big";
    }
}