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

        public LoggerConfiguration AddListener(IListener listener)
        {
            _listners.Add(listener);

            return this;
        }

        public LoggerConfiguration AddWriter(ILogWriter writer)
        {
            _writers.Add(writer);

            return this;
        }

        public LoggerConfiguration SetProcessor(ILogProcessor processor)
        {
            _processor = processor;

            return this;
        }

        public LoggerConfiguration SetMinimalLogLevel(LogLevel loglevel)
        {
            _loglevel = loglevel;

            return this;
        }

        public ILoggerFactory CreateLogger()
        {
            var factory = new LoggerFactory();
            if (_processor != null)
            {
                factory.SetProcessor(_processor);
            }

            if (_loglevel < LogLevel.Verbose)
            {
                var processor = factory.GetProcessor();
                processor.MinimalLogLevel = _loglevel;
            }

            foreach (var listner in _listners)
            {
                factory.AddListener(listner);
            }

            foreach (var writer in _writers)
            {
                factory.AddWriter(writer);
            }

            return factory;
        }
    }
}
