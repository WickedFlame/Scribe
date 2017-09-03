using System.Collections.Generic;

namespace Scribe
{
    /// <summary>
    /// Interface that provides a logic for processing and storing log entries
    /// </summary>
    public interface ILogProcessor
    {
        /// <summary>
        /// Initizalize the logprocessor with the manager
        /// </summary>
        /// <param name="logManager">The logmanager</param>
        void Initialize(ILogManager logManager);

        /// <summary>
        /// Processes and stores the log
        /// </summary>
        /// <param name="row">The log entry</param>
        void ProcessLog(ILogEntry row);
    }
}