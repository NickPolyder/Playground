using System;
using System.Collections.Generic;

namespace Playground.NETCORE.Logger
{
    public enum LoggerType { Debug, Console }
    public class Logger : ILog
    {
        private static volatile Logger _instance;
        public static Logger Instance => _instance ?? (_instance = new Logger());

        private readonly Dictionary<LoggerType, Lazy<ILog>> _loggers;
        private LoggerType _currentStrategy;
        public Logger() : this(GetDefaultLoggers())
        { }

        public Logger(Dictionary<LoggerType, Lazy<ILog>> loggers) : this(loggers, LoggerType.Console)
        { }

        public Logger(Dictionary<LoggerType, Lazy<ILog>> loggers, LoggerType strategy)
        {
            _loggers = loggers;
            _currentStrategy = strategy;
        }

        public void SetStrategy(LoggerType type)
        {
            _currentStrategy = type;
        }

        /// <inheritdoc />
        public void Debug(string message)
        {
            CurrentLogger?.Debug(message);
        }

        /// <inheritdoc />
        public void Information(string message)
        {
            CurrentLogger?.Information(message);
        }

        /// <inheritdoc />
        public void Exception(Exception exception)
        {
            CurrentLogger?.Exception(exception);
        }

        private ILog CurrentLogger =>
            _loggers.ContainsKey(_currentStrategy) ? _loggers[_currentStrategy].Value
                :
                throw new NotImplementedException($"{_currentStrategy} not implemented");

        private static Dictionary<LoggerType, Lazy<ILog>> GetDefaultLoggers()
        {
            return new Dictionary<LoggerType, Lazy<ILog>>
            {
                {LoggerType.Debug, new Lazy<ILog>(()=> new TextWriterLogger(new TextWriterDebug())) },
                {LoggerType.Console, new Lazy<ILog>(()=> new TextWriterLogger(Console.Out)) },
            };
        }
    }
}