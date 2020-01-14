using System;

namespace Scribe
{
    /// <summary>
    /// A log entry
    /// </summary>
    public class LogEntry : ILogEntry
    {
        /// <summary>
        /// Creates a new instance of the logentry
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="priority"></param>
        /// <param name="category"></param>
        /// <param name="logtime"></param>
        /// <param name="source"></param>
        public LogEntry(string message, LogLevel logLevel = LogLevel.Information, Priority priority = Priority.Medium, string category = null, DateTime? logtime = null, string source = null)
        {
            Message = message;
            LogLevel = logLevel;
            Priority = priority;
            Category = category;
            LogTime = logtime ?? DateTime.UtcNow;
            Source = source;
        }

        /// <summary>
        /// Gets te log message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the log level
        /// </summary>
        public LogLevel LogLevel { get; }

        /// <summary>
        /// Gets the log priority
        /// </summary>
        public Priority Priority { get; }

        /// <summary>
        /// Gets the log category
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Gets the log time
        /// </summary>
        public DateTime LogTime { get; }

        /// <summary>
        /// Gets or sets the source that wrote the log
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Writes a custom string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var logString = $"## LogLevel: [{LogLevel}] Priority: [{Priority}] Logtime: [{LogTime}]";

            if (!string.IsNullOrEmpty(Category))
            {
                logString += $" Category: [{Category}]";
            }

            if (!string.IsNullOrEmpty(Message))
            {
                logString += $" Message: [{Message}]";
            }

            return logString;
        }
    }
}
