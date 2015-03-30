using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe.Test
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void LoggExceptionWithFormatter()
        {
            var exception1 = new Exception("Exception 1");
            var exception2 = new Exception("Exception 2", exception1);

            var manager = new LogManager();
            var logger = manager.LoggerFactory.GetLogger();
            logger.Write(exception2, formatter: e =>
            {
                var sb = new StringBuilder();
                var ex = e;
                while (ex != null)
                {
                    sb.AppendLine(ex.Message);
                    ex = ex.InnerException;
                }

                return sb.ToString();
            });

            // give the logthread time to write logqueue
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            var processor = manager.LoggerFactory.GetProcessor();
            Assert.IsTrue(processor.LogEntries.First().Message == "Exception 2\r\nException 1\r\n");
        }
    }
}
