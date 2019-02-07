using System;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBoundTasks
{
    public class AsyncOperator<T> : IDisposable
    {
        readonly Thread workerThread;
        readonly SemaphoreSlim semaphore;
        readonly ManualResetEvent waitEvent;

        bool disposed;
        Action<T> currentWork;
        TaskCompletionSource<T> currentWorkTask;

        public AsyncOperator(T target)
        {
            Target = target;

            semaphore = new SemaphoreSlim(1, 1);
            waitEvent = new ManualResetEvent(false);
            workerThread = new Thread(Process);

            workerThread.Start();
        }

        public async Task<T> EnqueueOperationAsync(Action<T> operation, CancellationToken cancellationToken = default)
        {
            await semaphore.WaitAsync(cancellationToken);

            var completionSource = new TaskCompletionSource<T>(Target);

            currentWork = operation;
            currentWorkTask = completionSource;

            waitEvent.Set();

            await completionSource.Task
                .ConfigureAwait(false);

            var result = completionSource.Task.Result;

            semaphore.Release();

            return result;
        }

        public T Target { get; }

        void Process()
        {
            while (!disposed)
            {
                waitEvent.WaitOne();

                try
                {
                    currentWork.Invoke(Target);
                    currentWorkTask.SetResult(Target);
                }
                catch (Exception ex)
                {
                    currentWorkTask.SetException(ex);
                }

                currentWork = null;
                currentWorkTask = null;

                waitEvent.Reset();
            }

            currentWorkTask?.SetCanceled();
        }

        public void Dispose()
        {
            disposed = true;
            semaphore?.Dispose();
            waitEvent?.Dispose();
        }
    }
}
