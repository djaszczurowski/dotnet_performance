using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace dotnet_performance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig.CreateEmpty()
                .With(ConsoleLogger.Default)
                .With(BenchmarkDotNet.Diagnosers.MemoryDiagnoser.Default)
                .With(
                    EnvironmentAnalyser.Default,
                    OutliersAnalyser.Default,
                    MinIterationTimeAnalyser.Default,
                    IterationSetupCleanupAnalyser.Default,
                    MultimodalDistributionAnalyzer.Default
                )
                .With(BenchmarkDotNet.Columns.DefaultColumnProviders.Instance);

            BenchmarkRunner.Run<ForgetToCloseStream>(config);
        }
    }
}