using System;
using System.Threading.Tasks;
using Playground.NET.HttpHandlers;

namespace Playground.NET
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			//await HttpClientFactoryExample.Run();

            string? value = null;

            
            var position = Console.GetCursorPosition();
            Console.WriteLine("Hello There");
            var originalText = "Remaining Time: 35";

            Console.WriteLine(originalText);
            
            await Task.Delay(TimeSpan.FromSeconds(5));
			Console.SetCursorPosition(position.Left, position.Top);
            Console.WriteLine("Who Are you ?");

            var remainingTime = "Remaining Time: 5";
            
            Console.WriteLine(remainingTime + new string(' ', originalText.Length - (remainingTime.Length-1)));

            Console.ReadKey();
		}
	}

}
