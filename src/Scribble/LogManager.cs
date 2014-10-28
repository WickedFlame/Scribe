using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribble
{
    public class LogManager
    {

        Logger _logger;
        internal Logger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = new Logger();
                return _logger;
            }
        }

        public IEnumerable<LogEntry> LogEntries
        {
            get
            {
                return Logger.LogEntries;
            }
        }
      
        public void RegisterProvider(ILogProvider provider)
        {
            provider.Initialize(Logger);
        }
    }
}
