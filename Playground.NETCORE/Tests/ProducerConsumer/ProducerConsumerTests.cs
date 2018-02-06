using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Playground.NETCORE.Logger;

namespace Playground.NETCORE.Tests.ProducerConsumer
{
    public class ProducerConsumerTests : ITestCase
    {
        private const string StartString = "Start";
        private const string CompleteString = "Complete";
        private const string CancelString = "Cancel";
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Producer Consumer Tests";

        private readonly ILog _logger;

        public ProducerConsumerTests()
        {
            Logger.Logger.Instance.SetStrategy(LoggerType.Console);
            _logger = Logger.Logger.Instance;
        }

        /// <inheritdoc />
        public void Run()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "html/text; charset=utf-8");
            CancellationTokenSource source = new CancellationTokenSource();
            var consumer = new ConsumerWithWaitHandle();
            consumer.AddItem(async () =>
            {
                _logger.Information("Before delay");
                await Task.Delay(5000);
                _logger.Information("After delay");
            });

            do
            {
                _logger.Information("Write your text.\n" +
                                  $"'{StartString}' to close the consumer.\n" +
                                  $"'{CompleteString}' to close the consumer.\n" +
                                  $"'{CancelString}' to cancel the token.");
                var read = Console.ReadLine();
                switch (read)
                {
                    case StartString:
                        consumer.Start(source.Token);
                        break;
                    case CompleteString:
                        consumer.Complete();
                        break;
                    case CancelString:
                        break;
                    case "1":
                        AddConsumerItem("https://www.google.com", httpClient, consumer);
                        break;
                    case "2":
                        AddConsumerItem("https://www.facebook.com", httpClient, consumer);
                        break;
                    default:
                        consumer.AddItem(() => _logger.Information("I Produced: " + read));
                        break;
                }
                _logger.Information("Escape for exit.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            source.Cancel();
        }

        private static void AddConsumerItem(string url, HttpClient httpClient, IConsumer consumer)
        {
            consumer.AddItem(async () =>
            {
                var result = await httpClient.GetAsync(url);

                Console.WriteLine(result.ToString());
            });
        }
    }
}