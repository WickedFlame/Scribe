using NUnit.Framework;
using System.Linq;
using Scribe.Processing;

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
        public void Scribe_LoggerConfiguration_BuildLogger()
        {
            var configuration = new LoggerConfiguration();

            var logger = configuration.BuildLogger();

            Assert.IsInstanceOf<Logger>(logger);
        }

        [Test]
        public void Scribe_LoggerConfiguration_BuildLogger_Fluent()
        {
            var logger = new LoggerConfiguration()
                .BuildLogger();

            Assert.IsInstanceOf<Logger>(logger);
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter()
        {
            var writer = new TraceLogWriter();

            var configuration = new LoggerConfiguration();
            configuration.AddWriter(writer);

            var logger = configuration.BuildLogger() as Logger;

            Assert.AreSame(writer, logger.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter_Fluent()
        {
            var writer = new TraceLogWriter();

            var logger = new LoggerConfiguration()
                .AddWriter(writer)
                .BuildLogger() as Logger;

            Assert.AreSame(writer, logger.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfiguration_AddWriter_Multiple_Fluent()
        {
            var logger = new LoggerConfiguration()
                .AddWriter(new TraceLogWriter())
                .AddWriter(new TraceLogWriter())
                .BuildLogger() as Logger;

            Assert.That(logger.Manager.Writers.All(w => w is TraceLogWriter));
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetProcessor_Fluent()
        {
            var logger = new LoggerConfiguration()
                .SetProcessor(new BasicLogProcessor())
                .BuildLogger() as Logger;

            Assert.IsInstanceOf<BasicLogProcessor>(logger.Processor);
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetMinimalLogLevel_Fluent()
        {
            var logger = new LoggerConfiguration()
                .SetMinimalLogLevel(LogLevel.Critical)
                .BuildLogger() as Logger;

            Assert.That(logger.Manager.MinimalLogLevel == LogLevel.Critical);
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetProcessor_SetMinimalLogLevel_Higher_Fluent()
        {
            var manager = new LogManager();
            manager.SetMinimalLogLevel(LogLevel.Critical);

            var logger = new LoggerConfiguration()
                .SetProcessor(new BasicLogProcessor(manager))
                .SetMinimalLogLevel(LogLevel.Information)
                .BuildLogger() as Logger;

            Assert.That(logger.Manager.MinimalLogLevel == LogLevel.Information);
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetMinimalLogLevel_SetProcessor_Lower_Fluent()
        {
            var manager = new LogManager();
            manager.SetMinimalLogLevel(LogLevel.Critical);

            var logger = new LoggerConfiguration()
                .SetMinimalLogLevel(LogLevel.Information)
                .SetProcessor(new BasicLogProcessor(manager))
                .BuildLogger() as Logger;

            Assert.That(logger.Manager.MinimalLogLevel == LogLevel.Information);
        }

        [Test]
        public void Scribe_LoggerConfiguration_SetMinimalLogLevel_SetProcessor_Higher_Fluent()
        {
            var manager = new LogManager();
            manager.SetMinimalLogLevel(LogLevel.Critical);

            var logger = new LoggerConfiguration()
                .SetMinimalLogLevel(LogLevel.Critical)
                .SetProcessor(new BasicLogProcessor(manager))
                .BuildLogger() as Logger;

            Assert.That(logger.Manager.MinimalLogLevel == LogLevel.Critical);
        }
    }
}
