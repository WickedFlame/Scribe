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

        /// <summary>
        /// Gets the log listeners assigned to this manager
        /// </summary>
        IEnumerable<IListener> Listeners { get; }

        LogLevel MinimalLogLevel { get; }

        /// <summary>
        /// Add a log listener to the log manager
        /// </summary>
        /// <param name="listener">The listener</param>
        ILogManager AddListener(IListener listener);

        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        ILogManager AddWriter(ILogWriter writer);

        ILogManager SetMinimalLogLevel(LogLevel logLevel);

        void Initialize();
    }
}