using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.NETCORE.Tests.Async
{
    public class AsyncTests : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Async Tests";

        /// <inheritdoc />
        public void Run()
        {
            PrintThread();
            RunAsync().Wait();
            Console.WriteLine("Done!");
        }


        public async Task RunAsync()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            ConsoleKeyInfo keyboard;
            var countTask = CountToWithYielding(10000 * 10000, source.Token);
            do
            {
                Console.WriteLine("Hit Enter to cancel the task or Escape to continue: ");
                keyboard = Console.ReadKey(true);

                if (keyboard.Key == ConsoleKey.Enter)
                {
                    source.Cancel();
                }
            } while (keyboard.Key != ConsoleKey.Escape);
            await countTask;

        }

        public Task CountToWithTaskRun(int count, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                PrintThread();
                int i;
                for (i = 0; i < count; i++)
                {
                    await Task.Delay(1000, token).ContinueWith(tsk => { });
                    if (token.IsCancellationRequested)
                        break;

                }
                Console.WriteLine($"Counted Till: {i}");

            });
        }

        public async Task CountToWithoutTaskRun(int count, CancellationToken token)
        {
            PrintThread();
            int i;
            for (i = 0; i < count; i++)
            {
                await Task.Delay(1000);
                if (token.IsCancellationRequested)
                    break;

            }
            Console.WriteLine($"Counted Till: {i}");

        }

        public async Task CountToWithYielding(int count, CancellationToken token)
        {
            PrintThread();
            int i;
            for (i = 0; i < count; i++)
            {
                await Task.Yield();
                if (token.IsCancellationRequested)
                    break;

            }
            Console.WriteLine($"Counted Till: {i}");

        }


        private static void PrintThread()
        {
            Console.WriteLine($"Using the Thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}