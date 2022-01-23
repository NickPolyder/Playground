using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Playground.NETCORE.Logger
{
    public enum LoggerType { Debug, Console }
    public class Logger : ILog, ILogger
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
        public void Log(string state, string message)
        {
            CurrentLogger?.Log(state, message);
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

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Func<TState, Exception, string> form = formatter ?? ((st, ex) => $"{st}: {ex}");
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Information:
                case LogLevel.Warning:
                case LogLevel.Error:
                case LogLevel.Critical:
                    Log(logLevel.ToString(), form(state, exception));
                    break;
                case LogLevel.None:
                    Information(form(state, exception));
                    break;
            }

        }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <inheritdoc />
        public IDisposable BeginScope<TState>(TState state)
        {
            return new LoggerScope<TState>(state);
        }
    }


    public sealed class LoggerScope<TState> : IDisposable
    {
        private TState _state;
        public LoggerScope(TState state)
        {
            _state = state;

            Console.WriteLine($"Start state: {state}");
        }
        /// <inheritdoc />
        public void Dispose()
        {

            Console.WriteLine($"End state: {_state}");

            _state = default(TState);
        }
    }
}