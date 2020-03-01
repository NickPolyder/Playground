using System;
using System.Net;
using System.Net.Sockets;

namespace Playground.NETFRAMEWORK.Tests
{
	public class WebClientTests
	{
		public static void Run()
		{
			Console.WriteLine("Start Web Client Tests");
			
			bool close = false;
			Console.CancelKeyPress += (sender, args) => close = true;
			
			while (!close)
			{
				using var client = new TcpClient("127.0.0.1", 12312);
				Console.WriteLine("Write a message:");
				var message = Console.ReadLine();
				Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

				using NetworkStream stream = client.GetStream();

				// Send the message to the connected TcpServer. 
				stream.Write(data, 0, data.Length);

				Console.WriteLine("Sent: {0}", message);
			}
			// Translate the passed message into ASCII and store it as a Byte array.
			

            Console.WriteLine("Stopping... Web Client Tests");
		}
    }
}