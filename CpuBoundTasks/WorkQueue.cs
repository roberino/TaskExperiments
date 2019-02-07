using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace CpuBoundTasks
{
    public sealed class WorkQueue<T>
    {
        readonly ConcurrentQueue<T> _innerQueue;
        readonly ManualResetEvent _onDataEvent;

        public WorkQueue()
        {
            _innerQueue = new ConcurrentQueue<T>();
            _onDataEvent = new ManualResetEvent(false);
        }

        public int Count => _innerQueue.Count;

        public bool IsEmpty => _innerQueue.IsEmpty;

        public bool WaitForData(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _onDataEvent.WaitOne(50);

                if (!IsEmpty)
                {
                    return true;
                }
            }

            return false;
        }

        public void Enqueue(T item)
        {
            _innerQueue.Enqueue(item);
            _onDataEvent.Set();
        }

        public IEnumerator<T> GetEnumerator() => _innerQueue.GetEnumerator();

        public bool TryDequeue(out T result)
        {
            if (!_innerQueue.TryDequeue(out result))
                return false;

            if (IsEmpty)
            {
                _onDataEvent.Reset();
            }

            return true;
        }
    }
}