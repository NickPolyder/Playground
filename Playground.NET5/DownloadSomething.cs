using System.Net.Http;
using System.Threading.Tasks;

namespace Playground.NET5
{
	public class DownloadSomething
	{
		public async Task DownloadFile(string fileName, string fileUrl)
		{
			using (var http = new HttpClient())
			{
				var file = await http.GetByteArrayAsync(fileUrl);

				await System.IO.File.WriteAllBytesAsync($"C:\\temp\\{fileName}", file);
			}
		}
	}
}