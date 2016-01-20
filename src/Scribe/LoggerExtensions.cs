using System;
using System.Text;

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

        public static void Write(this ILogger logger, Exception message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var sb = new StringBuilder();
            var e = message;
            while (e != null)
            {
                sb.AppendLine(e.Message);
                sb.AppendLine("StackTrace:");
                sb.AppendLine(e.StackTrace);

                e = e.InnerException;
            }

            logger.Write(new LogEntry(sb.ToString(), LogLevel.Error, Priority.High, "Exception", DateTime.Now));
        }
    }
}
