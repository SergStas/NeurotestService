using Microsoft.Extensions.Logging;

namespace NeurotestServer.Logging
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string fileName)
        {
            factory.AddProvider(new FileLoggerProvider(fileName));
            return factory;
        }
    }
}
