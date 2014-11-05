using System;
using System.Collections.Generic;

namespace Scribe
{
    public delegate ILogWriter GetLogWriterCallback();

    public interface ILogManager
    {
        ILoggerFactory LoggerFactory { get; }

        ILogProcessor Processor { get; }

        Dictionary<string, GetLogWriterCallback> Writers { get; }

        IEnumerable<IListener> Listeners { get; }

        void AddListener(IListener listener);

        void AddLogger(ILogWriter logger, string name = null);

        void Initialize();
    }
}