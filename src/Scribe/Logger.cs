using System;

namespace Scribe
{
    internal class Logger : ILogger
    {
        readonly Lazy<ILogProcessor> _logProcessor;

        public Logger(ILoggerFactory loggerFactory)
        {
            _logProcessor = new Lazy<ILogProcessor>(() => loggerFactory.GetProcessor());
        }

        public void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null)
        {
            _logProcessor.Value.ProcessLog(new LogEntry(message, traceType, category, logtime ?? DateTime.Now));
        }
    }
}
