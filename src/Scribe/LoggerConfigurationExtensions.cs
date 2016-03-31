﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration AddTraceWriter(this LoggerConfiguration configuration)
        {
            configuration.AddWriter(new TraceLogWriter());

            return configuration;
        }
    }
}
