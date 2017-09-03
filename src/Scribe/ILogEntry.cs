using System;

namespace Scribe
{
    /// <summary>
    /// Represents a log entry
    /// </summary>
    public interface ILogEntry
    {
        /// <summary>
        /// The message to be loged
        /// </summary>
        string Message { get; }

        /// <summary>
        /// The loglevel
        /// </summary>
        LogLevel LogLevel { get; }

        /// <summary>
        /// The log priority
        /// </summary>
        Priority Priority { get; }

        /// <summary>
        /// The log category
        /// </summary>
        string Category { get; }

        /// <summary>
        /// The log time
        /// </summary>
        DateTime LogTime { get; }

        string Source { get; set; }

        string Module { get; set; }
    }
}
