namespace Scribe
{
    public static class LoggerConfigurationExtensions
    {
        /// <summary>
        /// Add a Log Writer that Traces to the Output
        /// </summary>
        /// <param name="configuration">The configuration</param>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}</param>
        /// <returns>The configuration</returns>
        public static LoggerConfiguration AddTraceWriter(this LoggerConfiguration configuration, string formatString = "[{LogTime:yyyy-MM-dd HH:mm:SS.fff zzz}] [{LogLevel}] [{Priority}] [{Category}] [{Message}]")
        {
            configuration.AddWriter(new TraceLogWriter(formatString));

            return configuration;
        }

        /// <summary>
        /// Add a Log Writer that Writes to a File
        /// </summary>
        /// <param name="configuration">The configuration</param>
        /// <param name="fileName">The file to write to</param>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}</param>
        /// <returns>The configuration</returns>
        public static LoggerConfiguration AddFileWriter(this LoggerConfiguration configuration, string fileName, string formatString = "[{LogTime:yyyy-MM-dd HH:mm:SS.fff zzz}] [{LogLevel}] [{Priority}] [{Category}] [{Message}]")
        {
            configuration.AddWriter(new FileLogWriter(fileName, formatString));

            return configuration;
        }
    }
}
