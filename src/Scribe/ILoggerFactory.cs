using System.Collections.Generic;

namespace Scribe
{
    public interface ILoggerFactory
    {
        //Dictionary<string, CreateLoggerCallback> LogProviders { get; }

        //IEnumerable<IListener> Listeners { get; }

        //void AddLogger(string name, CreateLoggerCallback loggerProvider);

        ILogger CreateLogger();

        ILogProcessor GetProcessor();
    }
}