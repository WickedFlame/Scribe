using System;

namespace Scribe
{
    public delegate ILogWriter GetLogWriterCallback();

    public class LoggerFactory : ILoggerFactory
    {
        //readonly Lazy<ILogProcessor> _processor;

        public LoggerFactory()
        {
            //_processor = new Lazy<ILogProcessor>(() => new LogProcessor(this));
            //_listeners = new Lazy<IList<IListener>>(() => new List<IListener>());
            //_logProviders = new Lazy<Dictionary<string, CreateLoggerCallback>>(() => new Dictionary<string, CreateLoggerCallback>());

            _logManager = new Lazy<ILogManager>(() => new LogManager());
            if (LogManager.HasConfiguration())
                Manager.Initialize();
        }

        public LoggerFactory(ILogManager manager)
        {
            //_processor = new Lazy<ILogProcessor>(() => new LogProcessor(this));
            //_listeners = new Lazy<IList<IListener>>(() => new List<IListener>());
            //_logProviders = new Lazy<Dictionary<string, CreateLoggerCallback>>(() => new Dictionary<string, CreateLoggerCallback>());

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
      
		
        //public ILogProcessor Processor
        //{
        //    get
        //    {
        //        return Manager.Processor;
        //    }
        //}
      
		


        //readonly Lazy<Dictionary<string, CreateLoggerCallback>> _logProviders;
        //public Dictionary<string, CreateLoggerCallback> LogProviders
        //{
        //    get
        //    {
        //        return _logProviders.Value;
        //    }
        //}

        //readonly Lazy<IList<IListener>> _listeners;
        //public IEnumerable<IListener> Listeners
        //{
        //    get
        //    {
        //        return _listeners.Value;
        //    }
        //}

        //public void AddListener(IListener listener)
        //{
        //    _listeners.Value.Add(listener);
        //}

        //public void AddLogger(string name, CreateLoggerCallback loggerProvider)
        //{
        //    if (!LogProviders.ContainsKey(name))
        //    {
        //        LogProviders.Add(name, loggerProvider);
        //    }
        //}

        public ILogger CreateLogger()
        {
            return new Logger(this);
        }

        public ILogProcessor GetProcessor()
        {
            //return _processor.Value;
            return Manager.Processor;
        }
    }
}
