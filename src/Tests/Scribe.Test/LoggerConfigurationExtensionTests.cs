using NUnit.Framework;
using System;
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

            var factory = configuration.BuildFactory() as LoggerFactory;

            Assert.IsInstanceOf<TraceLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddTraceWriter_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddTraceWriter()
                .BuildFactory() as LoggerFactory;

            Assert.IsInstanceOf<TraceLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddTraceWriter_WithFormatString_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddTraceWriter(formatString: "[{LogTime:d}][{Message}]")
                .BuildFactory() as LoggerFactory;

            Assert.IsInstanceOf<TraceLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddFileWriter()
        {
            var configuration = new LoggerConfiguration();
            configuration.AddFileWriter(fileName: "logfile.log");

            var factory = configuration.BuildFactory() as LoggerFactory;

            Assert.IsInstanceOf<FileLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddFileWriter_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddFileWriter(fileName: "logfile.log")
                .BuildFactory() as LoggerFactory;

            Assert.IsInstanceOf<FileLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddFileWriter_WithFormatString_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddFileWriter(fileName: "logfile.log", formatString: "[{LogTime:d}][{Message}]")
                .BuildFactory() as LoggerFactory;

            Assert.IsInstanceOf<FileLogWriter>(factory.Manager.Writers.Single());
        }

        [Test]
        public void Scribe_LoggerConfigurationExtension_AddWriter_Delegate_Fluent()
        {
            var factory = new LoggerConfiguration()
                .AddWriter(l => System.Diagnostics.Trace.WriteLine(l.ToString()))
                .BuildFactory() as LoggerFactory;

            factory.GetLogger().Write("Test message");

            Assert.IsInstanceOf<DelegateLogWriter>(factory.Manager.Writers.Single());
        }
    }
}
