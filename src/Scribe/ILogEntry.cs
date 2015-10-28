using System;

namespace Scribe
{
    public interface ILogEntry
    {
        string Message { get; }

        LogLevel LogLevel { get; }

        Priority Priority { get; }

        string Category { get; }

        DateTime LogTime { get; }
    }
}
