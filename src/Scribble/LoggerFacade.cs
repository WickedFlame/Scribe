using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribble
{
    public class LoggerFacade
    {
        IList<LogEntry> _logEntries;
        public IList<LogEntry> LogEntries
        {
            get
            {
                if (_logEntries == null)
                    _logEntries = new List<LogEntry>();
                return _logEntries;
            }
        }


        static LoggerFacade _instance;
        public static LoggerFacade Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoggerFacade();
                return _instance;
            }
        }
      
		
      
        public void Log(string message, string category = null, DateTime? logtime = null)
        {
            LogEntries.Add(new LogEntry(message, category, logtime));
        }
    }
}
