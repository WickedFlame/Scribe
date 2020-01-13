using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Scribe.Processing;

namespace Scribe.Test.Component
{
    [TestFixture]
    public class TraceLogListnerTests
    {
        private ILoggerFactory BuildLoggerFactoryWithTraceListener(ILogWriter writer)
        {
            var loggerFactory = new LoggerFactory()
                .AddWriter(writer)
                .SetProcessor(new BasicLogProcessor());

            var listener = new TraceListener(loggerFactory.GetLogger());

            return loggerFactory;
        }

        [Test]
        public void TraceWriteObject()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.Write(loggerFactory);
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message == "Scribe.LoggerFactory");
        }

        [Test]
        public void TraceWriteMessage()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.Write("Test message");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message == "Test message");
        }

        [Test]
        public void TraceWriteObjectWithCategory()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.Write(loggerFactory, "TestCategory");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message == "Scribe.LoggerFactory");
            Assert.IsTrue(writer.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceWriteMessageWithCategory()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.Write("Test message", "TestCategory");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message == "Test message");
            Assert.IsTrue(writer.LogEntries.First().Category == "TestCategory");
        }
        
        [Test]
        public void TraceWriteLineObject()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.WriteLine(loggerFactory);
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message == "Scribe.LoggerFactory");
        }

        [Test]
        public void TraceWriteLineMessage()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.WriteLine("Test message");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message == "Test message");
        }

        [Test]
        public void TraceWriteLineObjectWithCategory()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.WriteLine(loggerFactory, "TestCategory");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message == "Scribe.LoggerFactory");
            Assert.IsTrue(writer.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceWriteLineMessageWithCategory()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.WriteLine("Test message", "TestCategory");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message == "Test message");
            Assert.IsTrue(writer.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceTraceError()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.TraceError("Error message");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message.StartsWith("Error message"));
            Assert.IsTrue(writer.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(writer.LogEntries.First().LogLevel == LogLevel.Error);
        }

        [Test]
        public void TraceTraceErrorWithParams()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.TraceError("Error message {0} {1}", "parama 1", loggerFactory);
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message.StartsWith("Error message parama 1 Scribe.LoggerFactory"));
            Assert.IsTrue(writer.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(writer.LogEntries.First().LogLevel ==  LogLevel.Error);
        }

        [Test]
        public void TraceTraceInformation()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.TraceInformation("Information message");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message.StartsWith("Information message"));
            // No StackTrace
            Assert.IsFalse(writer.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(writer.LogEntries.First().LogLevel == LogLevel.Information);
        }

        [Test]
        public void TraceTraceInformationWithParams()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.TraceInformation("Information message {0} {1}", "parama 1", loggerFactory);
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message.StartsWith("Information message parama 1 Scribe.LoggerFactory"));
            // No StackTrace
            Assert.IsFalse(writer.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(writer.LogEntries.First().LogLevel == LogLevel.Information);
        }

        [Test]
        public void TraceTraceWarning()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.TraceWarning("Warning message");
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message.StartsWith("Warning message"));
            // No StackTrace
            Assert.IsFalse(writer.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(writer.LogEntries.First().LogLevel == LogLevel.Warning);
        }

        [Test]
        public void TraceTraceWarningWithParams()
        {
            var writer = new QueueLogWriter();
            var loggerFactory = BuildLoggerFactoryWithTraceListener(writer);

            Trace.TraceWarning("Warning message {0} {1}", "parama 1", loggerFactory);
            
            Assert.IsTrue(writer.LogEntries.Count() == 1);
            Assert.IsTrue(writer.LogEntries.First().Message.StartsWith("Warning message parama 1 Scribe.LoggerFactory"));
            // No StackTrace
            Assert.IsFalse(writer.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(writer.LogEntries.First().LogLevel == LogLevel.Warning);
        }
    }
}
