using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe.Configuration
{
    public static class LogManagerExtensions
    {
        /// <summary>
        /// Initialize the log manager from the Config files
        /// </summary>
        public static void Initialize(this ILogManager manager)
        {
            var section = ConfigurationManager.GetSection("scribe") as ScribeSection;
            if (section != null)
            {
                foreach (var element in section.Writers)
                {
                    var type = Type.GetType(element.Type);
                    if (type != null)
                    {
                        var instance = Activator.CreateInstance(type) as ILogWriter;
                        if (instance != null)
                        {
                            manager.AddWriter(instance);
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
        }

        internal static bool HasConfiguration()
        {
            return ConfigurationManager.GetSection("scribe") as ScribeSection != null;
        }
    }
}
