using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Scribe.Test.Component
{
    [TestFixture]
    public class TraceLogListnerTests
    {
        private LoggerFactory CreateLoggerFactor()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddListener(new TraceListener());
            loggerFactory.SetProcessor(new LogProcessor());
            
            return loggerFactory;
        }

        [Test]
        public void TraceWriteObject()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.Write(loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
        }

        [Test]
        public void TraceWriteMessage()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.Write("Test message");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
        }

        [Test]
        public void TraceWriteObjectWithCategory()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.Write(loggerFactory, "TestCategory");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
            Assert.IsTrue(logprocessor.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceWriteMessageWithCategory()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.Write("Test message", "TestCategory");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
            Assert.IsTrue(logprocessor.LogEntries.First().Category == "TestCategory");
        }
        
        [Test]
        public void TraceWriteLineObject()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.WriteLine(loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
        }

        [Test]
        public void TraceWriteLineMessage()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.WriteLine("Test message");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
        }

        [Test]
        public void TraceWriteLineObjectWithCategory()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.WriteLine(loggerFactory, "TestCategory");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
            Assert.IsTrue(logprocessor.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceWriteLineMessageWithCategory()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.WriteLine("Test message", "TestCategory");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
            Assert.IsTrue(logprocessor.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceTraceError()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.TraceError("Error message");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Error message"));
            Assert.IsTrue(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel == LogLevel.Error);
        }

        [Test]
        public void TraceTraceErrorWithParams()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.TraceError("Error message {0} {1}", "parama 1", loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Error message parama 1 Scribe.LoggerFactory"));
            Assert.IsTrue(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel ==  LogLevel.Error);
        }

        [Test]
        public void TraceTraceInformation()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.TraceInformation("Information message");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Information message"));
            // No StackTrace
            Assert.IsFalse(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel == LogLevel.Information);
        }

        [Test]
        public void TraceTraceInformationWithParams()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.TraceInformation("Information message {0} {1}", "parama 1", loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Information message parama 1 Scribe.LoggerFactory"));
            // No StackTrace
            Assert.IsFalse(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel == LogLevel.Information);
        }

        [Test]
        public void TraceTraceWarning()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.TraceWarning("Warning message");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Warning message"));
            // No StackTrace
            Assert.IsFalse(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel == LogLevel.Warning);
        }

        [Test]
        public void TraceTraceWarningWithParams()
        {
            var loggerFactory = CreateLoggerFactor();

            Trace.TraceWarning("Warning message {0} {1}", "parama 1", loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Warning message parama 1 Scribe.LoggerFactory"));
            // No StackTrace
            Assert.IsFalse(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel == LogLevel.Warning);
        }
    }
}
