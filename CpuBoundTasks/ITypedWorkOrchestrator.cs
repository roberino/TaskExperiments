using System;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBoundTasks
{
    public interface ITypedWorkOrchestrator<T> : IDisposable
    {
        Task<T> EnqueueWorkAsync(Func<T> work, CancellationToken cancellationToken = default);
    }
}