using NUnit.Framework;
using System.Linq;

namespace Scribe.Test.Component
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void Scribe_Integration_Logger_Create_Test()
        {
            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.GetLogger();

            Assert.That(logger, Is.Not.Null);
        }

        [Test]
        public void Scribe_Integration_Logger_AddProcessor_Test()
        {
            var loggerFactory = new LoggerFactory();
            var processor = new LogProcessor(loggerFactory.Manager);
            loggerFactory.SetProcessor(processor);

            Assert.That(processor, Is.SameAs(loggerFactory.GetProcessor()));
        }

        [Test]
        public void Scribe_Integration_Logger_WriteMessage_Test()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.SetProcessor(new LogProcessor(loggerFactory.Manager));

            var logger = loggerFactory.GetLogger();            
            logger.Write("This is a log message", LogLevel.Error);

            var processor = loggerFactory.GetProcessor();
            Assert.That(processor.ProcessedLogs.First().Message, Is.EqualTo("This is a log message"));
        }

        [Test]
        public void Scribe_Integration_Logger_WriteEnty_Test()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.SetProcessor(new LogProcessor(loggerFactory.Manager));

            var logger = loggerFactory.GetLogger();
            logger.Write(new LogEntry("This is a log message", LogLevel.Error));

            var processor = loggerFactory.GetProcessor();
            Assert.That(processor.ProcessedLogs.First().Message, Is.EqualTo("This is a log message"));
        }
    }
}
