using Microsoft.Extensions.Logging;

namespace NeurotestServer.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        public FileLoggerProvider(string fileName)
        {
            m_FileName = fileName;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(m_FileName);
        }
        public void Dispose()
        {}

        private readonly string m_FileName;
    }
}
