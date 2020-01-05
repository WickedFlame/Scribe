using System;

namespace Scribe
{
    public class LoggerFactory : ILoggerFactory
    {
        private ILogManager _logManager;
        private ILogProcessor _processor;

        /// <summary>
        /// Creates a new LoggerFactory. The LogManager will be created when neede
        /// </summary>
        public LoggerFactory()
        {
            _logManager = new LogManager();
        }

        /// <summary>
        /// Creates a new LoggerFactory with the LogManager passed as parameter
        /// </summary>
        /// <param name="manager">The logmanager for this instance</param>
        public LoggerFactory(ILogManager manager)
        {
            _logManager = manager;
        }

        /// <summary>
        /// Gets or sets a custom logger that will be used to log messages
        /// </summary>
        public static Func<ILogger> CustomLogger { get; set; }

        /// <summary>
        /// Gets the ILogManager associated with this LoggerFactory
        /// </summary>
        public ILogManager Manager
        {
            get
            {
                return _logManager;
            }
            internal set
            {
                _logManager = value;
            }
        }
        
        /// <summary>
        /// Gets a instance of the ILogger
        /// </summary>
        /// <returns>A logger</returns>
        public ILogger GetLogger()
        {
            // if there is an override call that one
            if (CustomLogger != null)
            {
                return CustomLogger();
            }

            // return a default logger
            var logger = new Logger(Manager);
            if (_processor != null)
            {
                logger.SetProcessor(_processor);
            }

            return logger;
        }

        /// <summary>
        /// Set a logprocessor that is used to pass the log entires from the listeners to the writers
        /// </summary>
        /// <param name="processor">The processor</param>
        /// <returns>this instance of the factory</returns>
        public ILoggerFactory SetProcessor(ILogProcessor processor)
        {
            _processor = processor;

            return this;
        }

        /// <summary>
        /// Add a log listener to the log manager
        /// </summary>
        /// <param name="listener">The listener</param>
        /// <returns>this instance of the factory</returns>
        public ILoggerFactory AddListener(IListener listener)
        {
            Manager.AddListener(listener);

            return this;
        }

        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        /// <returns>this instance of the factory</returns>
        public ILoggerFactory AddWriter(ILogWriter writer)
        {
            Manager.AddWriter(writer);

            return this;
        }

        public ILoggerFactory SetMinimalLogLevel(LogLevel logLevel)
        {
            Manager.SetMinimalLogLevel(logLevel);

            return this;
        }
    }
}
