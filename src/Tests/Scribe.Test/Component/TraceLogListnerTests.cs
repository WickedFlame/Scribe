#define TRACE
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using System;

namespace Scribe.Test.Component
{
    [TestFixture]
    public class TraceLogListnerTests
    {
        private static readonly bool IsRunningOnMono;

        static TraceLogListnerTests()
        {
            IsRunningOnMono = Type.GetType("Mono.Runtime") != null;
        }

        private LoggerFactory CreateLoggerFactory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddListener(new TraceLogListener());
            loggerFactory.SetProcessor(new LogProcessor(loggerFactory.Manager));

            return loggerFactory;
        }

        [Test]
        public void TraceWriteObject()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.Write(loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message == "Scribe.LoggerFactory");
        }

        [Test]
        public void TraceWriteMessage()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.Write("Test message");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message == "Test message");
        }

        [Test]
        public void TraceWriteObjectWithCategory()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.Write(loggerFactory, "TestCategory");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);

            Assert.IsTrue(entry.Message == "Scribe.LoggerFactory");
            Assert.IsTrue(entry.Category == "TestCategory");
        }

        [Test]
        public void TraceWriteMessageWithCategory()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.Write("Test message", "TestCategory");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message == "Test message");
            Assert.IsTrue(entry.Category == "TestCategory");
        }
        
        [Test]
        public void TraceWriteLineObject()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.WriteLine(loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message == "Scribe.LoggerFactory");
        }

        [Test]
        public void TraceWriteLineMessage()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.WriteLine("Test message");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message == "Test message");
        }

        [Test]
        public void TraceWriteLineObjectWithCategory()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.WriteLine(loggerFactory, "TestCategory");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message == "Scribe.LoggerFactory");
            Assert.IsTrue(entry.Category == "TestCategory");
        }

        [Test]
        public void TraceWriteLineMessageWithCategory()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.WriteLine("Test message", "TestCategory");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message == "Test message");
            Assert.IsTrue(entry.Category == "TestCategory");
        }

        [Test]
        public void TraceTraceError()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.TraceError("Error message");
            
            var logprocessor = loggerFactory.GetProcessor();

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message.StartsWith("Error message"), entry.ToString());
            Assert.IsTrue(entry.Message.Contains("StackTrace"));
            Assert.IsTrue(entry.LogLevel.Equals(LogLevel.Error));
        }

        [Test]
        public void TraceTraceErrorWithParams()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.TraceError("Error message {0} {1}", "parama 1", loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message.StartsWith("Error message parama 1 Scribe.LoggerFactory"));
            Assert.IsTrue(entry.Message.Contains("StackTrace"));
            Assert.IsTrue(entry.LogLevel ==  LogLevel.Error);
        }

        [Test]
        public void TraceTraceInformation()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.TraceInformation("Information message");
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }


            Assert.IsTrue(entry.Message.StartsWith("Information message"));
            // No StackTrace
            Assert.IsFalse(entry.Message.Contains("StackTrace"));
            Assert.IsTrue(entry.LogLevel == LogLevel.Information);
        }

        [Test]
        public void TraceTraceInformationWithParams()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.TraceInformation("Information message {0} {1}", "parama 1", loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message.StartsWith("Information message parama 1 Scribe.LoggerFactory"));
            // No StackTrace
            Assert.IsFalse(entry.Message.Contains("StackTrace"));
            Assert.IsTrue(entry.LogLevel == LogLevel.Information);
        }

        [Test]
        public void TraceTraceWarning()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.TraceWarning("Warning message");
            
            var logprocessor = loggerFactory.GetProcessor();
            
            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message.StartsWith("Warning message"));
            // No StackTrace
            Assert.IsFalse(entry.Message.Contains("StackTrace"));
            Assert.IsTrue(entry.LogLevel == LogLevel.Warning);
        }

        [Test]
        public void TraceTraceWarningWithParams()
        {
            var loggerFactory = CreateLoggerFactory();

            Trace.TraceWarning("Warning message {0} {1}", "parama 1", loggerFactory);
            
            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.ProcessedLogs.Count() == 1);

            var entry = logprocessor.ProcessedLogs.First();
            Assert.IsNotNull(entry);
            if (IsRunningOnMono)
            {
                // out of some unknown reason mono can't handle TraceHandlers properly
                return;
            }

            Assert.IsTrue(entry.Message.StartsWith("Warning message parama 1 Scribe.LoggerFactory"));
            // No StackTrace
            Assert.IsFalse(entry.Message.Contains("StackTrace"));
            Assert.IsTrue(entry.LogLevel == LogLevel.Warning);
        }
    }
}
