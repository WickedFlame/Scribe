using System;
using System.Collections.Generic;

namespace Scribe
{
    public interface ILogManager
    {
        /// <summary>
        /// Gets the log writers assigned to this manager
        /// </summary>
        IEnumerable<ILogWriter> Writers { get; }
        
        LogLevel MinimalLogLevel { get; }
        
        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        ILogManager AddWriter(ILogWriter writer);

        ILogManager SetMinimalLogLevel(LogLevel logLevel);
    }
}