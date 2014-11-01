using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Scribe.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var manager = new LogManager();
            manager.AddListner(new LogTraceListener());

            Trace.Write("Test");
            Trace.TraceError("Error message");

            // give the logthread time to write logqueue
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var processor = manager.LoggerFactory.GetProcessor();
            Assert.IsTrue(processor.LogEntries.First().Message == "Test");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var loggerFactory = new LoggerFactory();
            var traceLogger = new TraceLogger();
            loggerFactory.AddLogger("tracelogger", () => traceLogger);
            loggerFactory.AddLogger("tracelogger2", () => traceLogger);

            var logger = loggerFactory.CreateLogger();
            logger.Write("test", TraceType.Error);

            logger = loggerFactory.CreateLogger();
            logger.Write("logger 2", TraceType.Critical);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var manager = new LogManager();
            manager.AddListner(new LogTraceListener());

            var traceLogger = new TraceLogger();
            manager.AddLogger(traceLogger);

            traceLogger.Write("Test");
            traceLogger.Write("Error message");

            // give the logthread time to write logqueue
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var processor = manager.LoggerFactory.GetProcessor();
            Assert.IsTrue(processor.LogEntries.First().Message == "Test");
        }
    }
}
