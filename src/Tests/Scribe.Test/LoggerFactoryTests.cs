using NUnit.Framework;
using System.Diagnostics;

namespace Scribe.Test
{
    [TestFixture]
    public class LoggerFactoryTests
    {
        [Test]
        public void Scribe_LoggerFactory_CustomLogger()
        {
            var customLogger = new CustomLogger();
            LoggerFactory.CustomLogger = () => customLogger;
            var factory = new LoggerFactory();

            var logger = factory.GetLogger();

            Assert.AreSame(customLogger, logger);

            // revert logger to default
            LoggerFactory.CustomLogger = null;
        }

        private class CustomLogger : ILogger
        {
            public void AddWriter(ILogWriter writer)
            {
                // do nothing
            }

            public void Write(ILogEntry logEntry)
            {
                // just mock the ILogger...
            }
        }
    }
}
