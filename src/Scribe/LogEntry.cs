using System;

namespace Scribe
{
    public class LogEntry
    {
        public LogEntry(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null)
        {
            Message = message;
            TraceType = traceType;
            Categroy = category;
            LogTime = logtime ?? DateTime.UtcNow;
        }

        public string Message { get; private set; }

        public TraceType TraceType { get; private set; }

        public string Categroy { get; private set; }

        public DateTime LogTime { get; private set; }
    }
}
