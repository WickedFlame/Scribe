using NUnit.Framework;
using System.Linq;

namespace Scribe.Test.UnitTest
{
    [TestFixture]
    public class LogManagerTests
    {
        [Test]
        public void LogManager_Create_CheckLoggerFactory()
        {
            var manager = new LogManager();

            Assert.IsNotNull(manager.LoggerFactory);
        }

        [Test]
        public void LogManager_Create_CheckProcessor()
        {
            var manager = new LogManager();

            Assert.IsNotNull(manager.Processor);
        }

        [Test]
        public void LogManager_CreateWithFactory_CheckLoggerFactory()
        {
            var factory = new LoggerFactory();
            var manager = new LogManager(factory);

            Assert.AreSame(factory, manager.LoggerFactory);
        }

        [Test]
        public void LogManager_CreateWithFactory_CheckProcessor()
        {
            var factory = new LoggerFactory();
            var manager = new LogManager(factory);

            Assert.IsNotNull(manager.Processor);
        }

        [Test]
        public void LogManager_SetProcessor()
        {
            var manager = new LogManager();
            var processor = new LogProcessor(manager);

            manager.SetProcessor(processor);

            Assert.AreSame(processor, manager.Processor);
        }

        [Test]
        public void LogManager_AddWriter()
        {
            var manager = new LogManager();
            manager.AddWriter(new TraceLogWriter());

            Assert.IsTrue(manager.Writers.Any());
        }

        [Test]
        public void LogManager_AddListener()
        {
            var manager = new LogManager();
            manager.AddListener(new TraceLogListener());

            Assert.IsTrue(manager.Listeners.Any());
        }
    }
}
