using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Playground.NET5.HttpHandlers;

namespace Playground.NET5
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			await HttpClientFactoryExample.Run();

			Console.ReadKey();
		}
	}
}
