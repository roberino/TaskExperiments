using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBoundTasks
{
    public sealed class DedicatedWorkerPoolV2 : IWorkOrchestrator
    {
        readonly Worker[] _workers;

        bool _disposed;

        public DedicatedWorkerPoolV2(int size)
        {
            _workers = Enumerable
                .Range(1, size)
                .Select(n => new Worker(n))
                .ToArray();
        }

        public Task<T> EnqueueWorkAsync<T>(Func<T> work, CancellationToken cancellationToken = default)
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);

            return _workers
                .OrderBy(w => w.WorkLoad)
                .First()
                .EnqueueWorkAsync(work, cancellationToken);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                foreach (var worker in _workers)
                {
                    worker.Dispose();
                }
            }
        }

        ~DedicatedWorkerPoolV2()
        {
            Dispose();
        }
    }
}