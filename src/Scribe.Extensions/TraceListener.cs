﻿using System;
using System.Diagnostics;
using System.Text;

namespace Scribe
{
    public class TraceListener : System.Diagnostics.TraceListener
    {
        private ILogger _logger;

        public TraceListener(ILogger logger)
        {
            _logger = logger;
            Trace.Listeners.Add(this);
        }

        protected ILogger Logger => _logger;

        private void Log(string message, LogLevel loglevel = LogLevel.Information, string category = null)
        {
            if (Logger == null)
            {
                Trace.WriteLine($"Log listener {GetType().Name} is not initialized");
                return;
            }

            Logger.Write(message, loglevel, category: category);
        }

        public override void Write(string message)
        {
            Log(message);
        }

        public override void WriteLine(string message)
        {
            Log(message);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            var sb = new StringBuilder(100);
            sb.AppendLine(data.ToString());
            sb.AppendLine($"Id: {id}");
            sb.AppendLine($"Process: {eventCache.ProcessId}");
            sb.AppendLine($"Thread: {eventCache.ThreadId}");

            var traceType = Convert(eventType);
            switch (traceType)
            {
                case LogLevel.Error:
                case LogLevel.Critical:
                    sb.AppendLine();
                    sb.AppendLine("StackTrace:");
                    sb.AppendLine(eventCache.Callstack);
                    break;

                case LogLevel.Information:
                case LogLevel.Verbose:
                case LogLevel.Warning:
                    break;
            }

            Log(sb.ToString(), traceType); 
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            var sb = new StringBuilder(100);
            foreach (var arg in data)
            {
                sb.AppendLine(arg.ToString());
            }
            sb.AppendLine(string.Format("Id: {0}", id));
            sb.AppendLine(string.Format("Process: {0}", eventCache.ProcessId));
            sb.AppendLine(string.Format("Thread: {0}", eventCache.ThreadId));

            var traceType = Convert(eventType);
            switch (traceType)
            {
                case LogLevel.Error:
                case LogLevel.Critical:
                    sb.AppendLine();
                    sb.AppendLine("StackTrace:");
                    sb.AppendLine(eventCache.Callstack);
                    break;

                case LogLevel.Information:
                case LogLevel.Verbose:
                case LogLevel.Warning:
                    break;
            }

            Log(sb.ToString(), traceType); 
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            var sb = new StringBuilder(100);
            sb.AppendLine(string.Format("Id: {0}", id));
            sb.AppendLine(string.Format("Process: {0}", eventCache.ProcessId));
            sb.AppendLine(string.Format("Thread: {0}", eventCache.ThreadId));

            var traceType = Convert(eventType);
            switch (traceType)
            {
                case LogLevel.Error:
                case LogLevel.Critical:
                    sb.AppendLine();
                    sb.AppendLine("StackTrace:");
                    sb.AppendLine(eventCache.Callstack);
                    break;

                case LogLevel.Information:
                case LogLevel.Verbose:
                case LogLevel.Warning:
                    break;
            }

            Log(sb.ToString(), traceType); 
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            var sb = new StringBuilder(100);
            sb.AppendLine(string.Format(format, args));
            sb.AppendLine(string.Format("Id: {0}", id));
            sb.AppendLine(string.Format("Process: {0}", eventCache.ProcessId));
            sb.AppendLine(string.Format("Thread: {0}", eventCache.ThreadId));

            var traceType = Convert(eventType);
            switch (traceType)
            {
                case LogLevel.Error:
                case LogLevel.Critical:
                    sb.AppendLine();
                    sb.AppendLine("StackTrace:");
                    sb.AppendLine(eventCache.Callstack);
                    break;

                case LogLevel.Information:
                case LogLevel.Verbose:
                case LogLevel.Warning:
                    break;
            }

            Log(sb.ToString(), traceType); 
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            var sb = new StringBuilder(100);
            sb.AppendLine(message);
            sb.AppendLine(string.Format("Id: {0}", id));
            sb.AppendLine(string.Format("Process: {0}", eventCache.ProcessId));
            sb.AppendLine(string.Format("Thread: {0}", eventCache.ThreadId));

            var traceType = Convert(eventType);
            switch (traceType)
            {
                case LogLevel.Error:
                case LogLevel.Critical:
                    sb.AppendLine();
                    sb.AppendLine("StackTrace:");
                    sb.AppendLine(eventCache.Callstack);
                    break;

                case LogLevel.Information:
                case LogLevel.Verbose:
                case LogLevel.Warning:
                    break;
            }

            Log(sb.ToString(), traceType); 
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            var sb = new StringBuilder(100);
            sb.AppendLine(message);
            sb.AppendLine(string.Format("Related Activity: {0}", relatedActivityId));
            sb.AppendLine(string.Format("Id: {0}", id));
            sb.AppendLine(string.Format("Process: {0}", eventCache.ProcessId));
            sb.AppendLine(string.Format("Thread: {0}", eventCache.ThreadId));

            Log(sb.ToString()); 
        }

        public override void Write(object o)
        {
            if (o == null)
            {
                return;
            }

            Log(o.ToString());
        }

        public override void Write(object o, string category)
        {
            if (o == null)
            {
                return;
            }

            Log(o.ToString(), category: category);
        }

        public override void Write(string message, string category)
        {
            Log(message, category: category);
        }

        protected override void WriteIndent()
        {
            base.WriteIndent();
        }

        public override void WriteLine(object o)
        {
            if (o == null)
            {
                return;
            }

            Log(o.ToString());
        }

        public override void WriteLine(object o, string category)
        {
            if (o == null)
            {
                return;
            }

            Log(o.ToString(), category: category);
        }

        public override void WriteLine(string message, string category)
        {
            Log(message, category: category);
        }

        public override void Fail(string message)
        {
            Log(message, LogLevel.Critical);
        }

        public override void Fail(string message, string detailMessage)
        {
            var sb = new StringBuilder();
            sb.AppendLine(message);
            sb.AppendLine();
            sb.AppendLine("Detail:");
            sb.AppendLine(detailMessage);

            Log(sb.ToString(), LogLevel.Critical);
        }

        private static LogLevel Convert(TraceEventType traceEventType)
        {
            var traceType = LogLevel.Information;
            switch (traceEventType)
            {
                case TraceEventType.Critical:
                    traceType = LogLevel.Critical;
                    break;

                case TraceEventType.Error:
                    traceType = LogLevel.Error;
                    break;

                case TraceEventType.Verbose:
                    traceType = LogLevel.Verbose;
                    break;

                case TraceEventType.Warning:
                    traceType = LogLevel.Warning;
                    break;

                case TraceEventType.Information:
                    traceType = LogLevel.Information;
                    break;

                case TraceEventType.Resume:
                case TraceEventType.Start:
                case TraceEventType.Stop:
                case TraceEventType.Suspend:
                case TraceEventType.Transfer:
                default:
                    traceType = LogLevel.Information;
                    break;
            }

            return traceType;
        }
    }
}
