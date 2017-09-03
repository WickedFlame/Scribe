using NUnit.Framework;
using System;
using System.Linq;
using System.Text;

namespace Scribe.Test
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void Logger_LoggExceptionWithFormatter_LogLevel_Same()
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .SetProcessor(new LogProcessor())
                .AddWriter(writer)
                .SetMinimalLogLevel(LogLevel.Error);

            var logger = new Logger(manager);

            logger.Write(new LogEntry("Log message", LogLevel.Error));

            Assert.That(writer.LogEntries.Any());
        }

        [Test]
        public void Logger_LoggExceptionWithFormatter_LogLevel_Higher()
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .SetProcessor(new LogProcessor())
                .AddWriter(writer)
                .SetMinimalLogLevel(LogLevel.Warning);

            var logger = new Logger(manager);

            logger.Write(new LogEntry("Log message", LogLevel.Error));

            Assert.That(writer.LogEntries.Any());
        }

        [Test]
        public void Logger_LoggExceptionWithFormatter_LogLevel_Lower()
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .SetProcessor(new LogProcessor())
                .AddWriter(writer).SetMinimalLogLevel(LogLevel.Warning);

            var logger = new Logger(manager);

            logger.Write(new LogEntry("Log message", LogLevel.Information));

            Assert.That(!writer.LogEntries.Any());
        }
    }
}
