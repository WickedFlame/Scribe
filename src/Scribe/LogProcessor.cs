using System.Collections.Generic;

namespace Scribe
{
    public class LogProcessor : ILogProcessor
    {
        private readonly List<ILogEntry> _logEntries;
        private ILogManager _logManager;

        public LogProcessor()
        {
            _logEntries = new List<ILogEntry>();
        }

        public LogProcessor(ILogManager manager)
        {
            _logManager = manager;
            _logEntries = new List<ILogEntry>();
        }

        /// <summary>
        /// Gets or sets the minimal loglevel
        /// </summary>
        public LogLevel MinimalLogLevel { get; set; } = LogLevel.Verbose;

        /// <summary>
        /// Gets the processed logenties
        /// </summary>
        public IEnumerable<ILogEntry> LogEntries => _logEntries;

        /// <summary>
        /// Initizalize the logprocessor with the manager
        /// </summary>
        /// <param name="logManager">The logmanager</param>
        public void Initialize(ILogManager logManager)
        {
            _logManager = logManager;
        }
        
        /// <summary>
        /// Flush the logqueue
        /// </summary>
        public void Flush()
        {
            _logEntries.Clear();
        }

        /// <summary>
        /// Processes and stores the log
        /// </summary>
        /// <param name="row">The log entry</param>
        public void ProcessLog(ILogEntry row)
        {
            if (row.LogLevel > MinimalLogLevel)
            {
                return;
            }

            _logEntries.Add(row);

            foreach (var logger in _logManager.Writers)
            {
                logger.Write(row.Message, row.LogLevel, category: row.Category, logtime: row.LogTime);
            }
        }
    }
}
