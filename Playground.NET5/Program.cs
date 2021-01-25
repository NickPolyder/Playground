using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Playground.NET5
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			//var exampleObj = new Example
			//{
			//	Id = 1,
			//	Name = "Layla",
			//	ExampleType =  ExampleTypeEnum.Three
			//};

			//var jsonText = System.Text.Json.JsonSerializer.Serialize(exampleObj);
			//Console.WriteLine(jsonText);
			
			//Console.WriteLine(System.Text.Json.JsonSerializer.Deserialize<Example>(jsonText));

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

			Console.ReadKey();
		}
	}
}
