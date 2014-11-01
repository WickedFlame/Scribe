using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe
{
    public class LogManager
    {

        ILoggerFactory _loggerFactory;
        public ILoggerFactory LoggerFactory
        {
            get
            {
                if (_loggerFactory == null)
                    _loggerFactory = new LoggerFactory();
                return _loggerFactory;
            }
        }

        //public IEnumerable<LogEntry> LogEntries
        //{
        //    get
        //    {
        //        return LoggerFactory.LogQueue.LogEntries;
        //    }
        //}
      
        public void AddListner(IListner listner)
        {
            listner.Initialize(LoggerFactory);
        }

        public void AddLogger(ILogger logger, string name = null)
        {
            LoggerFactory.AddLogger(name ?? logger.GetType().Name, () => logger);
        }
    }
}
