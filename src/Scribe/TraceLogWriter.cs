using System.Diagnostics;

namespace Scribe
{
    public class TraceLogWriter : ILogWriter
    {
        private readonly LogEntryFormatProvider _formatProvider;

        public TraceLogWriter(string formatString = "## LogLevel: [{LogLevel}] Priority: [{Priority}] Logtime: [{LogTime}] Category: [{Category}] Message: [{Message}]")
        {
            _formatProvider = new LogEntryFormatProvider(formatString);
        }

        public void Write(ILogEntry logEntry)
        {
            Trace.WriteLine(_formatProvider.Format(logEntry));
        }
    }
}
