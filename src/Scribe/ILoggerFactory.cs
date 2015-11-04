
namespace Scribe
{
    /// <summary>
    /// Represents a ILoggerFactory that provides a ILogger
    /// </summary>
    public interface ILoggerFactory
    {
        //Dictionary<string, CreateLoggerCallback> LogProviders { get; }

        //IEnumerable<IListener> Listeners { get; }

        //void AddLogger(string name, CreateLoggerCallback loggerProvider);

        /// <summary>
        /// Gets a instance of the ILogger
        /// </summary>
        /// <returns></returns>
        ILogger GetLogger();

        /// <summary>
        /// Gets a instance of the ILogProcessor
        /// </summary>
        /// <returns></returns>
        ILogProcessor GetProcessor();

        /// <summary>
        /// Set a logprocessor that is used to pass the log entires from the listeners to the writers
        /// </summary>
        /// <param name="processor">The processor</param>
        void SetProcessor(ILogProcessor processor);

        /// <summary>
        /// Add a log listener to the log manager
        /// </summary>
        /// <param name="listener">The listener</param>
        void AddListener(IListener listener);

        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        /// <param name="name">The name of the log wirter</param>
        void AddWriter(ILogWriter writer, string name = null);
    }
}