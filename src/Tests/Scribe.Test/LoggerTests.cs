using NUnit.Framework;
using System;
using System.Linq;
using System.Text;

namespace Scribe.Test
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void LoggExceptionWithFormatter()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);

            var manager = new LogManager();
            manager.SetProcessor(new LogProcessor(manager));
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

            var processor = manager.LoggerFactory.GetProcessor();
            Assert.IsTrue(processor.ProcessedLogs.Any());
        }

        [Test]
        public void LoggerExtension_LoggException()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);

            var factory = new LoggerFactory();
            var processor = new LogProcessor(factory.Manager);
            factory.SetProcessor(processor);
            var logger = factory.GetLogger();

            logger.Write(exception2);
            
            var expected = new StringBuilder();
            var e = exception2;
            while (e != null)
            {
                expected.AppendLine(e.Message);
                expected.AppendLine("StackTrace:");
                expected.AppendLine(e.StackTrace);

                e = e.InnerException;
            }

            Assert.That(processor.ProcessedLogs.First().Message, Is.EqualTo(expected.ToString()));
        }
    }
}
