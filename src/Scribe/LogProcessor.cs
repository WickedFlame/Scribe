using System;
using System.Collections.Generic;

namespace Scribe
{
    public class LogProcessor : ILogProcessor, IDisposable
    {
        private readonly List<ILogEntry> _logEntries;
        private ILogManager _logManager;

        public LogProcessor():this(new LogManager())
        {
            _logEntries = new List<ILogEntry>();
        }

        public LogProcessor(ILogManager manager)
        {
            _logManager = manager;
            _logEntries = new List<ILogEntry>();
        }

        /// <summary>
        /// Gets the Logmanager associated with the processor
        /// </summary>
        public ILogManager Manager => _logManager;

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
        /// <param name="entry">The log entry</param>
        public void ProcessLog(ILogEntry entry)
        {
            //if (row.LogLevel > MinimalLogLevel)
            //{
            //    return;
            //}
            
            foreach (var logger in _logManager.Writers)
            {
                logger.Write(entry);
            }
        }

        public void Dispose()
        {
        }
    }
}
