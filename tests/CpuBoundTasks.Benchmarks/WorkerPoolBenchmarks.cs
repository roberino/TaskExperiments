using BenchmarkDotNet.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace CpuBoundTasks.Benchmarks
{
    public class WorkerPoolBenchmarks
    {
        DedicatedWorkerPool _workerPool;

        [Params(1, 5, 10)]
        public int WorkPoolSize { get; set; }

        [Params(5, 100)]
        public int Iterations { get; set; }

        [Params(5, 10)]
        public int ParallelOperations { get; set; }

        [IterationSetup]
        public void Setup()
        {
            _workerPool = new DedicatedWorkerPool(WorkPoolSize);
        }

        [Benchmark]
        public void EnqueueWorkAsync()
        {
            var tasks = Enumerable.Range(1, ParallelOperations)
                .Select(n => _workerPool.EnqueueWorkAsync(() => Fibonacci(Iterations)));

            Task.WhenAll(tasks).Wait();
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
    }
}