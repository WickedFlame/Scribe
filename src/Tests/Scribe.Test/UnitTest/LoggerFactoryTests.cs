using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe.Test.UnitTest
{
    [TestFixture]
    public class LoggerFactoryTests
    {
        [Test]
        public void LoggerFactory_CreateWithDefaultManager()
        {
            var factory = new LoggerFactory();

            Assert.IsNotNull(factory.Manager);
        }

        [Test]
        [Ignore("Not implemented yet")]
        public void LoggerFactory_CreateWithConfiguration()
        {
            Assert.Fail();
        }

        [Test]
        public void LoggerFactory_CreateWithManager()
        {
            var manager = new LogManager();
            var factory = new LoggerFactory(manager);

            Assert.AreSame(manager, factory.Manager);
        }

        [Test]
        public void LoggerFactory_StaticLoggerCallback()
        {
            var logger = new Logger(new LoggerFactory());
            LoggerFactory.LoggerCallback = () => logger;

            var factory = new LoggerFactory();

            var result = factory.GetLogger();

            Assert.AreSame(logger, result);

            LoggerFactory.LoggerCallback = null;
        }

        [Test]
        public void Scribe_LoggerFactory_GetLogger()
        {
            var factory = new LoggerFactory();

            var first = factory.GetLogger();

            Assert.That(first, Is.Not.Null);
        }

        [Test]
        public void Scribe_LoggerFactory_GetLogger_Multiple_Loggers()
        {
            var factory = new LoggerFactory();

            var first = factory.GetLogger();
            var second = factory.GetLogger();

            Assert.AreNotSame(first, second);
        }

        [Test]
        public void LoggerFactory_GetProcessor()
        {
            var factory = new LoggerFactory();

            var processor = factory.GetProcessor();

            Assert.AreSame(processor, factory.Manager.Processor);
        }

        [Test]
        public void LoggerFactory_SetProcessor()
        {
            var factory = new LoggerFactory();
            var initial = factory.Manager.Processor;

            factory.SetProcessor(new LogProcessor(factory.Manager));

            Assert.AreNotSame(initial, factory.Manager.Processor);
        }

        [Test]
        public void LoggerFactory_AddListener()
        {
            var factory = new LoggerFactory();

            factory.AddListener(new TraceLogListener());

            Assert.IsTrue(factory.Manager.Listeners.Count() == 1);
        }

        [Test]
        public void LoggerFactory_AddWriter()
        {
            var factory = new LoggerFactory();

            factory.AddWriter(new TraceLogWriter());

            Assert.IsTrue(factory.Manager.Writers.Count() == 1);
        }
    }
}
