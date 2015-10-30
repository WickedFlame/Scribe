using System;
using System.Diagnostics;

namespace Scribe.Test
{
    class TraceLogWriter : ILogWriter
    {
        //public void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null)
        //{
        //    Trace.WriteLine(message);
        //}


        //public void Write<T>(T messageObject, LogLevel traceType = LogLevel.Information, string category = null, DateTime? logtime = null, Func<T, string> formatter = null)
        //{
        //    var message = formatter != null ? formatter(messageObject) : messageObject.ToString();
        //    Trace.WriteLine(message);
        //}
        public void Write(ILogEntry logEntry)
        {
            Trace.WriteLine(logEntry.ToString());
        }
    }
}
