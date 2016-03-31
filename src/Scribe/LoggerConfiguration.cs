using System;
using System.Collections.Generic;

namespace Scribe
{
    public class LoggerConfiguration
    {
        private readonly IList<IListener> _listners;
        private readonly IList<ILogWriter> _writers;

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

        //public LoggerConfiguration SetLogLevel(LogLevel loglevel)
        //{
        //    throw new NotImplementedException();
        //}

        public ILoggerFactory CreateLogger()
        {
            var factory = new LoggerFactory();
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
