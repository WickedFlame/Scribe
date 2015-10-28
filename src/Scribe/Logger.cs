using System;

namespace Scribe
{
    internal class Logger : ILogger
    {
        private readonly Lazy<ILogProcessor> _logProcessor;

        public Logger(ILoggerFactory loggerFactory)
        {
            _logProcessor = new Lazy<ILogProcessor>(() => loggerFactory.GetProcessor());
        }

        public void Write(ILogEntry logEntry)
        {
            _logProcessor.Value.ProcessLog(logEntry);
        }
    }
}
