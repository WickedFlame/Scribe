using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;

namespace Scribe.Configuration.Test
{
    [TestClass]
    public class ConfigurationUnitTest
    {
        [TestMethod]
        public void TestConfigurationWithLoggerFactory()
        {
            var factory = new LoggerFactory();
            var manager = factory.Manager;

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

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.IsTrue(writer.LogEntries.Count == 10);

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.IsTrue(writer.LogEntries.Count == 20);
        }

        [TestMethod]
        public void TestConfigurationWithLogManager()
        {
            var manager = new LogManager();

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

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.IsTrue(writer.LogEntries.Count == 10);

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Assert.IsTrue(writer.LogEntries.Count == 20);
        }
    }
}
