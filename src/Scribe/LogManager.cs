using Scribe.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Scribe
{
    public class LogManager : ILogManager
    {
        bool _isInitialized;

        public LogManager()
        {
            _loggerFactory = new Lazy<LoggerFactory>(() => new LoggerFactory(this));


            _processor = new Lazy<ILogProcessor>(() => new LogProcessor(this));
            _listeners = new Lazy<IList<IListener>>(() => new List<IListener>());
            _logWriters = new Lazy<Dictionary<string, GetLogWriterCallback>>(() => new Dictionary<string, GetLogWriterCallback>());

            Initialize();
        }

        public LogManager(LoggerFactory loggerFactory)
        {
            _loggerFactory = new Lazy<LoggerFactory>(() => loggerFactory);


            _processor = new Lazy<ILogProcessor>(() => new LogProcessor(this));
            _listeners = new Lazy<IList<IListener>>(() => new List<IListener>());
            _logWriters = new Lazy<Dictionary<string, GetLogWriterCallback>>(() => new Dictionary<string, GetLogWriterCallback>());

            Initialize();
        }

        readonly Lazy<LoggerFactory> _loggerFactory;
        public ILoggerFactory LoggerFactory
        {
            get
            {
                return _loggerFactory.Value;
            }
        }

        readonly Lazy<ILogProcessor> _processor;
        public ILogProcessor Processor
        {
            get
            {
                return _processor.Value;
            }
        }

        readonly Lazy<Dictionary<string, GetLogWriterCallback>> _logWriters;
        public Dictionary<string, GetLogWriterCallback> Writers
        {
            get
            {
                return _logWriters.Value;
            }
        }

        readonly Lazy<IList<IListener>> _listeners;
        public IEnumerable<IListener> Listeners
        {
            get
            {
                return _listeners.Value;
            }
        }
        
        public void AddListener(IListener listener)
        {
            listener.Initialize(LoggerFactory);

            // keep a reference to the listener
            //_loggerFactory.Value.AddListener(listener);

            _listeners.Value.Add(listener);
        }

        public void AddLogger(ILogWriter logger, string name = null)
        {
            //_loggerFactory.Value.AddLogger(name ?? logger.GetType().Name, () => logger);
            Writers.Add(name ?? logger.GetType().Name, () => logger);
        }

        public void Initialize()
        {
            if (_isInitialized)
                return;

            var section = ConfigurationManager.GetSection("scribe") as ScribeSection;
            if (section != null)
            {
                foreach (var element in section.Listeners)
                {
                    var type = Type.GetType(element.Type);
                    if (type != null)
                    {
                        var instance = Activator.CreateInstance(type) as IListener;
                        if (instance != null)
                        {
                            AddListener(instance);
                        }
                    }
                    else
                    {
                        var message = string.Format("#### Scribe - Configuration error: Listener {0} cannot be created because the Type does not exist or does not derive from {1}.", element.Type, typeof(IListener).Name);
                        var logger = LoggerFactory.CreateLogger();
                        logger.Write(message, TraceType.Warning, "Configuration", DateTime.Now);
                        Trace.WriteLine(message);
                    }
                }

                foreach (var element in section.Writers)
                {
                    var type = Type.GetType(element.Type);
                    if (type != null)
                    {
                        var instance = Activator.CreateInstance(type) as ILogWriter;
                        if (instance != null)
                        {
                            AddLogger(instance, instance.GetType().Name);
                        }
                    }
                    else
                    {
                        var message = string.Format("#### Scribe - Configuration error: LogWiter {0} cannot be created because the Type does not exist or does not derive from {1}.", element.Type, typeof(ILogWriter).Name);
                        var logger = LoggerFactory.CreateLogger();
                        logger.Write(message, TraceType.Warning, "Configuration", DateTime.Now);
                        Trace.WriteLine(message);
                    }
                }
            }

            _isInitialized = true;
        }

        internal static bool HasConfiguration()
        {
            return ConfigurationManager.GetSection("scribe") as ScribeSection != null;
        }
    }
}
