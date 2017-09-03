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
        public void LoggExceptionWithFormatter()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);

            var manager = new LogManager()
                .SetProcessor(new LogProcessor());

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

            var processor = new LoggerFactory(manager).GetProcessor();
            Assert.IsTrue(writer.LogEntries.Any());
            //Assert.IsTrue(processor.LogEntries.First().Message == "Exception 2\r\nException 1\r\n");
        }
    }
}
