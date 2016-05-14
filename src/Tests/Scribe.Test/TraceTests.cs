﻿using NUnit.Framework;
using System.Diagnostics;
using System.Linq;

namespace Scribe.Test
{
    [TestFixture]
    public class TraceTests
    {
        [Test]
        public void BasicTraceListnerTest()
        {
            var manager = new LogManager();
            manager.AddListener(new TraceListener());
            manager.SetProcessor(new LogProcessor());

            Trace.Write("Test");
            Trace.TraceError("Error message");
            
            Assert.IsTrue(manager.Processor.LogEntries.First().Message == "Test");
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
            var manager = new LogManager();
            manager.AddWriter(new TraceLogWriter());
            manager.SetProcessor(new LogProcessor());

            var logger = new LoggerFactory(manager).GetLogger();
            logger.Write("Test");
            logger.Write("Error message");
            
            Assert.IsTrue(manager.Processor.LogEntries.First().Message == "Test");
        }
    }
}
