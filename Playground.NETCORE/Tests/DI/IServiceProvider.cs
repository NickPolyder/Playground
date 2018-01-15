using System;
namespace Playground.NETCORE.Tests.DI
{
    public interface IServiceInjector : IServiceProvider
    {
        TType GetService<TType>();
    }
}