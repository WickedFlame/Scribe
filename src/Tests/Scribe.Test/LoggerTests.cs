using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using Scribe.Processing;

namespace Scribe.Test
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void Scribe_Logger_Defaults()
        {
            Assert.That(() =>
            {
                var logger = new Logger();
                logger.Write("test");
            }, Throws.Nothing);
        }

        [Test]
        public void Scribe_Logger_AddWriter()
        {
            var writer = new QueueLogWriter();
            var logger = new Logger()
                .AddWriter(writer);

            Assert.That(logger.Manager.Writers.Single() == writer);
        }

        [Test]
        public void Logger_LoggExceptionWithFormatter_LogLevel_Same()
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .AddWriter(writer)
                .SetMinimalLogLevel(LogLevel.Error);

            var logger = new Logger(manager, new BasicLogProcessor());

            logger.Write(new LogEntry("Log message", LogLevel.Error));

            Assert.That(writer.LogEntries.Any());
        }

        [Test]
        public void Logger_LoggExceptionWithFormatter_LogLevel_Higher()
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .AddWriter(writer)
                .SetMinimalLogLevel(LogLevel.Warning);

            var logger = new Logger(manager, new BasicLogProcessor());

            logger.Write(new LogEntry("Log message", LogLevel.Error));

            Assert.That(writer.LogEntries.Any());
        }

        [Test]
        public void Logger_LoggExceptionWithFormatter_LogLevel_Lower()
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .AddWriter(writer)
                .SetMinimalLogLevel(LogLevel.Warning);

            var logger = new Logger(manager, new BasicLogProcessor());

            logger.Write(new LogEntry("Log message", LogLevel.Information));

            Assert.That(!writer.LogEntries.Any());
        }
    }
}
