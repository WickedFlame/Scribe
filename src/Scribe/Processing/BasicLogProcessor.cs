using System;
using System.Collections.Generic;

namespace Scribe.Processing
{
    public class BasicLogProcessor : ILogProcessor, IDisposable
    {
        private readonly List<ILogEntry> _logEntries;
        private ILogManager _manager;

        public BasicLogProcessor():this(new LogManager())
        {
            _logEntries = new List<ILogEntry>();
        }

        public BasicLogProcessor(ILogManager manager)
        {
            _manager = manager;
            _logEntries = new List<ILogEntry>();
        }

        /// <summary>
        /// Gets the Logmanager associated with the processor
        /// </summary>
        public ILogManager Manager => _manager;

        /// <summary>
        /// Initizalize the logprocessor with the manager
        /// </summary>
        /// <param name="manager">The logmanager</param>
        public void Initialize(ILogManager manager)
        {
            _manager = manager;
        }
        
        /// <summary>
        /// Processes and stores the log
        /// </summary>
        /// <param name="entry">The log entry</param>
        public void ProcessLog(ILogEntry entry)
        {
            if (entry.LogLevel < _manager.MinimalLogLevel)
            {
                return;
            }

            foreach (var logger in _manager.Writers)
            {
                logger.Write(entry);
            }
        }

        public void Dispose()
        {
        }
    }
}
