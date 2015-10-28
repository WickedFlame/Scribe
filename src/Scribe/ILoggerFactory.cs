
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

        void AddLogger(ILogWriter logger, string name = null);
    }
}