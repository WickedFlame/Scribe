using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
        }

        [TestMethod]
        public void TestConfigurationWithLogManager()
        {
            var manager = new LogManager();

            Assert.IsTrue(manager.Listeners.Any());
            Assert.IsTrue(manager.Listeners.First().GetType() == typeof(TraceLogListener));

            Assert.IsTrue(manager.Writers.Any());
            Assert.IsTrue(manager.Writers.First().Value().GetType() == typeof(MockLogWriter));
        }
    }
}
