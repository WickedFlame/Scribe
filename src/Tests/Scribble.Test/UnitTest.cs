using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Scribble.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var manager = new LogManager();
            manager.RegisterProvider(new LogTraceListener());

            Trace.Write("Test");
            Trace.TraceError("Error message");

            Assert.IsTrue(manager.LogEntries.First().Message == "Test");
        }
    }
}
