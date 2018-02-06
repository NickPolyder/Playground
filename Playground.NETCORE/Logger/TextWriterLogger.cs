using System;
using System.IO;

namespace Playground.NETCORE.Logger
{
    public class TextWriterLogger : ILog
    {
        private readonly TextWriter _writer;

        public TextWriterLogger() : this(Console.Out)
        { }

        public TextWriterLogger(TextWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }
        /// <inheritdoc />
        public void Debug(string message)
        {
            _writer.WriteLine($"Debug: {message}");
        }

        /// <inheritdoc />
        public void Information(string message)
        {
            _writer.WriteLine($"Info: {message}");
        }

        /// <inheritdoc />
        public void Exception(Exception exception)
        {
            _writer.WriteLine($"Exception: {exception}");
        }
    }
}