﻿using System;

namespace Scribe
{
    /// <summary>
    /// The default logger
    /// </summary>
    public class Logger : ILogger
    {
        private readonly ILogManager _manager;
        private ILogProcessor _processor;

        /// <summary>
        /// Creates a instance of the default logger
        /// </summary>
        public Logger()
            : this(new LogManager())
        {
        }

        /// <summary>
        /// Creates a instance of the default logger
        /// </summary>
        /// <param name="manager">The log manager</param>
        public Logger(ILogManager manager)
            : this(manager, new AsyncLogProcessor(manager))
        {
        }

        /// <summary>
        /// Creates a instance of the default logger
        /// </summary>
        /// <param name="manager">The log manager</param>
        /// <param name="processor">The processor</param>
        public Logger(ILogManager manager, ILogProcessor processor)
        {
            _manager = manager;
            _processor = processor;
        }

        /// <summary>
        /// Gets the ILogManager associated with this logger
        /// </summary>
        public ILogManager Manager => _manager;

        /// <summary>
        /// Gets the ILogProcessor associated with this manager
        /// </summary>
        public ILogProcessor Processor
        {
            get => _processor;
            set
            {
                _processor = value;
                _processor.Initialize(_manager);
            }
        }

        /// <summary>
        /// Write the log entry
        /// </summary>
        /// <param name="logEntry">The item to log</param>
        public void Write(ILogEntry logEntry)
        {
            if (logEntry.LogLevel < _manager.MinimalLogLevel)
            {
                return;
            }

            Processor.ProcessLog(logEntry);
        }
    }
}
