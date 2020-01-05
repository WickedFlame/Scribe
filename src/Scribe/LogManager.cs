﻿using Scribe.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace Scribe
{
    public class LogManager : ILogManager
    {
        private readonly IList<ILogWriter> _logWriters;
        private readonly IList<IListener> _listeners;

        private bool _isInitialized;
        
        private LogLevel _minimalLogLevel;

        public LogManager()
        {
            _listeners = new List<IListener>();
            _logWriters = new List<ILogWriter>();

            Initialize();
        }
        
        
        
        /// <summary>
        /// Gets the log writers assigned to this manager
        /// </summary>
        public IEnumerable<ILogWriter> Writers => _logWriters;

        /// <summary>
        /// Gets the log listeners assigned to this manager
        /// </summary>
        public IEnumerable<IListener> Listeners =>  _listeners;

        public LogLevel MinimalLogLevel => _minimalLogLevel;

        /// <summary>
        /// Add a log listener to the log manager
        /// </summary>
        /// <param name="listener">The listener</param>
        /// <returns>the logmanager</returns>
        public ILogManager AddListener(IListener listener)
        {
            var listenerType = listener.GetType();
            if (_listeners.Any(l => l.GetType() == listenerType))
            {
                var factory = new LoggerFactory(this);
                factory.GetLogger().Write($"There is already a listener of type {listenerType.Name} contained in the collection.");
                return this;
            }
             
            listener.Initialize(this);

            // keep a reference to the listener
            _listeners.Add(listener);

            return this;
        }
        
        /// <summary>
        /// Add a log writer to the log manager
        /// </summary>
        /// <param name="writer">The log writer</param>
        /// <returns>the logmanager</returns>
        public ILogManager AddWriter(ILogWriter writer)
        {
            var writerType = writer.GetType();
            if (_logWriters.Any(w => w.GetType() == writerType))
            {
                var factory = new LoggerFactory(this);
                factory.GetLogger().Write($"There is already a writer of type {writerType.Name} contained in the collection.");
                return this;
            }

            _logWriters.Add(writer);

            return this;
        }

        public ILogManager SetMinimalLogLevel(LogLevel logLevel)
        {
            _minimalLogLevel = logLevel;

            return this;
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
