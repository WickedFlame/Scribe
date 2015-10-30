using System;

namespace Scribe.Integration.Test
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void Write(ILogEntry logEntry)
        {
            Console.WriteLine(logEntry.ToString());
        }
    }
}
