using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Scribe.Test
{
    [TestClass]
    public class TraceTests
    {
        [TestMethod]
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

        [TestMethod]
        public void TraceLoggerWriterWithMulltipleWritersTest()
        {
            var loggerFactory = new LoggerFactory();
            var traceLogger = new TraceLogWriter();
            loggerFactory.AddLogger(traceLogger, "tracelogger");
            loggerFactory.AddLogger(traceLogger, "tracelogger2");

            var logger = loggerFactory.GetLogger();
            logger.Write("test", LogLevel.Error);

            logger = loggerFactory.GetLogger();
            logger.Write("logger 2", LogLevel.Critical);
        }

        [TestMethod]
        public void BasicTraceLoggerWritertest()
        {
            var manager = new LogManager();
            //manager.AddListner(new LogTraceListener());
            manager.AddLogger(new TraceLogWriter());


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
