using System;
using System.Collections.Generic;

namespace Scribe
{
    public delegate ILogWriter GetLogWriterCallback();

    public interface ILogManager
    {
        ILoggerFactory LoggerFactory { get; }

        ILogProcessor Processor { get; }

        Dictionary<string, GetLogWriterCallback> Writers { get; }

        IEnumerable<IListener> Listeners { get; }

        /// <summary>
        /// Set a logprocessor that is used to pass the log entires from the listeners to the writers
        /// </summary>
        /// <param name="processor">The processor</param>
        void SetProcessor(ILogProcessor processor);

        /// <summary>
        /// Add a log listener to the log manager
        /// </summary>
        /// <param name="listener">The listener</param>
        void AddListener(IListener listener);

        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        /// <param name="name">The name of the log wirter</param>
        void AddWriter(ILogWriter writer, string name = null);

        void Initialize();
    }
}