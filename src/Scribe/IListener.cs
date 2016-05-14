
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
        /// <param name="manager">The logmanager containing the logging mechanism</param>
        void Initialize(ILogManager manager);
    }
}
