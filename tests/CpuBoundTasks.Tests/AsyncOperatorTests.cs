using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CpuBoundTasks.Tests
{
    public class AsyncOperatorTests
    {
        [Fact]
        public async Task EnqueueWorkAsync_SomeMethod_AppliesOperations()
        {
            var worker = new AsyncOperator<Data>(new Data());

            foreach (var n in Enumerable.Range(1, 5))
            {
                await worker.EnqueueOperationAsync(data => { data.Value++; });
            }

            Assert.Equal(5, worker.Target.Value);
        }

        [Fact]
        public async Task EnqueueWorkAsync_SomeMethodParallel_AppliesOperations()
        {
            var worker = new AsyncOperator<Data>(new Data());

            var tasks = Enumerable.Range(1, 5)
                .Select(n => worker.EnqueueOperationAsync(data => { data.Value++; }))
                .ToList();

            await Task.WhenAll(tasks);

            Assert.Equal(5, worker.Target.Value);
        }
    }
}
