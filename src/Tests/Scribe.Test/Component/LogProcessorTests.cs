using NUnit.Framework;
using System.Linq;

namespace Scribe.Test.Component
{
    [TestFixture]
    public class LogProcessorTests
    {
        [Test]
        public void LogProcessor_ProcessLogTest()
        {
            var processor = new LogProcessor(new LogManager());
            processor.ProcessLog(new LogEntry("Message"));

            Assert.IsTrue(processor.ProcessedLogs.Count() == 1);
        }

        [Test]
        public void LogProcessor_FlushTest()
        {
            var processor = new LogProcessor(new LogManager());
            processor.ProcessLog(new LogEntry("Message"));

            Assert.IsTrue(processor.ProcessedLogs.Any());

            processor.Flush();

            Assert.IsFalse(processor.ProcessedLogs.Any());
        }
    }
}
