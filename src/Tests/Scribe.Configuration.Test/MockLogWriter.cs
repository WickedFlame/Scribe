using System;
using System.Collections.Generic;

namespace Scribe.Configuration.Test
{
    public class MockLogWriter : ILogWriter
    {

        List<ILogEntry> _logEntries;
        public List<ILogEntry> LogEntries
        {
            get
            {
                if (_logEntries == null)
                {
                    _logEntries = new List<ILogEntry>();
                }

                return _logEntries;
            }
        }
      
		
        //public void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null)
        //{
        //    LogEntries.Add(new LogEntry(message, traceType, category, logtime));
        //}


        //public void Write<T>(T message, LogLevel traceType = LogLevel.Information, string category = null, DateTime? logtime = null, Func<T, string> formatter = null)
        //{
        //    var msg = formatter != null ? formatter(message) : message.ToString();
        //    LogEntries.Add(new LogEntry(msg, traceType, category, logtime));
        //}

        public void Write(ILogEntry logEntry)
        {
            LogEntries.Add(logEntry);
        }
    }
}
