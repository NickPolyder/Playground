using System;

namespace Playground.NETCORE.Tests.DispatchProxy
{
    public class DispatchProxyTests : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Dispatch Proxy Test";

        /// <inheritdoc />
        public void Run()
        {
            var dispatchInfo = new DispatchInformation
            {
                InformationType = "Dispatching Info's",
                Title = "Heyaaaaaa",
                Description = "Im trying to watch SPN"
            };
            var proxy = ExceptionDispatch<IDispatchInformation>.Create(dispatchInfo);
            proxy.Display();
            proxy.DisplayThrowException();

            var objectWatch = System.Diagnostics.Stopwatch.StartNew();
            dispatchInfo.Display();
            objectWatch.Stop();

            var proxyWatch = System.Diagnostics.Stopwatch.StartNew();
            proxy.Display();
            proxyWatch.Stop();

            Console.WriteLine($"Object: {objectWatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Proxy: {proxyWatch.ElapsedMilliseconds} ms");

        }
    }
}