using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace NeurotestServer.Logging
{
    public class FileLogger : ILogger
    {
        public FileLogger(string filename)
        {
            Debug.Assert(Path.GetExtension(filename) == ".txt",
                $"Got unexpected file extension: {Path.GetExtension(filename)}.");
            m_FileName = filename;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter is not null)
            {
                lock (m_Lock)
                {
                    string path = Path.Combine(m_LoggingDir, m_FileName);
                    string message = $"[{DateTime.Now}] " + formatter(state, exception) + "\n";
                    File.AppendAllText(path, message);
                }
            }
        }

        private static object m_Lock = new object();
        private static string m_LoggingDir = Path.Combine(Directory.GetCurrentDirectory(), "Logging");
        private readonly string m_FileName;  // Name of the file for logging
    }
}
