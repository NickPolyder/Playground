using System;

namespace Playground.NETCORE.Logger
{
    public interface ILog
    {
        void Log(string state, string message);

        void Debug(string message);

        void Information(string message);

        void Exception(Exception exception);
    }
}