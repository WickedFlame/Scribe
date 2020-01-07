using System.Collections.Generic;

namespace Scribe
{
    public class LoggerConfiguration
    {
        private readonly IList<ILogWriter> _writers;
        private ILogProcessor _processor;
        private LogLevel _loglevel = LogLevel.Verbose;
        private ILogManager _manager;

        public LoggerConfiguration()
        {
            _writers = new List<ILogWriter>();
        }

        /// <summary>
        /// Add a log writer to the configuration
        /// </summary>
        /// <param name="writer">The writer to add</param>
        /// <returns>The configuration</returns>
        public LoggerConfiguration AddWriter(ILogWriter writer)
        {
            _writers.Add(writer);

            return this;
        }

        /// <summary>
        /// Set the LogProcessor for processing logs
        /// </summary>
        /// <param name="processor">The LogProcessor</param>
        /// <returns>The configuration</returns>
        public LoggerConfiguration SetProcessor(ILogProcessor processor)
        {
            if (_processor != null)
            {
                _processor.Dispose();
            }

            _processor = processor;

            return this;
        }

        /// <summary>
        /// Set the minimal loglevel
        /// </summary>
        /// <param name="loglevel">The minimal loglevel</param>
        /// <returns>The configuration</returns>
        public LoggerConfiguration SetMinimalLogLevel(LogLevel loglevel)
        {
            _loglevel = loglevel;

            return this;
        }

        public LoggerConfiguration SetLogManager(ILogManager manager)
        {
            _manager = manager;

            return this;
        }

        public ILogger BuildLogger()
        {
            var manager = _manager ?? new LogManager();
            
            if (_loglevel > LogLevel.Verbose)
            {
                manager.SetMinimalLogLevel(_loglevel);
            }

            foreach (var writer in _writers)
            {
                manager.AddWriter(writer);
            }

            var logger = new Logger(manager);
            if (_processor != null)
            {
                logger.SetProcessor(_processor);
            }

            return logger;
        }

        public ILoggerFactory BuildFactory()
        {
            var manager = _manager ?? new LogManager();

            if (_loglevel > LogLevel.Verbose)
            {
                manager.SetMinimalLogLevel(_loglevel);
            }

            foreach (var writer in _writers)
            {
                manager.AddWriter(writer);
            }

            var factory = new LoggerFactory(manager);
            return factory;
        }
    }
}
