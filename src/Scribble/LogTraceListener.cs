using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribble
{
    public class LogTraceListener : TraceListener, ILogProvider
    {
        internal Logger Logger { get; private set; }

        public void Initialize(Logger logger)
        {
            Logger = logger;

            Trace.Listeners.Add(this);
        }

        public override void Write(string message)
        {
            if (Logger == null)
                return;

            Logger.Log(message);
        }

        public override void WriteLine(string message)
        {
            if (Logger == null)
                return;

            Logger.Log(message);
        }
    }
}
