using NUnit.Framework;
using System;
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
            manager.AddListener(new TraceLogListener());

            Trace.Write("Test");
            Trace.TraceError("Error message");

            // give the logthread time to write logqueue
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var processor = manager.LoggerFactory.GetProcessor();
            Assert.IsTrue(processor.LogEntries.First().Message == "Test");
        }

        [Test]
        public void TraceLoggerWriterWithMulltipleWritersTest()
        {
            var loggerFactory = new LoggerFactory();
            var traceLogger = new TraceLogWriter();
            loggerFactory.AddWriter(traceLogger, "tracelogger");
            loggerFactory.AddWriter(traceLogger, "tracelogger2");

            var logger = loggerFactory.GetLogger();
            logger.Write("test", LogLevel.Error);

            logger = loggerFactory.GetLogger();
            logger.Write("logger 2", LogLevel.Critical);
        }

        [Test]
        public void BasicTraceLoggerWritertest()
        {
            var manager = new LogManager();
            //manager.AddListner(new LogTraceListener());
            manager.AddWriter(new TraceLogWriter());


            var logger = manager.LoggerFactory.GetLogger();
            logger.Write("Test");
            logger.Write("Error message");

            // give the logthread time to write logqueue
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var processor = manager.LoggerFactory.GetProcessor();
            Assert.IsTrue(processor.LogEntries.First().Message == "Test");
        }
    }
}
