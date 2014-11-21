using System;

namespace Scribe
{
    

    //public delegate ILogger GetLoggerCallback();

    public class LoggerFactory : ILoggerFactory
    {
        //TODO: Does LoggerCallback have to be static???
        public static Func<ILogger> LoggerCallback { get; set; }

        public LoggerFactory()
        {
            _logManager = new Lazy<ILogManager>(() => new LogManager(this));
            if (LogManager.HasConfiguration())
                Manager.Initialize();
        }

        public LoggerFactory(ILogManager manager)
        {
            //TODO: Does LoggerCallback have to be static???
            //LoggerCallback = () => new Logger(this);

            _logManager = new Lazy<ILogManager>(() => manager);
        }

        readonly Lazy<ILogManager> _logManager;
        public ILogManager Manager
        {
            get
            {
                return _logManager.Value;
            }
        }
      
        public ILogger CreateLogger()
        {
            // if there is an override call that one
            if (LoggerCallback != null)
                return LoggerCallback();

            // return a default logger
            return new Logger(this);
        }

        public ILogProcessor GetProcessor()
        {
            return Manager.Processor;
        }
    }
}
