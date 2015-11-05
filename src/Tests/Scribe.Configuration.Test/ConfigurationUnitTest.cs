using System;
using System.Linq;
using System.Diagnostics;
using NUnit.Framework;

namespace Scribe.Configuration.Test
{
    [TestFixture]
    public class ConfigurationUnitTest
    {
        //[Test]
        public void TestConfigurationWithLoggerFactory()
        {
            var factory = new LoggerFactory();
            var manager = factory.Manager;
            manager.SetProcessor(new LogProcessor(manager));

            Assert.IsTrue(manager.Listeners.Any());
            Assert.IsTrue(manager.Listeners.First().GetType() == typeof(TraceLogListener));

            Assert.IsTrue(manager.Writers.Any());
            Assert.IsTrue(manager.Writers.First().Value().GetType() == typeof(MockLogWriter));

            var writer = manager.Writers.First().Value() as MockLogWriter;
            writer.LogEntries.Clear();

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.IsTrue(writer.LogEntries.Count == 10);

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.IsTrue(writer.LogEntries.Count == 20);
        }

        //[Test]
        public void TestConfigurationWithLogManager()
        {
            var manager = new LogManager();
            manager.SetProcessor(new LogProcessor(manager));

            Assert.IsTrue(manager.Listeners.Any());
            Assert.IsTrue(manager.Listeners.First().GetType() == typeof(TraceLogListener));

            Assert.IsTrue(manager.Writers.Any());
            Assert.IsTrue(manager.Writers.First().Value().GetType() == typeof(MockLogWriter));

            var writer = manager.Writers.First().Value() as MockLogWriter;
            writer.LogEntries.Clear();

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.IsTrue(writer.LogEntries.Count == 10);

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.IsTrue(writer.LogEntries.Count == 20);
        }
    }
}
