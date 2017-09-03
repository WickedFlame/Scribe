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
        //public LogLevel MinimalLogLevel { get; set; } = LogLevel.Verbose;
        
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
    }
}
