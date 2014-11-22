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

        //public void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null)
        //{
        //    _logProcessor.Value.ProcessLog(new LogEntry(message, traceType, category, logtime ?? DateTime.Now));
        //}


        public void Write<T>(T message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null, Func<T, string> formatter = null)
        {
            if (message == null)
                throw new ArgumentNullException("messageObject");

            string messageString = formatter != null ? formatter(message) : message.ToString();

            _logProcessor.Value.ProcessLog(new LogEntry(messageString, traceType, category, logtime ?? DateTime.Now));
        }
    }
}
