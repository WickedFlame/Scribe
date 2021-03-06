﻿using NUnit.Framework;
using System.Diagnostics;
using System.Linq;
using Scribe.Processing;

namespace Scribe.Test
{
    [TestFixture]
    public class TraceTests
    {
        [Test]
        public void BasicTraceListnerTest()
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .AddWriter(writer);

            var logger = new Logger(manager, new BasicLogProcessor())
                .SetTraceListener();

            Trace.Write("Test");
            Trace.TraceError("Error message");

            Assert.IsTrue(writer.LogEntries.First().Message == "Test");
        }

        [Test]
        public void TraceLoggerWriterWithMulltipleWritersTest()
        {
            var loggerFactory = new LoggerFactory();
            var traceLogger = new TraceLogWriter();
            loggerFactory.AddWriter(traceLogger);
            loggerFactory.AddWriter(traceLogger);

            var logger = loggerFactory.GetLogger();
            logger.Write("test", LogLevel.Error);

            logger = loggerFactory.GetLogger();
            logger.Write("logger 2", LogLevel.Critical);
        }

        [Test]
        public void BasicTraceLoggerWritertest()
        {
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .AddWriter(new TraceLogWriter())
                .AddWriter(writer);

            var logger = new Logger(manager).SetProcessor(new BasicLogProcessor());
            logger.Write("Test");
            logger.Write("Error message");
            
            Assert.IsTrue(writer.LogEntries.First().Message == "Test");
        }
    }
}
