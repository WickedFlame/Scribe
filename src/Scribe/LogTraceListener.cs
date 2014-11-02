using System.Diagnostics;

namespace Scribe
{
    public class LogTraceListener : TraceListener, IListener
    {
        ILogger _logger;

        public void Initialize(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger();

            Trace.Listeners.Add(this);
        }

        public override void Write(string message)
        {
            _logger.Write(message, TraceType.Information);
        }

        public override void WriteLine(string message)
        {
            _logger.Write(message, TraceType.Information);
        }
    }
}
