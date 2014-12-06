using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scribe
{
    internal class LogProcessor : ILogProcessor, IDisposable
    {
        readonly Queue<Action> _queue = new Queue<Action>();
        readonly ManualResetEvent _hasNewItems = new ManualResetEvent(false);
        readonly ManualResetEvent _terminate = new ManualResetEvent(false);
        readonly ManualResetEvent _waiting = new ManualResetEvent(false);

        readonly Thread _loggingThread;
        readonly Thread _mainThread;

        readonly ILogManager _logManager;
        private bool _isThreadAlive;

        public LogProcessor(ILogManager manager)
        {
            _logManager = manager;
            _isThreadAlive = true;
            _loggingThread = new Thread(new ThreadStart(ProcessQueue));
            _loggingThread.IsBackground = true;
            // this is performed from a bg thread, to ensure the queue is serviced from a single thread
            _loggingThread.Start();


            // find a way to close the child thread when main thread completes
            _mainThread = Thread.CurrentThread;
        }

        IEnumerable<LogEntry> ILogProcessor.LogEntries
        {
            get
            {
                return LogEntries;
            }
        }

        IList<LogEntry> _logEntries;
        public IList<LogEntry> LogEntries
        {
            get
            {
                if (_logEntries == null)
                    _logEntries = new List<LogEntry>();
                return _logEntries;
            }
        }


        void ProcessQueue()
        {
            while (_isThreadAlive)
            {
                _waiting.Set();
                int i = ManualResetEvent.WaitAny(new WaitHandle[] { _hasNewItems, _terminate });
                // terminate was signaled 
                if (i == 1) 
                    return;

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
                    return;
            }
        }

        public void ProcessLog(LogEntry row)
        {
            lock (_queue)
            {
                _queue.Enqueue(() => AsyncLogMessage(row));
            }

            _hasNewItems.Set();
        }

        protected void AsyncLogMessage(LogEntry row)
        {
            LogEntries.Add(row);

            foreach (var logger in _logManager.Writers.Values)
            {
                logger().Write(row.Message, row.TraceType, row.Categroy, row.LogTime);
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
