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
        //readonly ILoggerFactory _loggerFactory;
        readonly ILogManager _logManager;

        //public LogProcessor(ILoggerFactory loggerFactory)
        //{
        //    _loggerFactory = loggerFactory;

        //    _loggingThread = new Thread(new ThreadStart(ProcessQueue));
        //    _loggingThread.IsBackground = true;
        //    // this is performed from a bg thread, to ensure the queue is serviced from a single thread
        //    _loggingThread.Start();
        //}

        public LogProcessor(ILogManager manager)
        {
            _logManager = manager;

            _loggingThread = new Thread(new ThreadStart(ProcessQueue));
            _loggingThread.IsBackground = true;
            // this is performed from a bg thread, to ensure the queue is serviced from a single thread
            _loggingThread.Start();
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
            while (true)
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
            }
        }

        public void ProcessLog(LogEntry row)
        {
            lock (_queue)
            {
                _queue.Enqueue(() => LogMessageAsync(row));
            }

            _hasNewItems.Set();
        }

        protected void LogMessageAsync(LogEntry row)
        {
            //TODO: Test if a logentry already exists so the Traceing can be stopped



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
