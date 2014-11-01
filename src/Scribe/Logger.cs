using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe
{
    internal class Logger : ILogger
    {
        //readonly ILoggerFactory _loggerFactory;
        readonly Lazy<ILogProcessor> _logProcessor;

        public Logger(ILoggerFactory loggerFactory)
        {
            //_loggerFactory = loggerFactory;
            _logProcessor = new Lazy<ILogProcessor>(() => loggerFactory.GetProcessor());
        }

        public void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null)
        {
            //TODO: add message to _loggerFactory.LogQueue
            //TODO: find alternative to _loggerFactory.LogQueue /other place for LogQueue


            _logProcessor.Value.ProcessLog(new LogEntry(message, traceType, category, logtime ?? DateTime.Now));

            //foreach (var logger in _loggerFactory.LogProviders.Values)
            //{
            //    logger().Write(message, traceType, category, logtime);
            //}
        }
    }
}
