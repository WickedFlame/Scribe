using NUnit.Framework;
using System.Linq;

namespace Scribe.Test.UnitTest
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void Scribe_Logger_WriteTest()
        {
            var factory = new LoggerFactory();
            var processor = new LogProcessor(factory.Manager);
            factory.SetProcessor(processor);

            var logger = new Logger(factory);

            logger.Write(new LogEntry("test"));

            Assert.IsTrue(processor.ProcessedLogs.Count() == 1);
        }
    }
}
