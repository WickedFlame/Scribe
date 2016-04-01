using NUnit.Framework;
using System.Linq;

namespace Scribe.Test
{
    [TestFixture]
    public class LoggerConfigurationExtensionTests
    {
        [Test]
        public void Scribe_LoggerConfigurationExtension_AddTraceWriter()
        {
            var configuration = new LoggerConfiguration();
            configuration.AddTraceWriter();

            var factory = configuration.CreateLogger() as LoggerFactory;

            Assert.IsInstanceOf<TraceLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddTraceWriter_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddTraceWriter()
                .CreateLogger() as LoggerFactory;

            Assert.IsInstanceOf<TraceLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddTraceWriter_WithFormatString_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddTraceWriter(formatString: "[{LogTime:d}][{Message}]")
                .CreateLogger() as LoggerFactory;

            Assert.IsInstanceOf<TraceLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddFileWriter()
        {
            var configuration = new LoggerConfiguration();
            configuration.AddFileWriter(fileName: "logfile.log");

            var factory = configuration.CreateLogger() as LoggerFactory;

            Assert.IsInstanceOf<FileLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddFileWriter_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddFileWriter(fileName: "logfile.log")
                .CreateLogger() as LoggerFactory;

            Assert.IsInstanceOf<FileLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddFileWriter_WithFormatString_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddFileWriter(fileName: "logfile.log", formatString: "[{LogTime:d}][{Message}]")
                .CreateLogger() as LoggerFactory;

            Assert.IsInstanceOf<FileLogWriter>(factory.Manager.Writers.Single());
        }
    }
}
