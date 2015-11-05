using System;

namespace Scribe
{
    /// <summary>
    /// A log entry
    /// </summary>
    public class LogEntry : ILogEntry
    {
        public LogEntry(string message, LogLevel logLevel = LogLevel.Information, Priority priority = Priority.Medium, string category = null, DateTime? logtime = null)
        {
            Message = message;
            LogLevel = logLevel;
            Priority = priority;
            Category = category;
            LogTime = logtime ?? DateTime.UtcNow;
        }

        /// <summary>
        /// Gets te log message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the log level
        /// </summary>
        public LogLevel LogLevel { get; private set; }

        /// <summary>
        /// Gets the log priority
        /// </summary>
        public Priority Priority { get; private set; }

        /// <summary>
        /// Gets the log category
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Gets the log time
        /// </summary>
        public DateTime LogTime { get; private set; }

        public override string ToString()
        {
            var logString = string.Format("## LogLevel: [{0}] Priority: [{1}] Logtime: [{2}]", LogLevel, Priority, LogTime);

            if (!string.IsNullOrEmpty(Category))
            {
                logString += string.Format(" Category: [{0}]", Category);
            }

            if (!string.IsNullOrEmpty(Message))
            {
                logString += string.Format(" Message: [{0}]", Message);
            }

            return logString;
        }
    }
}
