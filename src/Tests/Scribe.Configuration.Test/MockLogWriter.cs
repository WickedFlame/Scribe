﻿using System;
using System.Collections.Generic;

namespace Scribe.Configuration.Test
{
    public class MockLogWriter : ILogWriter
    {

        List<LogEntry> _logEntries;
        public List<LogEntry> LogEntries
        {
            get
            {
                if (_logEntries == null)
                    _logEntries = new List<LogEntry>();
                return _logEntries;
            }
        }
      
		
        public void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null)
        {
            LogEntries.Add(new LogEntry(message, traceType, category, logtime));
        }
    }
}