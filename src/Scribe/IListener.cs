
namespace Scribe
{
    /// <summary>
    /// Defines the interface for loggers that listen do logsources like Trace
    /// </summary>
    public interface IListener
    {
        void Initialize(ILoggerFactory loggerFactory);
    }
}
