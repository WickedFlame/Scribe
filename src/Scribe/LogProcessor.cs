using System.Collections.Generic;

namespace Scribe
{
    public class LogProcessor : ILogProcessor
    {
        private readonly ILogManager _logManager;
        private readonly List<ILogEntry> _logEntries;

        public LogProcessor(ILogManager manager)
        {
            _logManager = manager;
            _logEntries = new List<ILogEntry>();
        }

        public IEnumerable<ILogEntry> LogEntries
        {
            get
            {
                return _logEntries;
            }
        }

        public void Flush()
        {
            _logEntries.Clear();
        }

        public void ProcessLog(ILogEntry row)
        {
            _logEntries.Add(row);

            foreach (var logger in _logManager.Writers.Values)
            {
                logger().Write(row.Message, row.LogLevel, category: row.Category, logtime: row.LogTime);
            }
        }
    }
}
