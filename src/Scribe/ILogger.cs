namespace Scribe
{
    /// <summary>
    /// Interface that defines the basic functions for the logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes a log message with the passed object. Calls ToString on the message object if no formatter is supplied
        /// </summary>
        /// <param name="logEntry">The logentry</param>
        void Write(ILogEntry logEntry);
    }
}
