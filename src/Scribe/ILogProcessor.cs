using System.Collections.Generic;

namespace Scribe
{
    /// <summary>
    /// Interface that provides a logic for processing and storing log entries
    /// </summary>
    public interface ILogProcessor
    {
        /// <summary>
        /// Gets a collection of processed log entries
        /// </summary>
        IEnumerable<ILogEntry> LogEntries { get; }

        /// <summary>
        /// Processes and stores the log
        /// </summary>
        /// <param name="row"></param>
        void ProcessLog(ILogEntry row);

        /// <summary>
        /// Flush the log store
        /// </summary>
        void Flush();
    }
}