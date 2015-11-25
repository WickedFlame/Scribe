using System;

namespace Scribe
{
    /// <summary>
    /// The default logger
    /// </summary>
    public class Logger : ILogger
    {
        private readonly Lazy<ILogProcessor> _logProcessor;

        /// <summary>
        /// Creates a instance of the default logger
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        public Logger(ILoggerFactory loggerFactory)
        {
            _logProcessor = new Lazy<ILogProcessor>(() => loggerFactory.GetProcessor());
        }

        /// <summary>
        /// Write the log entry
        /// </summary>
        /// <param name="logEntry">The item to log</param>
        public void Write(ILogEntry logEntry)
        {
            _logProcessor.Value.ProcessLog(logEntry);
        }
    }
}
