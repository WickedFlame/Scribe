using System;

namespace Scribe
{
    /// <summary>
    /// Interface that defines the basic functions for the logger
    /// </summary>
    public interface ILogger
    {
        ///// <summary>
        ///// Writes a log message
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="traceType"></param>
        ///// <param name="category"></param>
        ///// <param name="logtime"></param>
        //void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null);

        /// <summary>
        /// Writes a log message with the passed object. Calls ToString on the message object if no formatter is supplied
        /// </summary>
        /// <param name="message"></param>
        /// <param name="traceType"></param>
        /// <param name="category"></param>
        /// <param name="logtime"></param>
        /// <param name="formatter"></param>
        void Write<T>(T message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null, Func<T, string> formatter = null);
    }
}
