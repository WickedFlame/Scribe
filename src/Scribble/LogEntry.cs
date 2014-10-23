using System;

namespace Scribble
{
    public class LogEntry
    {
        public LogEntry(string message, string category = null, DateTime? logtime = null)
        {
            Message = message;
            Categroy = category;
            LogTime = logtime ?? DateTime.UtcNow;
        }

        public string Message { get; private set; }

        public string Categroy { get; private set; }

        public DateTime LogTime { get; private set; }
    }
}
