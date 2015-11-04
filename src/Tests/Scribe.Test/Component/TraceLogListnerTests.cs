using NUnit.Framework;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Scribe.Test.Component
{
    [TestFixture]
    public class TraceLogListnerTests
    {
        [Test]
        public void TraceWriteObject()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.Write(loggerFactory);

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
        }

        [Test]
        public void TraceWriteMessage()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.Write("Test message");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
        }

        [Test]
        public void TraceWriteObjectWithCategory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.Write(loggerFactory, "TestCategory");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
            Assert.IsTrue(logprocessor.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceWriteMessageWithCategory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.Write("Test message", "TestCategory");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
            Assert.IsTrue(logprocessor.LogEntries.First().Category == "TestCategory");
        }
        
        [Test]
        public void TraceWriteLineObject()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.WriteLine(loggerFactory);

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
        }

        [Test]
        public void TraceWriteLineMessage()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.WriteLine("Test message");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
        }

        [Test]
        public void TraceWriteLineObjectWithCategory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.WriteLine(loggerFactory, "TestCategory");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
            Assert.IsTrue(logprocessor.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceWriteLineMessageWithCategory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.WriteLine("Test message", "TestCategory");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
            Assert.IsTrue(logprocessor.LogEntries.First().Category == "TestCategory");
        }

        [Test]
        public void TraceTraceError()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.TraceError("Error message");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Error message"));
            Assert.IsTrue(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel == LogLevel.Error);
        }

        [Test]
        public void TraceTraceErrorWithParams()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.TraceError("Error message {0} {1}", "parama 1", loggerFactory);

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Error message parama 1 Scribe.LoggerFactory"));
            Assert.IsTrue(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel ==  LogLevel.Error);
        }

        [Test]
        public void TraceTraceInformation()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.TraceInformation("Information message");

            Thread.Sleep(500);

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
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.TraceInformation("Information message {0} {1}", "parama 1", loggerFactory);

            Thread.Sleep(500);

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
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.TraceWarning("Warning message");

            Thread.Sleep(500);

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
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.TraceWarning("Warning message {0} {1}", "parama 1", loggerFactory);
            
            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Warning message parama 1 Scribe.LoggerFactory"));
            // No StackTrace
            Assert.IsFalse(logprocessor.LogEntries.First().Message.Contains("StackTrace"));
            Assert.IsTrue(logprocessor.LogEntries.First().LogLevel == LogLevel.Warning);
        }

        //[Test]
        //public void TraceFail()
        //{
        //    var loggerFactory = new LoggerFactory();
        //    loggerFactory.Manager.AddListener(new TraceLogListener());

        //    Trace.Fail("Fail");

        //    Thread.Sleep(500);

        //    var logprocessor = loggerFactory.GetProcessor();

        //    Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
        //    Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Fail"));
        //    Assert.IsTrue(logprocessor.LogEntries.First().TraceType == TraceType.Critical);
        //}

        //[Test]
        //public void TraceFailWithDetail()
        //{
        //    var loggerFactory = new LoggerFactory();
        //    loggerFactory.Manager.AddListener(new TraceLogListener());

        //    Trace.Fail("Fail", "The complete message");

        //    Thread.Sleep(500);

        //    var logprocessor = loggerFactory.GetProcessor();

        //    Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
        //    Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Fail"));
        //    Assert.IsTrue(logprocessor.LogEntries.First().Message.Contains("Detail"));
        //    Assert.IsTrue(logprocessor.LogEntries.First().Message.Contains("The complete message"));
        //    Assert.IsTrue(logprocessor.LogEntries.First().TraceType == TraceType.Critical);
        //}

        //[Test]
        //public void TraceAssert()
        //{
        //    var loggerFactory = new LoggerFactory();
        //    loggerFactory.Manager.AddListener(new TraceLogListener());

        //    Trace.Assert(false,"Fail");

        //    Thread.Sleep(500);

        //    var logprocessor = loggerFactory.GetProcessor();

        //    Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
        //    Assert.IsTrue(logprocessor.LogEntries.First().Message.StartsWith("Fail"));
        //    Assert.IsTrue(logprocessor.LogEntries.First().TraceType == TraceType.Critical);
        //}
    }
}
