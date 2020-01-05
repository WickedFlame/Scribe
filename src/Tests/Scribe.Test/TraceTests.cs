using NUnit.Framework;
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
            var writer = new QueueLogWriter();

            var manager = new LogManager()
                .AddListener(new TraceListener())
                .AddWriter(writer);
                //.SetProcessor(new LogProcessor());

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

            var logger = new Logger(manager).SetProcessor(new LogProcessor());
            logger.Write("Test");
            logger.Write("Error message");
            
            Assert.IsTrue(writer.LogEntries.First().Message == "Test");
        }
    }
}
