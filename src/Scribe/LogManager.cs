using Scribe.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Scribe
{
    public class LogManager : ILogManager
    {
        private readonly Lazy<LoggerFactory> _loggerFactory;
        private readonly Lazy<Dictionary<string, GetLogWriterCallback>> _logWriters;
        private readonly Lazy<IList<IListener>> _listeners;

        private bool _isInitialized;
        private Lazy<ILogProcessor> _processor;

        public LogManager()
        {
            _loggerFactory = new Lazy<LoggerFactory>(() => new LoggerFactory(this));


            _processor = new Lazy<ILogProcessor>(() => new AsncLogProcessor(this));
            _listeners = new Lazy<IList<IListener>>(() => new List<IListener>());
            _logWriters = new Lazy<Dictionary<string, GetLogWriterCallback>>(() => new Dictionary<string, GetLogWriterCallback>());

            Initialize();
        }

        public LogManager(LoggerFactory loggerFactory)
        {
            _loggerFactory = new Lazy<LoggerFactory>(() => loggerFactory);


            _processor = new Lazy<ILogProcessor>(() => new AsncLogProcessor(this));
            _listeners = new Lazy<IList<IListener>>(() => new List<IListener>());
            _logWriters = new Lazy<Dictionary<string, GetLogWriterCallback>>(() => new Dictionary<string, GetLogWriterCallback>());

            Initialize();
        }

        public ILoggerFactory LoggerFactory
        {
            get
            {
                return _loggerFactory.Value;
            }
        }

        public ILogProcessor Processor
        {
            get
            {
                return _processor.Value;
            }
        }

        public Dictionary<string, GetLogWriterCallback> Writers
        {
            get
            {
                return _logWriters.Value;
            }
        }

        public IEnumerable<IListener> Listeners
        {
            get
            {
                return _listeners.Value;
            }
        }

        /// <summary>
        /// Set a logprocessor that is used to pass the log entires from the listeners to the writers
        /// </summary>
        /// <param name="processor">The processor</param>
        public void SetProcessor(ILogProcessor processor)
        {
            var reference = processor;
            _processor = new Lazy<ILogProcessor>(() => reference);
        }

        /// <summary>
        /// Add a log listener to the log manager
        /// </summary>
        /// <param name="listener">The listener</param>
        public void AddListener(IListener listener)
        {
            listener.Initialize(LoggerFactory);

            // keep a reference to the listener
            _listeners.Value.Add(listener);
        }

        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        /// <param name="name">The name of the log wirter</param>
        public void AddWriter(ILogWriter writer, string name = null)
        {
            Writers.Add(name ?? writer.GetType().Name, () => writer);
        }

        /// <summary>
        /// Initialize the log manager from the Config files
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            //var section = ConfigurationManager.GetSection("scribe") as ScribeSection;
            //if (section != null)
            //{
            //    foreach (var element in section.Listeners)
            //    {
            //        var type = Type.GetType(element.Type);
            //        if (type != null)
            //        {
            //            var instance = Activator.CreateInstance(type) as IListener;
            //            if (instance != null)
            //            {
            //                AddListener(instance);
            //            }
            //        }
            //        else
            //        {
            //            var message = string.Format("#### Scribe - Configuration error: Listener {0} cannot be created because the Type does not exist or does not derive from {1}.", element.Type, typeof(IListener).Name);
            //            var logger = LoggerFactory.GetLogger();
            //            logger.Write(message, LogLevel.Warning, category: "Configuration", logtime: DateTime.Now);
            //            Trace.WriteLine(message);
            //        }
            //    }

            //    foreach (var element in section.Writers)
            //    {
            //        var type = Type.GetType(element.Type);
            //        if (type != null)
            //        {
            //            var instance = Activator.CreateInstance(type) as ILogWriter;
            //            if (instance != null)
            //            {
            //                AddWriter(instance, instance.GetType().Name);
            //            }
            //        }
            //        else
            //        {
            //            var message = string.Format("#### Scribe - Configuration error: LogWiter {0} cannot be created because the Type does not exist or does not derive from {1}.", element.Type, typeof(ILogWriter).Name);
            //            var logger = LoggerFactory.GetLogger();
            //            logger.Write(message, LogLevel.Warning, category: "Configuration", logtime: DateTime.Now);
            //            Trace.WriteLine(message);
            //        }
            //    }
            //}

            _isInitialized = true;
        }

        internal static bool HasConfiguration()
        {
            return ConfigurationManager.GetSection("scribe") as ScribeSection != null;
        }
    }
}
