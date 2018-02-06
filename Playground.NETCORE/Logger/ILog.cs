using System;

namespace Playground.NETCORE.Logger
{
    public interface ILog
    {
        void Debug(string message);

        void Information(string message);

        void Exception(Exception exception);
    }
}