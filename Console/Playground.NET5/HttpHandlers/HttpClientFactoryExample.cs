using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Playground.NET.HttpHandlers
{
	public class HttpClientFactoryExample
	{
		public static async Task Run()
		{

			var containerBuilder = new ServiceCollection();
			containerBuilder.AddLogging(builder =>
			{
				builder.AddConsole();
			});

			containerBuilder.AddTransient<LoggingHttpHandler>();

			containerBuilder.AddHttpClient("loggingClient")
				.AddHttpMessageHandler<LoggingHttpHandler>();

			using var container = containerBuilder.BuildServiceProvider();
		
			try
			{
				var httpClientFactory = container.GetRequiredService<IHttpClientFactory>();

				using var httpClient = httpClientFactory.CreateClient("loggingClient");

				Console.Write("Get FileName: ");
				var fileName = Console.ReadLine();

				Console.Write("Get Url: ");
				var url = Console.ReadLine();

				var file = await httpClient.GetByteArrayAsync(url);

				await System.IO.File.WriteAllBytesAsync($"C:\\temp\\{fileName}", file);

				Console.WriteLine($"C:\\temp\\{fileName}");
			}
			catch (Exception ex)
			{
				await Console.Error.WriteLineAsync(ex.Message);
				await Console.Error.WriteLineAsync(ex.StackTrace);

			}

		}
	}
}