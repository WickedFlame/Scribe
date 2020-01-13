using NUnit.Framework;
using System;
using System.Linq;
using System.Text;

namespace Scribe.Test
{
    [TestFixture]
    public class LoggerIntegrationTests
    {
        [Test]
        public void LoggExceptionWithFormatter_MultipleLoggerFactories_Same_Manager()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);

            var manager = new LogManager();

            var writer = new QueueLogWriter();
            var logger = new LoggerFactory(manager)
                .AddWriter(writer)
                .GetLogger();

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

            // create a new logger and add a message
            var secondLogger = new LoggerFactory(manager)
                .GetLogger();

            secondLogger.Write("message 2");

            ((Processing.LogProcessor)((Logger) secondLogger).Processor).WaitAll();

            // ensure the writer is used by both loggers
            var cnt = writer.LogEntries.Count();
            Assert.IsTrue(cnt == 2);
        }

        [Test]
        public void LoggExceptionWithFormatter_MultipleLoggerFactories_Same_Manager_Parallel()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);

            var manager = new LogManager();

            var writer = new QueueLogWriter();
            var logger = new LoggerFactory(manager)
                .SetProcessor(new LogProcessor())
                .AddWriter(writer)
                .GetLogger();

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

            // create a new logger and add a message
            var secondLogger = new LoggerFactory(manager)
                .SetProcessor(new LogProcessor())
                .GetLogger();

            secondLogger.Write("message 2");

            // ensure the writer is used by both loggers
            var cnt = writer.LogEntries.Count();
            Assert.IsTrue(cnt == 2);
        }

        [Test]
        public void LoggExceptionWithFormatter()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);
            
            var writer = new QueueLogWriter();
            var logger = new LoggerFactory()
                .AddWriter(writer)
                .GetLogger();

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

            ((Processing.LogProcessor)((Logger)logger).Processor).WaitAll();

            // ensure the writer uses both loggers
            Assert.IsTrue(writer.LogEntries.First().Message.Contains("Exception 2") && writer.LogEntries.First().Message.Contains("Exception 1"));
        }

        [Test]
        public void LoggExceptionWithFormatter_Parallel()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);

            var writer = new QueueLogWriter();
            var logger = new LoggerFactory()
                .SetProcessor(new LogProcessor())
                .AddWriter(writer)
                .GetLogger();

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

            // ensure the writer uses both loggers
            Assert.IsTrue(writer.LogEntries.First().Message.Contains("Exception 2") && writer.LogEntries.First().Message.Contains("Exception 1"));
        }
    }
}
