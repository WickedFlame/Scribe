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
    }
}
