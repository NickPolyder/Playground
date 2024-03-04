using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Playground.NET
{
	public class DownloadSomething
	{
		public async Task DownloadFile(string fileName, string fileUrl)
		{
			using var http = new HttpClient();
			var file = await http.GetByteArrayAsync(fileUrl);

			await System.IO.File.WriteAllBytesAsync($"C:\\temp\\{fileName}", file);
		}

		public async Task Run()
		{
			Console.Write("Get FileName: ");
			var fileName = Console.ReadLine();

			Console.Write("Get Url: ");
			var url = Console.ReadLine();

			try
			{
				await new DownloadSomething().DownloadFile(fileName, url);

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