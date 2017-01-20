using System;

namespace Scribe
{
    /// <summary>
    /// The default logger
    /// </summary>
    public class Logger : ILogger
    {
        private readonly ILogManager _manager;

        /// <summary>
        /// Creates a instance of the default logger
        /// </summary>
        /// <param name="processor">The log processor</param>
        public Logger(ILogManager manager)
        {
            _manager = manager;
        }
        
        /// <summary>
        /// Write the log entry
        /// </summary>
        /// <param name="logEntry">The item to log</param>
        public void Write(ILogEntry logEntry)
        {
            if (logEntry.LogLevel < _manager.MinimalLogLevel)
            {
                return;
            }

            _manager.Processor.ProcessLog(logEntry);
        }
    }
}
