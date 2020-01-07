using System;
using System.Collections.Generic;
using System.Text;

namespace Scribe
{
    public static class LoggerListenerExtensions
    {
        public static ILogger SetTraceListener(this ILogger logger)
        {
            var listener = new TraceListener(logger);

            return logger;
        }
    }
}
