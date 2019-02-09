using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CpuBoundTasks
{
    class Worker
    {
        readonly Thread _workerThread;
        readonly Queue<(Func<object> work, TaskCompletionSource<object> task, CancellationToken cancellationToken)> _work;
        readonly CancellationTokenSource _disposed;

        public Worker(int id)
        {
            _work =
                new Queue<(Func<object> work, 
                    TaskCompletionSource<object> task, CancellationToken cancellationToken)>();

            _workerThread = new Thread(Process);

            _workerThread.Start();

            _disposed = new CancellationTokenSource();
        }

        public int WorkLoad => _work.Count;

        public event Action<Worker> Free; 

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
                while (_work.Count > 0)
                {
                    var workItem = _work.Dequeue();

                    if (workItem.cancellationToken.IsCancellationRequested)
                    {
                        workItem.task.SetCanceled();

                        continue;
                    }

                    try
                    {
                        var result = workItem.work();

                        workItem.task.SetResult(result);
                    }
                    catch (Exception ex)
                    {
                        workItem.task.SetException(ex);
                    }
                }

                Free?.Invoke(this);
            }
        }

        public void Dispose()
        {
            _disposed.Cancel();
        }

        ~Worker()
        {
            Dispose();
        }
    }
}