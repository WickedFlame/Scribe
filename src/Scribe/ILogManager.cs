using System;
using System.Collections.Generic;

namespace Scribe
{
    public interface ILogManager
    {
        ///// <summary>
        ///// Gets the ILoggerFactory associated with this manager
        ///// </summary>
        //ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// Gets the ILogProcessor associated with this manager
        /// </summary>
        ILogProcessor Processor { get; }

        /// <summary>
        /// Gets the log writers assigned to this manager
        /// </summary>
        IEnumerable<ILogWriter> Writers { get; }

        /// <summary>
        /// Gets the log listeners assigned to this manager
        /// </summary>
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
        void AddWriter(ILogWriter writer);

        void Initialize();
    }
}