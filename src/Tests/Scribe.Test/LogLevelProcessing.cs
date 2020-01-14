using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Scribe.Processing;

namespace Scribe.Test
{
    [TestFixture]
    public class LogLevelProcessing
    {
        [TestCase(LogLevel.Critical, 1, TestName = "Scribe_LogLevel_LogProcessor_Critical")]
        [TestCase(LogLevel.Error, 2, TestName = "Scribe_LogLevel_LogProcessor_Error")]
        [TestCase(LogLevel.Warning, 3, TestName = "Scribe_LogLevel_LogProcessor_Warning")]
        [TestCase(LogLevel.Information, 4, TestName = "Scribe_LogLevel_LogProcessor_Information")]
        [TestCase(LogLevel.Verbose, 5, TestName = "Scribe_LogLevel_LogProcessor_Verbose")]
        public void Scribe_LogLevel_LogProcessor(LogLevel minLevel, int count)
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .AddWriter(writer)
                .SetMinimalLogLevel(minLevel);

            var logger = new Logger(manager);

            logger.Write(new LogEntry("Critical", LogLevel.Critical));
            logger.Write(new LogEntry("Error", LogLevel.Error));
            logger.Write(new LogEntry("Warning", LogLevel.Warning));
            logger.Write(new LogEntry("Info", LogLevel.Information));
            logger.Write(new LogEntry("Verbose", LogLevel.Verbose));

            ((LogProcessor) logger.Processor).WaitAll();

            Assert.That(writer.LogEntries.Count() == count);
            Assert.That(writer.LogEntries.All(e => (int) e.LogLevel >= (int) minLevel));
        }

        [TestCase(LogLevel.Critical, 1, TestName = "Scribe_LogLevel_BasicLogProcessor_Critical")]
        [TestCase(LogLevel.Error, 2, TestName = "Scribe_LogLevel_BasicLogProcessor_Error")]
        [TestCase(LogLevel.Warning, 3, TestName = "Scribe_LogLevel_BasicLogProcessor_Warning")]
        [TestCase(LogLevel.Information, 4, TestName = "Scribe_LogLevel_BasicLogProcessor_Information")]
        [TestCase(LogLevel.Verbose, 5, TestName = "Scribe_LogLevel_BasicLogProcessor_Verbose")]
        public void Scribe_LogLevel_BasicLogProcessor(LogLevel minLevel, int count)
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .AddWriter(writer)
                .SetMinimalLogLevel(minLevel);

            var logger = new Logger(manager, new BasicLogProcessor());

            logger.Write(new LogEntry("Critical", LogLevel.Critical));
            logger.Write(new LogEntry("Error", LogLevel.Error));
            logger.Write(new LogEntry("Warning", LogLevel.Warning));
            logger.Write(new LogEntry("Info", LogLevel.Information));
            logger.Write(new LogEntry("Verbose", LogLevel.Verbose));

            Assert.That(writer.LogEntries.Count() == count);
            Assert.That(writer.LogEntries.All(e => (int)e.LogLevel >= (int)minLevel));
        }
    }
}
