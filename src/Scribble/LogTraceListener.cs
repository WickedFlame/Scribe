using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribble
{
    public class LogTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            LoggerFacade.Instance.Log(message);
        }

        public override void WriteLine(string message)
        {
            LoggerFacade.Instance.Log(message);
        }
    }
}
