using System;
using System.Collections.Generic;
using System.Threading;

namespace Scribe
{
    public interface ILogProcessor
    {
        IEnumerable<LogEntry> LogEntries { get; }

        void ProcessLog(LogEntry row);

        void Flush();
    }
}