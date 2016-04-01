using Scribe.Format;
using System;
using System.IO;

namespace Scribe
{
    public class FileLogWriter : ILogWriter, IDisposable
    {
        private readonly LogEntryFormatProvider _formatProvider;
        private readonly string _fileName;
        private StreamWriter _fileStream;

        /// <summary>
        /// Creates a new instance of a FileLogWriter
        /// </summary>
        /// <param name="fileName">The file to write to</param>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}</param>
        public FileLogWriter(string fileName, string formatString = "[{LogTime:yyyy-MM-dd HH:mm:SS.fff zzz}] [{LogLevel}] [{Priority}] [{Category}] [{Message}]")
        {
            _formatProvider = new LogEntryFormatProvider(formatString);
            _fileName = fileName;

            var directory = Path.GetDirectoryName(_fileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fileName));
            }
        }

        public void Write(ILogEntry logEntry)
        {
            if (_fileStream == null)
            {
                // Write the string to a file.
                _fileStream = new StreamWriter(_fileName);
            }

            // message, LogLevel.Information, category: messageType.ToString(), logtime: DateTime.Now
            _fileStream.WriteLine(_formatProvider.Format(logEntry));
            _fileStream.Flush();
        }

        public void Dispose()
        {
            if (_fileStream != null)
            {
                _fileStream.Close();
            }
        }
    }
}
