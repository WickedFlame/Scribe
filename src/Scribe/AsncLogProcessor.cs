using System;
using System.Collections.Generic;
using System.Threading;

namespace Scribe
{
    public class AsncLogProcessor : ILogProcessor, IDisposable
    {
        private readonly Queue<Action> _queue = new Queue<Action>();
        private readonly ManualResetEvent _hasNewItems = new ManualResetEvent(false);
        private readonly ManualResetEvent _terminate = new ManualResetEvent(false);
        private readonly ManualResetEvent _waiting = new ManualResetEvent(false);

        private readonly IList<ILogEntry> _logEntries;

        private readonly Thread _loggingThread;
        private readonly Thread _mainThread;

        private readonly ILogManager _logManager;
        private bool _isThreadAlive;

        public AsncLogProcessor(ILogManager manager)
        {
            _logManager = manager;
            _isThreadAlive = true;
            _logEntries = new List<ILogEntry>();

            // this is performed from a bg thread, to ensure the queue is serviced from a single thread
            _loggingThread = new Thread(new ThreadStart(ProcessQueue));
            _loggingThread.IsBackground = true;
            _loggingThread.Start();

            // find a way to close the child thread when main thread completes
            _mainThread = Thread.CurrentThread;
        }

        public IEnumerable<ILogEntry> LogEntries
        {
            get
            {
                return _logEntries;
            }
        }
        
        private void ProcessQueue()
        {
            while (_isThreadAlive)
            {
                _waiting.Set();
                int i = ManualResetEvent.WaitAny(new WaitHandle[] { _hasNewItems, _terminate });

                // terminate was signaled 
                if (i == 1)
                {
                    return;
                }

                _hasNewItems.Reset();
                _waiting.Reset();

                Queue<Action> queueCopy;
                lock (_queue)
                {
                    queueCopy = new Queue<Action>(_queue);
                    _queue.Clear();
                }

                foreach (var log in queueCopy)
                {
                    log();
                }

                // let the thread wait for next run
                Thread.Sleep(TimeSpan.FromSeconds(0.5));

                if (!_mainThread.IsAlive)
                {
                    return;
                }
            }
        }

        public void ProcessLog(ILogEntry row)
        {
            lock (_queue)
            {
                _queue.Enqueue(() => AsyncLogMessage(row));
            }

            _hasNewItems.Set();
        }

        protected void AsyncLogMessage(ILogEntry row)
        {
            _logEntries.Add(row);

            foreach (var logger in _logManager.Writers.Values)
            {
                logger().Write(row.Message, row.LogLevel, category: row.Category, logtime: row.LogTime);
            }
        }

        public void Flush()
        {
            _waiting.WaitOne();
        }

        public void Dispose()
        {
            _terminate.Set();
            _loggingThread.Join();
        }
    }
}
