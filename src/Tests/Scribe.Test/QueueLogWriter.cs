using System.Collections.Generic;

namespace Scribe.Test
{
    public class QueueLogWriter : ILogWriter
    {
        private readonly List<ILogEntry> _logEntries;

        public QueueLogWriter()
        {
            _logEntries = new List<ILogEntry>();
        }

        public IEnumerable<ILogEntry> LogEntries => _logEntries;

        public void Write(ILogEntry logEntry)
        {
            _logEntries.Add(logEntry);
        }
    }
}
