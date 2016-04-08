using Scribe.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Scribe
{
    public class LogManager : ILogManager
    {
        private readonly LoggerFactory _loggerFactory;
        private readonly IList<ILogWriter> _logWriters;
        private readonly IList<IListener> _listeners;

        private bool _isInitialized;
        private ILogProcessor _processor;

        public LogManager()
        {
            _loggerFactory = new LoggerFactory(this);


            _processor = new AsncLogProcessor(this);
            _listeners = new List<IListener>();
            _logWriters = new List<ILogWriter>();

            Initialize();
        }

        public LogManager(LoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;


            _processor = new AsncLogProcessor(this);
            _listeners = new List<IListener>();
            _logWriters = new List<ILogWriter>();

            Initialize();
        }

        /// <summary>
        /// Gets the ILoggerFactory associated with this manager
        /// </summary>
        public ILoggerFactory LoggerFactory => _loggerFactory;

        /// <summary>
        /// Gets the ILogProcessor associated with this manager
        /// </summary>
        public ILogProcessor Processor => _processor;
        
        /// <summary>
        /// Gets the log writers assigned to this manager
        /// </summary>
        public IEnumerable<ILogWriter> Writers => _logWriters;

        /// <summary>
        /// Gets the log listeners assigned to this manager
        /// </summary>
        public IEnumerable<IListener> Listeners =>  _listeners;

        /// <summary>
        /// Set a logprocessor that is used to pass the log entires from the listeners to the writers
        /// </summary>
        /// <param name="processor">The processor</param>
        public void SetProcessor(ILogProcessor processor)
        {
            _processor = processor;

            // reinitialize the processor to use the correct manager
            processor.Initialize(this);
        }

        /// <summary>
        /// Add a log listener to the log manager
        /// </summary>
        /// <param name="listener">The listener</param>
        public void AddListener(IListener listener)
        {
            listener.Initialize(LoggerFactory);

            // keep a reference to the listener
            _listeners.Add(listener);
        }
        
        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        public void AddWriter(ILogWriter writer)
        {
            _logWriters.Add(writer);
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
                        var message = $"#### Scribe - Configuration error:\nListener {element.Type} cannot be created because the Type does not exist or does not derive from {typeof(IListener).Name}.";
                        //var logger = LoggerFactory.GetLogger();
                        //logger.Write(message, LogLevel.Warning, category: "Configuration", logtime: DateTime.Now);
                        Trace.WriteLine(message);

                        throw new Exception(message);
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
                            AddWriter(instance);
                        }
                    }
                    else
                    {
                        var message = $"#### Scribe - Configuration error:\nLogWiter {element.Type} cannot be created because the Type does not exist or does not derive from {typeof(ILogWriter).Name}.";
                        //var logger = LoggerFactory.GetLogger();
                        //logger.Write(message, LogLevel.Warning, category: "Configuration", logtime: DateTime.Now);
                        Trace.WriteLine(message);

                        throw new Exception(message);
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
