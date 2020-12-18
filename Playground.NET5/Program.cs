using System;
using System.Text.Json.Serialization;

namespace Playground.NET5
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var exampleObj = new Example
			{
				Id = 1,
				Name = "Layla",
				ExampleType =  ExampleTypeEnum.Three
			};

			var jsonText = System.Text.Json.JsonSerializer.Serialize(exampleObj);
			Console.WriteLine(jsonText);
			
			Console.WriteLine(System.Text.Json.JsonSerializer.Deserialize<Example>(jsonText));

			Console.ReadKey();
		}
	}
}
