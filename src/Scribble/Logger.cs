using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribble
{
    public class Logger
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


        //static Logger _instance;
        //public static Logger Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new Logger();
        //        return _instance;
        //    }
        //}

        public void Info(string message)
        {
            LogEntries.Add(new LogEntry(message, "Info", null));
        }
      
        public void Log(string message, string category = null, DateTime? logtime = null)
        {
            LogEntries.Add(new LogEntry(message, category, logtime));
        }
    }
}
