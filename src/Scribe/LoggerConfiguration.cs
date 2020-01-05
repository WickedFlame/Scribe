using System;
using System.Collections.Generic;

namespace Scribe
{
    public class LoggerConfiguration
    {
        private readonly IList<IListener> _listners;
        private readonly IList<ILogWriter> _writers;
        private ILogProcessor _processor;
        private LogLevel _loglevel = LogLevel.Verbose;

        public LoggerConfiguration()
        {
            _listners = new List<IListener>();
            _writers = new List<ILogWriter>();
        }

        /// <summary>
        /// Add a log listener to the configuration
        /// </summary>
        /// <param name="listener">The listener to add</param>
        /// <returns>The configuration</returns>
        public LoggerConfiguration AddListener(IListener listener)
        {
            _listners.Add(listener);

            return this;
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

        public ILogger BuildLogger()
        {
            var manager = new LogManager();
            
            if (_loglevel > LogLevel.Verbose)
            {
                manager.SetMinimalLogLevel(_loglevel);
            }

            foreach (var listner in _listners)
            {
                manager.AddListener(listner);
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
            var manager = new LogManager();

            if (_loglevel > LogLevel.Verbose)
            {
                manager.SetMinimalLogLevel(_loglevel);
            }

            foreach (var listner in _listners)
            {
                manager.AddListener(listner);
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
