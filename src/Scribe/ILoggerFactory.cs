
namespace Scribe
{
    /// <summary>
    /// Represents a ILoggerFactory that provides a ILogger
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Gets a instance of the ILogger
        /// </summary>
        /// <returns>A new instance of a ILogger</returns>
        ILogger GetLogger();

        /// <summary>
        /// Set a logprocessor that is used to pass the log entires from the listeners to the writers
        /// </summary>
        /// <param name="processor">The processor</param>
        /// <returns>this instance of the factory</returns>
        ILoggerFactory SetProcessor(ILogProcessor processor);
        
        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        /// <returns>this instance of the factory</returns>
        ILoggerFactory AddWriter(ILogWriter writer);
    }
}