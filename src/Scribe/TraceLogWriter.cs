using System.Diagnostics;

namespace Scribe
{
    public class TraceLogWriter : ILogWriter
    {
        public void Write(ILogEntry logEntry)
        {
            Trace.WriteLine(logEntry.ToString());
        }
    }
}
