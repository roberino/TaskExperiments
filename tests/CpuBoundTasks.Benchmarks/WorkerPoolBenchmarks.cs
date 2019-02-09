using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpuBoundTasks.Benchmarks
{
    [CoreJob]
    [RankColumn, MarkdownExporter]
    public class WorkerPoolBenchmarks : IDisposable
    {
        IDictionary<ExecutionStrategy, IWorkOrchestrator> _workOrchestrators;

        [Params(50, 100, 500)]
        public int Iterations { get; set; }

        [Params(4, 8)]
        public int ParallelOperations { get; set; }

        [Params(ExecutionStrategy.Inline, ExecutionStrategy.Dedicated, ExecutionStrategy.GlobalThreadPool)]
        public ExecutionStrategy ExecutionStrategy { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _workOrchestrators = new Dictionary<ExecutionStrategy, IWorkOrchestrator>
            {
                [ExecutionStrategy.Dedicated] = new DedicatedWorkerPool(4),
                [ExecutionStrategy.GlobalThreadPool] = new GlobalWorkerPool(),
                [ExecutionStrategy.Inline] = new InlineWorkOrchestrator()
            };
        }

        [Benchmark]
        public void EnqueueWorkAsync()
        {
            var workerPool = _workOrchestrators[ExecutionStrategy];

            var tasks = Enumerable.Range(1, ParallelOperations)
                .Select(n => workerPool.EnqueueWorkAsync(() => Fibonacci(Iterations)));

            Task.WhenAll(tasks)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            if (_workOrchestrators == null)
            {
                return;
            }

            foreach (var keyValuePair in _workOrchestrators)
            {
                keyValuePair.Value.Dispose();
            }

            _workOrchestrators = null;
        }

        static int Fibonacci(int iterations)
        {
            int n1 = 0, n2 = 1, i;

            for (i = 2; i < iterations; ++i)
            {
                var next = n1 + n2;
                n1 = n2;
                n2 = next;
            }

            return n2;
        }

        public void Dispose()
        {
            Cleanup();
        }
    }
}