using System;

namespace Scribe
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Writes a log message with the passed object. Calls ToString on the message object if no formatter is supplied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="traceType"></param>
        /// <param name="priority"></param>
        /// <param name="category"></param>
        /// <param name="logtime"></param>
        /// <param name="formatter"></param>
        public static void Write<T>(this ILogger logger, T message, LogLevel traceType = LogLevel.Information, Priority priority = Priority.Medium, string category = null, DateTime? logtime = null, Func<T, string> formatter = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var messageString = formatter != null ? formatter(message) : message.ToString();

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

        /// <summary>
        /// Sets a new processor to the logger
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="processor"></param>
        /// <returns></returns>
        public static Logger SetProcessor(this Logger logger, ILogProcessor processor)
        {
            logger.Processor = processor;
            return logger;
        }
    }
}
