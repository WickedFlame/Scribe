using System;
using System.Collections.Generic;
using System.Text;

namespace Scribe.Processing
{
    public class LogQueue
    {
        private readonly object _syncRoot = new object();
        private readonly Queue<ILogEntry> _queue;

        public LogQueue()
        {
            _queue = new Queue<ILogEntry>();
        }

        public int Count => _queue.Count;

        public void Enqueue(ILogEntry @event)
        {
            lock (_syncRoot)
            {
                _queue.Enqueue(@event);
            }
        }

        public bool TryDequeue(out ILogEntry value)
        {
            lock (_syncRoot)
            {
                if (_queue.Count > 0)
                {
                    value = _queue.Dequeue();

                    return true;
                }

                value = default(ILogEntry);

                return false;
            }
        }
    }
}
