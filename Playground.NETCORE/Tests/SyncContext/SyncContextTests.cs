using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.NETCORE.Tests.SyncContext
{
    public class SyncContextTests : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = true;

        /// <inheritdoc />
        public string Name { get; } = "Sync Context";

        /// <inheritdoc />
        public void Run()
        {
            Console.WriteLine($"My thread is: {Thread.CurrentThread.ManagedThreadId}.");

            var syncContext = new SynchronizationContext();
            syncContext.Post(post =>
            {
                Console.WriteLine($"My outer thread is: {Thread.CurrentThread.ManagedThreadId}. Yours is: {post}");
            }, Thread.CurrentThread.ManagedThreadId);


            Task.Run(async () =>
            {
                syncContext.Post(post =>
                {
                    Console.WriteLine($"My level 1 task thread is: {Thread.CurrentThread.ManagedThreadId}. Yours is: {post}");
                }, Thread.CurrentThread.ManagedThreadId);


                await Task.Run(() =>
                {
                    syncContext.Send(post =>
                    {
                        Console.WriteLine($"My level 2 task thread is: {Thread.CurrentThread.ManagedThreadId}. Yours is: {post}");
                    }, Thread.CurrentThread.ManagedThreadId);

                });
            }).Wait();
        }
    }
}