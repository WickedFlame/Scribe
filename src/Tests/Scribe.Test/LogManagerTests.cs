﻿using NUnit.Framework;
using System.Linq;
using Scribe.Processing;

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
            manager.AddWriter(new TraceLogWriter());
            manager.AddWriter(new TraceLogWriter());

            Assert.That(manager.Writers.OfType<TraceLogWriter>().Count() == 1);
        }

        [Test]
        public void Scribe_Logger_DefaultProcessor()
        {
            var logger = new Logger();

            Assert.That(logger.Processor.GetType() == typeof(Processing.LogProcessor));
        }

        [Test]
        public void Scribe_LogManager_SetProcessor()
        {
            var logger = new Logger();
            logger.SetProcessor(new BasicLogProcessor());

            Assert.That(logger.Processor.GetType() == typeof(BasicLogProcessor));
        }

        [Test]
        public void Scribe_LogManager_SetProcessor_CheckManager()
        {
            var processor = new BasicLogProcessor();
            var manager = new LogManager();

            var logger = new Logger(manager);

            logger.SetProcessor(processor);

            Assert.That(processor.Manager, Is.SameAs(manager));
        }
    }
}
