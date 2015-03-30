using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scribe.Test.Component
{
    [TestClass]
    public class TraceLogListnerTests
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void TraceWriteObjectWithCategory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.Write(loggerFactory, "TestCategory");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
            Assert.IsTrue(logprocessor.LogEntries.First().Categroy == "TestCategory");
        }

        [TestMethod]
        public void TraceWriteMessageWithCategory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.Write("Test message", "TestCategory");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
            Assert.IsTrue(logprocessor.LogEntries.First().Categroy == "TestCategory");
        }
        
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void TraceWriteLineObjectWithCategory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.WriteLine(loggerFactory, "TestCategory");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Scribe.LoggerFactory");
            Assert.IsTrue(logprocessor.LogEntries.First().Categroy == "TestCategory");
        }

        [TestMethod]
        public void TraceWriteLineMessageWithCategory()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.Manager.AddListener(new TraceLogListener());

            Trace.WriteLine("Test message", "TestCategory");

            Thread.Sleep(500);

            var logprocessor = loggerFactory.GetProcessor();

            Assert.IsTrue(logprocessor.LogEntries.Count() == 1);
            Assert.IsTrue(logprocessor.LogEntries.First().Message == "Test message");
            Assert.IsTrue(logprocessor.LogEntries.First().Categroy == "TestCategory");
        }

        [TestMethod]
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
            Assert.IsTrue(logprocessor.LogEntries.First().TraceType == TraceType.Error);
        }

        [TestMethod]
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
            Assert.IsTrue(logprocessor.LogEntries.First().TraceType ==  TraceType.Error);
        }

        [TestMethod]
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
            Assert.IsTrue(logprocessor.LogEntries.First().TraceType == TraceType.Information);
        }

        [TestMethod]
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
            Assert.IsTrue(logprocessor.LogEntries.First().TraceType == TraceType.Information);
        }

        [TestMethod]
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
            Assert.IsTrue(logprocessor.LogEntries.First().TraceType == TraceType.Warning);
        }

        [TestMethod]
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
            Assert.IsTrue(logprocessor.LogEntries.First().TraceType == TraceType.Warning);
        }

        //[TestMethod]
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

        //[TestMethod]
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

        //[TestMethod]
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
