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
            _logger.Debug($"Main Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            CancellationTokenSource source = new CancellationTokenSource();
            var consumer = new TaskConsumer(source.Token);
            consumer.QueueItem(async () =>
            {
                _logger.Information("Before delay");
                await Task.Delay(5000);
                _logger.Information("After delay");
            });

            do
            {
                _logger.Information("Write your text.\n" +
                                  $"'{StartString}' to Start the consumer.\n" +
                                   "'1' to send a get request to google.com \n" +
                                   "'2' to send a get request to facebook.com \n" +
                                   "'3' to send a get request to instagram.com \n" +
                                  $"'{CompleteString}' to close the consumer.\n" +
                                  $"'{CancelString}' to cancel the token.");
                var read = Console.ReadLine();
                switch (read)
                {
                    case StartString:
                        consumer.Start();
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

                        _logger.Debug("Before facebook call");

                        var facebookTask = AddConsumerItem("https://www.facebook.com", httpClient, consumer);

                        facebookTask.GetAwaiter().GetResult();

                        _logger.Debug("After facebook call");
                        _logger.Debug($"Facebook Task Status: {facebookTask.Status}, {facebookTask.Id}");
                        break;
                    case "3":

                        _logger.Debug("Before instagram call");

                        var instagramTask = consumer.QueueItem(async () =>
                        {
                            var result = await httpClient.GetAsync("https://www.instagram.com");
                            Logger.Logger.Instance.Information(result.RequestMessage.RequestUri.ToString());
                            Logger.Logger.Instance.Information(result.ToString());
                            return result;
                        });

                        var objResult = instagramTask.GetAwaiter().GetResult() as HttpResponseMessage;

                        _logger.Debug("After instagram call");
                        _logger.Debug($"Instagram Task Status: {instagramTask.Status}, {instagramTask.Id}");
                        _logger.Debug($"Object Result: {objResult?.RequestMessage.Method}: {objResult?.RequestMessage.RequestUri} STATUS {objResult?.StatusCode}, ");
                        break;
                    default:
                        consumer.QueueItem(() => _logger.Information("I Produced: " + read));
                        break;
                }
                _logger.Information("Escape for exit.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            source.Cancel();
        }

        private static Task AddConsumerItem(string url, HttpClient httpClient, TaskConsumer consumer)
        {
            return consumer.QueueItem(async () =>
            {
                var result = await httpClient.GetAsync(url);
                Logger.Logger.Instance.Information(result.RequestMessage.RequestUri.ToString());
                Logger.Logger.Instance.Information(result.ToString());
            });
        }
    }
}