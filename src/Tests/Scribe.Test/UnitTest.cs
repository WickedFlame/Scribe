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
            manager.AddListener(new TraceLogListener());

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
            var traceLogger = new TraceLoggerWriter();
            loggerFactory.Manager.AddLogger(traceLogger, "tracelogger");
            loggerFactory.Manager.AddLogger(traceLogger, "tracelogger2");

            var logger = loggerFactory.GetLogger();
            logger.Write("test", TraceType.Error);

            logger = loggerFactory.GetLogger();
            logger.Write("logger 2", TraceType.Critical);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var manager = new LogManager();
            //manager.AddListner(new LogTraceListener());
            manager.AddLogger(new TraceLoggerWriter());


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
