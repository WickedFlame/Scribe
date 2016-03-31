using NUnit.Framework;

namespace Scribe.Test
{
    [TestFixture]
    public class LogEntryFormatProviderTests
    {
        [Test]
        public void Scribe_LogEntryFormatProvider()
        {
            var provider = new LogEntryFormatProvider(formatString: "[{Category}] - [{Message}]");
            var formated = provider.Format(new LogEntry(message: "The Message", category: "Test Category"));

            Assert.AreEqual("[Test Category] - [The Message]", formated);
        }

        [Test]
        public void Scribe_LogEntryFormatProvider_SubFormat()
        {
            // format the datetime to only "dd" format
            var provider = new LogEntryFormatProvider(formatString: "[{Category}] - [{LogTime:dd}]");
            var formated = provider.Format(new LogEntry(message: "The Message", category: "Test Category", logtime: new System.DateTime(2012, 12, 21)));

            Assert.AreEqual("[Test Category] - [21]", formated);
        }

        [Test]
        public void Scribe_LogEntryFormatProvider_AllProperties()
        {
            // format the datetime to only "dd" format
            var provider = new LogEntryFormatProvider(formatString: "[{LogLevel}][{Priority}][{Category}][{Message}][{LogTime:dd}]");
            var formated = provider.Format(new LogEntry(message: "The Message", logLevel: LogLevel.Warning, priority: Priority.High, category: "Test Category", logtime: new System.DateTime(2012, 12, 21)));

            Assert.AreEqual("[Warning][High][Test Category][The Message][21]", formated);
        }

        [Test]
        public void Scribe_LogEntryFormatProvider_CaseInvariant()
        {
            // format the datetime to only "dd" format
            var provider = new LogEntryFormatProvider(formatString: "[{loglevel}][{priority}][{category}][{message}][{logtime:dd}]");
            var formated = provider.Format(new LogEntry(message: "The Message", logLevel: LogLevel.Warning, priority: Priority.High, category: "Test Category", logtime: new System.DateTime(2012, 12, 21)));

            Assert.AreEqual("[{loglevel}][{priority}][{category}][{message}][{logtime:dd}]", formated);
        }
    }
}
