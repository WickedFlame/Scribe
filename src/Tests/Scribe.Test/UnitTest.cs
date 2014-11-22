using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Scribe.Test
{
    [TestClass]
    public class UnitTest
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
            loggerFactory.Manager.AddLogger(traceLogger, "tracelogger");
            loggerFactory.Manager.AddLogger(traceLogger, "tracelogger2");

            var logger = loggerFactory.GetLogger();
            logger.Write("test", TraceType.Error);

            logger = loggerFactory.GetLogger();
            logger.Write("logger 2", TraceType.Critical);
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

        [TestMethod]
        public void LoggExceptionWithFormatter()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);

            var manager = new LogManager();
            var logger = manager.LoggerFactory.GetLogger();
            logger.Write(exception2, formatter: e =>
            {
                var sb = new StringBuilder();
                var ex = e;
                while (ex != null)
                {
                    sb.AppendLine(ex.Message);
                    ex = ex.InnerException;
                }

                return sb.ToString();
            });

            // give the logthread time to write logqueue
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var processor = manager.LoggerFactory.GetProcessor();
            Assert.IsTrue(processor.LogEntries.First().Message == "Exception 2\r\nException 1\r\n");
        }
    }
}
