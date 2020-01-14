using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scribe.Processing
{
    /// <summary>
    /// Default LogProcessor
    /// </summary>
    public class LogProcessor : ILogProcessor, IDisposable
    {
        private readonly object _syncRoot = new object();

        private readonly LogQueue _queue;
        private bool _isDisposed;

        private readonly List<WorkerThread> _threads = new List<WorkerThread>();
        private ILogManager _manager;

        /// <summary>
        /// Creates a new instance of the processor
        /// </summary>
        public LogProcessor()
        {
            _queue = new LogQueue();
            SetupWorkers(1);
        }

        /// <summary>
        /// Creates a new instance of the processor
        /// </summary>
        /// <param name="manager">The manager containing all writers and configuration</param>
        public LogProcessor(ILogManager manager)
            : this()
        {
            Initialize(manager);
        }

        /// <summary>
        /// Initializes the LogProcessor with a new manager
        /// </summary>
        /// <param name="manager">The new manager to initialize with</param>
        public void Initialize(ILogManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Waits for all threads to finish processing logs
        /// </summary>
        public void WaitAll()
        {
            var tasks = _threads.Select(t => t.Task)
                .Where(t => t != null)
                .ToList();

            if (tasks.Any())
            {
                Task.WaitAll(tasks.ToArray());
            }
        }

        /// <summary>
        /// Process the logentry
        /// </summary>
        /// <param name="event">The entry to process</param>
        public void ProcessLog(ILogEntry @event)
        {
            if (@event.LogLevel < _manager.MinimalLogLevel)
            {
                return;
            }

            _queue.Enqueue(@event);

            if (_queue.Count / _threads.Count > 10)
            {
                SetupWorkers(_threads.Count + 1);
            }

            foreach (var thread in _threads.ToList())
            {
                thread.Start();
            }
        }

        /// <summary>
        /// Start processing the queue
        /// </summary>
        internal void Process()
        {
            while (_queue.TryDequeue(out var @event))
            {
                foreach (var logger in _manager.Writers)
                {
                    logger.Write(@event);
                }
            }

            CleanupWorkers();
        }

        /// <summary>
        /// Creates and removes worker threads
        /// </summary>
        /// <param name="threadCount">Amount of threads to create</param>
        private void SetupWorkers(int threadCount)
        {
            if (threadCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(threadCount), "The EventBus requires at least one worker thread.");
            }

            lock (_syncRoot)
            {
                for (var i = _threads.Count; i < threadCount; i++)
                {
                    var thread = new WorkerThread(() => Process());

                    _threads.Add(thread);
                }

                var toRemove = _threads.Count - threadCount;

                if (toRemove > 0)
                {
                    foreach (var thread in _threads.ToList())
                    {
                        _threads.Remove(thread);

                        toRemove--;
                    }

                    while (toRemove > 0)
                    {
                        var thread = _threads[_threads.Count - 1];

                        _threads.Remove(thread);

                        toRemove--;
                    }
                }
            }
        }

        /// <summary>
        /// Cleans all worker threads that are done with processing
        /// </summary>
        public void CleanupWorkers()
        {
            lock (_syncRoot)
            {
                foreach (var thread in _threads.ToList())
                {
                    if (thread.IsWorking)
                    {
                        continue;
                    }

                    if (_threads.Count == 1)
                    {
                        return;
                    }

                    _threads.Remove(thread);
                    thread.Dispose();
                }
            }
        }

        /// <summary>
        /// Dispose the processor
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var thread in _threads)
                {
                    thread.Dispose();
                }
            }

            _isDisposed = true;
        }

        internal class WorkerThread : IDisposable
        {
            private readonly object _syncRoot = new object();
            private readonly Action _action;

            private bool _isWorking;

            public WorkerThread(Action action)
            {
                _action = action;
            }

            public Task Task { get; private set; }

            public bool IsWorking
            {
                get
                {
                    lock (_syncRoot)
                    {
                        return _isWorking;
                    }
                }
            }

            public void Dispose()
            {
                lock (_syncRoot)
                {
                    _isWorking = false;
                }
            }

            public void Start()
            {
                lock (_syncRoot)
                {
                    if (_isWorking)
                    {
                        return;
                    }

                    _isWorking = true;
                }

                Task = Task.Factory.StartNew(() =>
                {
                    _action();

                    lock (_syncRoot)
                    {
                        _isWorking = false;
                    }
                }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
        }
    }
}
