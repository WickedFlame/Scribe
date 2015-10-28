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
    }
}
