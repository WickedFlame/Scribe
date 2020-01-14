using System;

namespace Scribe
{
    public static class LoggerExtensions
    {
        public static void Write<T>(this ILogger logger, T message, LogLevel traceType = LogLevel.Information, Priority priority = Priority.Medium, string category = null, DateTime? logtime = null, Func<T, string> formatter = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            string messageString = formatter != null ? formatter(message) : message.ToString();

            logger.Write(new LogEntry(messageString, traceType, priority, category, logtime ?? DateTime.Now));
        }

        /// <summary>
        /// Adds a log writer to the logger
        /// </summary>
        /// <param name="logger">The logger to add the writer to</param>
        /// <param name="writer">The log writer</param>
        public static Logger AddWriter(this Logger logger, ILogWriter writer)
        {
            logger.Manager.AddWriter(writer);

            return logger;
        }

        public static Logger SetProcessor(this Logger logger, ILogProcessor processor)
        {
            logger.Processor = processor;
            return logger;
        }
    }
}
