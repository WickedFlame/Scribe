using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace Scribe
{
    public class LogManager : ILogManager
    {
        private readonly IList<ILogWriter> _logWriters;
        private LogLevel _minimalLogLevel;

        public LogManager()
        {
            _logWriters = new List<ILogWriter>();
        }
        
        
        
        /// <summary>
        /// Gets the log writers assigned to this manager
        /// </summary>
        public IEnumerable<ILogWriter> Writers => _logWriters;

        public LogLevel MinimalLogLevel => _minimalLogLevel;
        
        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        /// <returns>the logmanager</returns>
        public ILogManager AddWriter(ILogWriter writer)
        {
            var writerType = writer.GetType();
            if (_logWriters.Any(w => w.GetType() == writerType))
            {
                var factory = new LoggerFactory(this);
                factory.GetLogger().Write($"There is already a writer of type {writerType.Name} contained in the collection.");
                return this;
            }

            _logWriters.Add(writer);

            return this;
        }

        public ILogManager SetMinimalLogLevel(LogLevel logLevel)
        {
            _minimalLogLevel = logLevel;

            return this;
        }
    }
}
