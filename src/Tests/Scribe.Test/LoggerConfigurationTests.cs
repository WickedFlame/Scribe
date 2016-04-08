using NUnit.Framework;
using System.Linq;

namespace Scribe.Test
{
    [TestFixture]
    public class LoggerConfigurationTests
    {
        [Test]
        public void Scribe_LoggerConfiguration_CreateFactory()
        {
            var configuration = new LoggerConfiguration();

            var factory = configuration.BuildFactory();

            Assert.IsInstanceOf<LoggerFactory>(factory);
        }

        [Test]
        public void Scribe_LoggerConfiguration_CreateFactory_Fluent()
        {
            var factory = new LoggerConfiguration()
                .BuildFactory();

            Assert.IsInstanceOf<LoggerFactory>(factory);
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter()
        {
            var writer = new TraceLogWriter();

            var configuration = new LoggerConfiguration();
            configuration.AddWriter(writer);

            var factory = configuration.BuildFactory() as LoggerFactory;

            Assert.AreSame(writer, factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter_Fluent()
        {
            var writer = new TraceLogWriter();

            var factory = new LoggerConfiguration()
                .AddWriter(writer)
                .BuildFactory() as LoggerFactory;

            Assert.AreSame(writer, factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter_Multiple_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddWriter(new TraceLogWriter())
                .AddWriter(new TraceLogWriter())
                .BuildFactory() as LoggerFactory;

            Assert.That(factory.Manager.Writers.All(w => w is TraceLogWriter));
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddListener()
        {
            var listener = new TraceListener();

            var configuration = new LoggerConfiguration();
            configuration.AddListener(listener);

            var factory = configuration.BuildFactory() as LoggerFactory;

            Assert.AreSame(listener, factory.Manager.Listeners.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddListener_Fluent()
        {
            var listener = new TraceListener();

            var factory = new LoggerConfiguration()
                .AddListener(listener)
                .BuildFactory() as LoggerFactory;

            Assert.AreSame(listener, factory.Manager.Listeners.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddListener_Multiple_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddListener(new TraceListener())
                .AddListener(new TraceListener())
                .BuildFactory() as LoggerFactory;

            Assert.That(factory.Manager.Listeners.All(l => l is TraceListener));
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetProcessor_Fluent()
        {
            var factory = new LoggerConfiguration()
                .SetProcessor(new LogProcessor())
                .BuildFactory() as LoggerFactory;

            Assert.IsInstanceOf<LogProcessor>(factory.Manager.Processor);
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetMinimalLogLevel_Fluent()
        {
            var factory = new LoggerConfiguration()
                .SetMinimalLogLevel(LogLevel.Critical)
                .BuildFactory() as LoggerFactory;

            Assert.That(factory.Manager.Processor.MinimalLogLevel == LogLevel.Critical);
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetProcessor_SetMinimalLogLevel_Higher_Fluent()
        {
            var processor = new LogProcessor();
            processor.MinimalLogLevel = LogLevel.Critical;

            var factory = new LoggerConfiguration()
                .SetProcessor(processor)
                .SetMinimalLogLevel(LogLevel.Information)
                .BuildFactory() as LoggerFactory;

            Assert.That(factory.Manager.Processor.MinimalLogLevel == LogLevel.Information);
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetMinimalLogLevel_SetProcessor_Lower_Fluent()
        {
            var processor = new LogProcessor();
            processor.MinimalLogLevel = LogLevel.Critical;

            var factory = new LoggerConfiguration()
                .SetMinimalLogLevel(LogLevel.Information)
                .SetProcessor(processor)
                .BuildFactory() as LoggerFactory;

            Assert.That(factory.Manager.Processor.MinimalLogLevel == LogLevel.Information);
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetMinimalLogLevel_SetProcessor_Higher_Fluent()
        {
            var processor = new LogProcessor();
            processor.MinimalLogLevel = LogLevel.Information;

            var factory = new LoggerConfiguration()
                .SetMinimalLogLevel(LogLevel.Critical)
                .SetProcessor(processor)
                .BuildFactory() as LoggerFactory;

            Assert.That(factory.Manager.Processor.MinimalLogLevel == LogLevel.Critical);
        }
    }
}
