using System;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBoundTasks
{
    public class InlineWorkOrchestrator : IWorkOrchestrator
    {
        public Task<T> EnqueueWorkAsync<T>(Func<T> work, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(work());
        }

        public void Dispose()
        {
        }
    }
}
