using System;
using System.Collections.Generic;
using System.Threading;

namespace Scribe
{
    public class AsyncLogProcessor : ILogProcessor, IDisposable
    {
        private readonly Queue<Action> _queue = new Queue<Action>();
        private readonly ManualResetEvent _hasNewItems = new ManualResetEvent(false);
        private readonly ManualResetEvent _terminate = new ManualResetEvent(false);
        private readonly ManualResetEvent _waiting = new ManualResetEvent(false);

        private readonly IList<ILogEntry> _logEntries;

        private readonly Thread _loggingThread;
        private readonly Thread _mainThread;

        private ILogManager _logManager;
        private bool _isThreadAlive;

        public AsyncLogProcessor(ILogManager manager)
            : this()
        {
            _logManager = manager;
        }

        public AsyncLogProcessor()
        {
            _isThreadAlive = true;
            _logEntries = new List<ILogEntry>();

            // this is performed from a bg thread, to ensure the queue is serviced from a single thread
            _loggingThread = new Thread(new ThreadStart(ProcessQueue));
            _loggingThread.IsBackground = true;
            _loggingThread.Start();

            // find a way to close the child thread when main thread completes
            _mainThread = Thread.CurrentThread;
        }

        /// <summary>
        /// Initizalize the logprocessor with the manager
        /// </summary>
        /// <param name="logManager">The logmanager</param>
        public void Initialize(ILogManager logManager)
        {
            _logManager = logManager;
        }

        /// <summary>
        /// Processes and stores the log
        /// </summary>
        /// <param name="row">The log entry</param>
        public void ProcessLog(ILogEntry row)
        {
            //if (row.LogLevel > MinimalLogLevel)
            //{
            //    return;
            //}

            lock (_queue)
            {
                _queue.Enqueue(() => AsyncLogMessage(row));
            }

            _hasNewItems.Set();
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

        protected void AsyncLogMessage(ILogEntry entry)
        {
            foreach (var logger in _logManager.Writers)
            {
                logger.Write(entry);
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
