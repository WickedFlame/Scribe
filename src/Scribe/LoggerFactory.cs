using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe
{
    public delegate ILogger CreateLoggerCallback();

    public class LoggerFactory : ILoggerFactory
    {
        readonly Lazy<ILogProcessor> _processor;

        public LoggerFactory()
        {
            _processor = new Lazy<ILogProcessor>(() => new LogProcessor(this));
        }

        readonly Dictionary<string, CreateLoggerCallback> _logProviders = new Dictionary<string, CreateLoggerCallback>();
        public Dictionary<string, CreateLoggerCallback> LogProviders
        {
            get
            {
                return _logProviders;
            }
        }

        public void AddLogger(string name, CreateLoggerCallback loggerProvider)
        {
            if (!_logProviders.ContainsKey(name))
            {
                _logProviders.Add(name, loggerProvider);
            }
        }

        public ILogger CreateLogger()
        {
            return new Logger(this);
        }

        public ILogProcessor GetProcessor()
        {
            return _processor.Value;
        }
    }
}
