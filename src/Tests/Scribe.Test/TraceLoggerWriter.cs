using System;
using System.Diagnostics;

namespace Scribe.Test
{
    class TraceLoggerWriter : ILogWriter
    {
        public void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null)
        {
            Trace.WriteLine(message);
        }
    }
}
