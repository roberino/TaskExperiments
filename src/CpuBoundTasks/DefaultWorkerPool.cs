using System;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBoundTasks
{
    public sealed class DefaultWorkerPool : IWorkOrchestrator
    {
        public Task<T> EnqueueWorkAsync<T>(Func<T> work, CancellationToken cancellationToken = default)
        {
            return Task.Run(work, cancellationToken);
        }

        public void Dispose()
        {
        }
    }
}