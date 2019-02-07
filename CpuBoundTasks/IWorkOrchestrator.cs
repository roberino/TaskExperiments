using System;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBoundTasks
{
    public interface IWorkOrchestrator : IDisposable
    {
        Task<T> EnqueueWorkAsync<T>(Func<T> work, CancellationToken cancellationToken = default);
    }
}