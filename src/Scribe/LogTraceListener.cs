using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe
{
    public class LogTraceListener : TraceListener, IListner
    {
        ILogger _logger;

        public void Initialize(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger();

            Trace.Listeners.Add(this);
        }

        public override void Write(string message)
        {
            _logger.Write(message, TraceType.Information);
        }

        public override void WriteLine(string message)
        {
            _logger.Write(message, TraceType.Information);
        }
    }
}
