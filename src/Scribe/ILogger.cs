using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe
{
    public interface ILogger
    {
        void Write(string message, TraceType traceType = TraceType.Information, string category = null, DateTime? logtime = null);

        ///// <summary>
        ///// Aggregates most logging patterns to a single method.  This must be compatible with the Func representation in the OWIN environment.
        ///// </summary>
        ///// <param name="eventType"></param>
        ///// <param name="eventId"></param>
        ///// <param name="state"></param>
        ///// <param name="exception"></param>
        ///// <param name="formatter"></param>
        ///// <returns></returns>
        //void Write(TraceType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter);

        ///// <summary>
        ///// Checks if the given TraceEventType is enabled.
        ///// </summary>
        ///// <param name="eventType"></param>
        ///// <returns></returns>
        //bool IsEnabled(TraceType eventType);

        ///// <summary>
        ///// Begins a logical operation scope.
        ///// </summary>
        ///// <param name="state">The identifier for the scope.</param>
        ///// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
        //IDisposable BeginScope(object state);
    }
}
