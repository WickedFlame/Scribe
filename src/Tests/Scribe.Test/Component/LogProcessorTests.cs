using NUnit.Framework;
using System.Linq;
using Scribe.Processing;

namespace Scribe.Test.Component
{
    [TestFixture]
    public class LogProcessorTests
    {
        [Test]
        [Ignore("The logprocessor should only pass the logs to the writers")]
        public void LogProcessor_ProcessLogTest()
        {
            var processor = new BasicLogProcessor(new LogManager());
            processor.ProcessLog(new LogEntry("Message"));

            //Assert.IsTrue(processor.LogEntries.Count() == 1);
        }

        [Test]
        [Ignore("The logprocessor should only pass the logs to the writers")]
        public void LogProcessor_FlushTest()
        {
            var processor = new BasicLogProcessor(new LogManager());
            processor.ProcessLog(new LogEntry("Message"));

            //Assert.IsTrue(processor.LogEntries.Any());

            //processor.Flush();

            //Assert.IsFalse(processor.LogEntries.Any());
        }
    }
}
