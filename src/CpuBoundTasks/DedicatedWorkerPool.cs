using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBoundTasks
{
    public sealed class DedicatedWorkerPool : IWorkOrchestrator
    {
        readonly Thread[] _workerThreads;
        readonly WorkQueue<(Func<object> work, TaskCompletionSource<object> task, CancellationToken cancellationToken)> _work;
        readonly CancellationTokenSource _disposed;

        int _activeThreads;

        public DedicatedWorkerPool(int size)
        {
            _work = new WorkQueue<(Func<object> work, TaskCompletionSource<object> task, CancellationToken cancellationToken)>();
            _disposed = new CancellationTokenSource();

            _workerThreads = Enumerable
                .Range(1, size)
                .Select(n =>
                {
                    var thread = new Thread(Process);

                    thread.Start();

                    return thread;
                })
                .ToArray();
        }

        public int AvailableThreads => _workerThreads.Length - _activeThreads;

        public async Task<T> EnqueueWorkAsync<T>(Func<T> work, CancellationToken cancellationToken = default)
        {
            if (_disposed.IsCancellationRequested)
                throw new ObjectDisposedException(GetType().Name);

            var taskComplete = new TaskCompletionSource<object>();

            _work.Enqueue((() => work(), taskComplete, cancellationToken));

            await taskComplete.Task;

            return (T)taskComplete.Task.Result;
        }

        void Process()
        {
            while (!_disposed.IsCancellationRequested)
            {
                _work.WaitForData(_disposed.Token);

                while (!_work.IsEmpty)
                {
                    if (_work.TryDequeue(out var workItem))
                    {
                        if (workItem.cancellationToken.IsCancellationRequested)
                        {
                            workItem.task.SetCanceled();

                            continue;
                        }

                        Interlocked.Increment(ref _activeThreads);

                        try
                        {
                            var result = workItem.work();

                            workItem.task.SetResult(result);
                        }
                        catch (Exception ex)
                        {
                            workItem.task.SetException(ex);
                        }

                        Interlocked.Decrement(ref _activeThreads);
                    }
                }
            }
        }

        public void Dispose()
        {
            _disposed.Cancel();
        }

        ~DedicatedWorkerPool()
        {
            Dispose();
        }
    }
}