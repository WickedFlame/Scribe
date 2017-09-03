namespace Scribe
{
    /// <summary>
    /// Interface that defines the basic functions for the logger
    /// </summary>
    public interface ILogger
    {
        //CategoryProvider Categories { get; }

        /// <summary>
        /// Adds a log writer to the logger
        /// </summary>
        /// <param name="writer">The log writer</param>
        void AddWriter(ILogWriter writer);

        /// <summary>
        /// Writes a log message with the passed object. Calls ToString on the message object if no formatter is supplied
        /// </summary>
        /// <param name="logEntry">The logentry</param>
        void Write(ILogEntry logEntry);
    }
}
