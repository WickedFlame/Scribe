
namespace Scribe
{
    /// <summary>
    /// Defines the interface for loggers that listen do logsources like Trace
    /// </summary>
    public interface IListener
    {
        /// <summary>
        /// Initialize the logger with the loggerfactory
        /// </summary>
        /// <param name="loggerFactory">The loggerfactory containing the logging mechanism</param>
        void Initialize(ILoggerFactory loggerFactory);
    }
}
