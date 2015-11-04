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

            Assert.IsTrue(processor.LogEntries.Count() == 1);
        }

        [Test]
        public void LogProcessor_FlushTest()
        {
            var processor = new LogProcessor(new LogManager());
            processor.ProcessLog(new LogEntry("Message"));

            Assert.IsTrue(processor.LogEntries.Any());

            processor.Flush();

            Assert.IsFalse(processor.LogEntries.Any());
        }
    }
}
