using System;
using System.Diagnostics;

namespace Scribe
{
    public class TraceLogListener : TraceListener, IListener
    {
        Lazy<ILogger> _logger;

        protected ILogger Logger
        {
            get
            {
                return _logger != null ? _logger.Value : null;
            }
        }

        public void Initialize(ILoggerFactory loggerFactory)
        {
            _logger = new Lazy<ILogger>(() => loggerFactory.CreateLogger());

            Trace.Listeners.Add(this);
        }

        public override void Write(string message)
        {
            WriteLine(message);
        }

        public override void WriteLine(string message)
        {
            if (Logger == null)
            {
                Trace.WriteLine(string.Format("Log listener {0} is not initialized", GetType().Name));
                return;
            }

            Logger.Write(message, TraceType.Information);
        }
    }
}
