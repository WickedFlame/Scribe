using System;

namespace Scribe
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly Lazy<ILogManager> _logManager;

        /// <summary>
        /// Creates a new LoggerFactory. The LogManager will be created when neede
        /// </summary>
        public LoggerFactory()
        {
            _logManager = new Lazy<ILogManager>(() => new LogManager(this));
            if (LogManager.HasConfiguration())
            {
                Manager.Initialize();
            }
        }

        /// <summary>
        /// Creates a new LoggerFactory with the LogManager passed as parameter
        /// </summary>
        /// <param name="manager">The logmanager for this instance</param>
        public LoggerFactory(ILogManager manager)
        {
            _logManager = new Lazy<ILogManager>(() => manager);
        }

        /// <summary>
        /// Gets or sets a custom logger that will be used to log messages
        /// </summary>
        public static Func<ILogger> LoggerCallback { get; set; }

        /// <summary>
        /// Gets the ILogManager associated with this LoggerFactory
        /// </summary>
        public ILogManager Manager
        {
            get
            {
                return _logManager.Value;
            }
        }

        /// <summary>
        /// Gets a instance of the ILogger
        /// </summary>
        /// <returns>A logger</returns>
        public ILogger GetLogger()
        {
            // if there is an override call that one
            if (LoggerCallback != null)
            {
                return LoggerCallback();
            }

            // return a default logger
            return new Logger(this);
        }

        /// <summary>
        /// Gets a instance of the ILogProcessor
        /// </summary>
        /// <returns>The Logprocessor</returns>
        public ILogProcessor GetProcessor()
        {
            return Manager.Processor;
        }

        /// <summary>
        /// Set a logprocessor that is used to pass the log entires from the listeners to the writers
        /// </summary>
        /// <param name="processor">The processor</param>
        public void SetProcessor(ILogProcessor processor)
        {
            Manager.SetProcessor(processor);
        }

        /// <summary>
        /// Add a log listener to the log manager
        /// </summary>
        /// <param name="listener">The listener</param>
        public void AddListener(IListener listener)
        {
            Manager.AddListener(listener);
        }

        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        public void AddWriter(ILogWriter writer)
        {
            Manager.AddWriter(writer);
        }
    }
}
