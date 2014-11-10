using System;

namespace Scribe
{
    

    //public delegate ILogger GetLoggerCallback();

    public class LoggerFactory : ILoggerFactory
    {
        //TODO: Does LoggerCallback have to be static???
        public Func<ILogger> LoggerCallback { get; set; }

        public LoggerFactory()
        {
            //TODO: Does LoggerCallback have to be static???
            if (LoggerCallback == null)
            {
                LoggerCallback = () => new Logger(this);
            }

            _logManager = new Lazy<ILogManager>(() => new LogManager(this));
            if (LogManager.HasConfiguration())
                Manager.Initialize();
        }

        public LoggerFactory(ILogManager manager)
        {
            //TODO: Does LoggerCallback have to be static???
            LoggerCallback = () => new Logger(this);

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
            //TODO: Does LoggerCallback have to be static???
            // if there is an override call that one
            //if (LoggerCallback != null)
            //    return LoggerCallback();

            //return new Logger(this);

            //TODO: Does LoggerCallback have to be static???
            return LoggerCallback();
        }

        public ILogProcessor GetProcessor()
        {
            return Manager.Processor;
        }
    }
}
