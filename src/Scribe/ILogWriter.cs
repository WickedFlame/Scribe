
namespace Scribe
{
    public interface ILogWriter// : ILogger
    {
        /// <summary>
        /// Writes a log message with the passed object. Calls ToString on the message object if no formatter is supplied
        /// </summary>
        /// <param name="logEntry">The logentry</param>
        void Write(ILogEntry logEntry);
    }
}
