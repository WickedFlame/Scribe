using NUnit.Framework;
using System.Linq;

namespace Scribe.Test
{
    [TestFixture]
    public class LogManagerTests
    {
        [Test]
        public void Scribe_LogManager_CheckManagerInFactory()
        {
            var manager = new LogManager();
            var factory = new LoggerFactory(manager);

            Assert.That(factory.Manager, Is.SameAs(manager));
        }

        [Test]
        public void Scribe_LogManager_AddWriter()
        {
            var manager = new LogManager();
            manager.AddWriter(new TraceLogWriter());

            Assert.That(manager.Writers.OfType<TraceLogWriter>().Any());
        }

        [Test]
        public void Scribe_LogManager_AddWriter_MultipleSameWriters()
        {
            var manager = new LogManager();
            manager.SetProcessor(new LogProcessor());
            manager.AddWriter(new TraceLogWriter());
            manager.AddWriter(new TraceLogWriter());

            Assert.That(manager.Writers.OfType<TraceLogWriter>().Count() == 1);
        }

        [Test]
        public void Scribe_LogManager_AddListener()
        {
            var manager = new LogManager();
            manager.AddListener(new TraceListener());

            Assert.That(manager.Listeners.OfType<TraceListener>().Any());
        }

        [Test]
        public void Scribe_LogManager_AddListener_MultipleSameListeners()
        {
            var manager = new LogManager();
            manager.SetProcessor(new LogProcessor());
            manager.AddListener(new TraceListener());
            manager.AddListener(new TraceListener());

            Assert.That(manager.Listeners.OfType<TraceListener>().Count() == 1);
        }

        [Test]
        public void Scribe_LogManager_DefaultProcessor()
        {
            var manager = new LogManager();

            Assert.That(manager.Processor.GetType() == typeof(AsyncLogProcessor));
        }

        [Test]
        public void Scribe_LogManager_SetProcessor()
        {
            var manager = new LogManager();
            manager.SetProcessor(new LogProcessor());

            Assert.That(manager.Processor.GetType() == typeof(LogProcessor));
        }

        [Test]
        public void Scribe_LogManager_SetProcessor_CheckManager()
        {
            var processor = new LogProcessor();
            var manager = new LogManager();
            manager.SetProcessor(processor);

            Assert.That(processor.Manager, Is.SameAs(manager));
        }
    }
}
