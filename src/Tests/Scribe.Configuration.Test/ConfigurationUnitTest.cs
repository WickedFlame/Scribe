using System;
using System.Linq;
using System.Diagnostics;
using NUnit.Framework;
using Scribe;

namespace Scribe.Configuration.Test
{
    [TestFixture]
    public class ConfigurationUnitTest
    {
        [TearDown]
        public void TeadDown()
        {
            Trace.Listeners.Clear();
        }

        [Test]
        public void TestConfigurationWithLoggerFactory()
        {
            var factory = new LoggerFactory();
            factory.SetProcessor(new LogProcessor());
            factory.Manager.Initialize();

            Assert.IsTrue(factory.Manager.Writers.Any());
            Assert.IsTrue(factory.Manager.Writers.First().GetType() == typeof(MockLogWriter));

            var writer = factory.Manager.Writers.First() as MockLogWriter;
            writer.LogEntries.Clear();

            factory.GetLogger().SetTraceListener();

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            Assert.IsTrue(writer.LogEntries.Count == 10);

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            Assert.IsTrue(writer.LogEntries.Count == 20);
        }

        [Test]
        public void TestConfigurationWithLogManager()
        {
            var manager = new LogManager();
            manager.Initialize();

            Assert.IsTrue(manager.Writers.Any());
            Assert.IsTrue(manager.Writers.First().GetType() == typeof(MockLogWriter));

            var config = new LoggerConfiguration()
                .SetLogManager(manager)
                .SetProcessor(new LogProcessor());
            config.BuildLogger()
                .SetTraceListener();

            var writer = manager.Writers.First() as MockLogWriter;
            writer.LogEntries.Clear();

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            Assert.IsTrue(writer.LogEntries.Count == 10);

            for (int i = 1; i <= 10; i++)
            {
                Trace.WriteLine("Test " + i);
            }

            Assert.IsTrue(writer.LogEntries.Count == 20);
        }
    }
}
