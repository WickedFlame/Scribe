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


        public void Write<T>(T messageObject, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null, Func<T, string> formatter = null)
        {
            var message = formatter != null ? formatter(messageObject) : messageObject.ToString();
            Trace.WriteLine(message);
        }
    }
}
