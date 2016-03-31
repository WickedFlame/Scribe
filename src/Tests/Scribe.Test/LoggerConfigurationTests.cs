using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Scribe.Test
{
    [TestFixture]
    public class LoggerConfigurationTests
    {
        [Test]
        public void Scribe_LoggerConfiguration_CreateFactory()
        {
            var configuration = new LoggerConfiguration();

            var factory = configuration.CreateLogger();

            Assert.IsInstanceOf<LoggerFactory>(factory);
        }

        [Test]
        public void Scribe_LoggerConfiguration_CreateFactory_Fluent()
        {
            var factory = new LoggerConfiguration()
                .CreateLogger();

            Assert.IsInstanceOf<LoggerFactory>(factory);
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter()
        {
            var writer = new TraceLogWriter();

            var configuration = new LoggerConfiguration();
            configuration.AddWriter(writer);

            var factory = configuration.CreateLogger() as LoggerFactory;

            Assert.AreSame(writer, factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter_Fluent()
        {
            var writer = new TraceLogWriter();

            var factory = new LoggerConfiguration()
                .AddWriter(writer)
                .CreateLogger() as LoggerFactory;

            Assert.AreSame(writer, factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter_Multiple_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddWriter(new TraceLogWriter())
                .AddWriter(new TraceLogWriter())
                .CreateLogger() as LoggerFactory;

            Assert.That(factory.Manager.Writers.All(w => w is TraceLogWriter));
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddListener()
        {
            var listener = new TraceLogListener();

            var configuration = new LoggerConfiguration();
            configuration.AddListener(listener);

            var factory = configuration.CreateLogger() as LoggerFactory;

            Assert.AreSame(listener, factory.Manager.Listeners.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddListener_Fluent()
        {
            var listener = new TraceLogListener();

            var factory = new LoggerConfiguration()
                .AddListener(listener)
                .CreateLogger() as LoggerFactory;

            Assert.AreSame(listener, factory.Manager.Listeners.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddListener_Multiple_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddListener(new TraceLogListener())
                .AddListener(new TraceLogListener())
                .CreateLogger() as LoggerFactory;

            Assert.That(factory.Manager.Listeners.All(l => l is TraceLogListener));
        }
    }
}
