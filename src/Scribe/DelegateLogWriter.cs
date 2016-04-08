using System;

namespace Scribe
{
    /// <summary>
    /// Represents a logwriter that logs to a delegate methode
    /// </summary>
    public class DelegateLogWriter : ILogWriter
    {
        private readonly Action<ILogEntry> _predicate;

        /// <summary>
        /// Creates a log writer that writes to a delegate method
        /// </summary>
        /// <param name="predicate">The delegate method</param>
        public DelegateLogWriter(Action<ILogEntry> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// Write a log entry
        /// </summary>
        /// <param name="logEntry">The log entry</param>
        public void Write(ILogEntry logEntry)
        {
            _predicate.Invoke(logEntry);
        }
    }
}
