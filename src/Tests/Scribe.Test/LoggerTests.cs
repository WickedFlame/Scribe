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
            var manager = new LogManager();
            manager.SetProcessor(new LogProcessor());
            manager.SetMinimalLogLevel(LogLevel.Error);

            var logger = new Logger(manager);

            logger.Write(new LogEntry("Log message", LogLevel.Error));

            Assert.That(manager.Processor.LogEntries.Any());
        }

        [Test]
        public void Logger_LoggExceptionWithFormatter_LogLevel_Higher()
        {
            var manager = new LogManager();
            manager.SetProcessor(new LogProcessor());
            manager.SetMinimalLogLevel(LogLevel.Warning);

            var logger = new Logger(manager);

            logger.Write(new LogEntry("Log message", LogLevel.Error));

            Assert.That(manager.Processor.LogEntries.Any());
        }

        [Test]
        public void Logger_LoggExceptionWithFormatter_LogLevel_Lower()
        {
            var manager = new LogManager();
            manager.SetProcessor(new LogProcessor());
            manager.SetMinimalLogLevel(LogLevel.Warning);

            var logger = new Logger(manager);

            logger.Write(new LogEntry("Log message", LogLevel.Information));

            Assert.That(!manager.Processor.LogEntries.Any());
        }
    }
}
