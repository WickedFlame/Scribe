using System;

namespace Scribe.Integration.Test
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void Write<T>(T message, LogLevel traceType = LogLevel.Information, string category = null, DateTime? logtime = null, Func<T, string> formatter = null)
        {
            var msg = formatter != null ? formatter(message) : message.ToString();
            Console.WriteLine(msg);
        }
    }
}
