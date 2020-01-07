using Scribe.Format;
using System.Diagnostics;

namespace Scribe
{
    public class TraceLogWriter : ILogWriter
    {
        private readonly LogEntryFormatProvider _formatProvider;

        /// <summary>
        /// Creates a new instance of a TraceLogWriter
        /// </summary>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}</param>
        public TraceLogWriter(string formatString = "[{LogTime:yyyy-MM-dd HH:mm:SS.fff zzz}] [{LogLevel}] [{Priority}] [{Category}] [{Message}]")
        {
            _formatProvider = new LogEntryFormatProvider(formatString);
        }

        public void Write(ILogEntry logEntry)
        {
            Trace.WriteLine(_formatProvider.Format(logEntry));
        }
    }
}
