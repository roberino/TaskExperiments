using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CpuBoundTasks.Tests
{
    public class DedicatedWorkerPoolTests
    {
        [Fact]
        public async Task EnqueueWorkAsync_SomeWork()
        {
            using (var workerPool = new DedicatedWorkerPool(4))
            {
                var result1 = await workerPool.EnqueueWorkAsync(() => new Data() {Value = 1});
                var result2 = await workerPool.EnqueueWorkAsync(() => new Data() {Value = 2});
                var result3 = await workerPool.EnqueueWorkAsync(() => new Data() {Value = 3});

                Assert.Equal(1, result1.Value);
                Assert.Equal(2, result2.Value);
                Assert.Equal(3, result3.Value);
            }
        }

        [Fact]
        public async Task EnqueueWorkAsync_SomeWorkParallel()
        {
            const int taskCount = 50;

            using (var workerPool = new DedicatedWorkerPool(4))
            {
                var tasks = Enumerable
                    .Range(1, taskCount).Select(n => workerPool.EnqueueWorkAsync(() => new Data() { Value = n }))
                    .ToList();

                var results = await Task.WhenAll(tasks);

                Assert.Equal(taskCount, results.Length);

                Assert.Equal(taskCount,
                    Enumerable
                        .Range(1, taskCount)
                        .Join(results, o => o, i => i.Value, (o, i) => (i, o))
                        .Count());

            }
        }
    }
}