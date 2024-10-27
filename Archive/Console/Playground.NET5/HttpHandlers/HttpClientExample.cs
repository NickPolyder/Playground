using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Playground.NET.HttpHandlers
{
	public class HttpClientExample
	{
		public static async Task Run()
		{
			using var loggerFactory = LoggerFactory.Create(builder =>
			{
				builder.AddConsole();
			});

			var logger = loggerFactory.CreateLogger<LoggingHttpHandler>();

			try
			{
				using (var httpClient = new HttpClient(new LoggingHttpHandler(logger, new HttpClientHandler()), true))
				{

					Console.Write("Get FileName: ");
					var fileName = Console.ReadLine();

					Console.Write("Get Url: ");
					var url = Console.ReadLine();
					
					var file = await httpClient.GetByteArrayAsync(url);

					await System.IO.File.WriteAllBytesAsync($"C:\\temp\\{fileName}", file);

					Console.WriteLine($"C:\\temp\\{fileName}");

				}
			}
			catch (Exception ex)
			{
				await Console.Error.WriteLineAsync(ex.Message);
				await Console.Error.WriteLineAsync(ex.StackTrace);

			}
			
		}
	}
}