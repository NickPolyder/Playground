using System;
using Microsoft.Extensions.Logging;

namespace Playground.NETCORE.Logger
{
    public sealed class LoggerFactory : ILoggerFactory
    {
        /// <inheritdoc />
        public void Dispose()
        {

        }

        /// <inheritdoc />
        public ILogger CreateLogger(string categoryName)
        {
            return Logger.Instance;
        }

        /// <inheritdoc />
        public void AddProvider(ILoggerProvider provider)
        {
            Console.WriteLine(provider);

        }
    }
}