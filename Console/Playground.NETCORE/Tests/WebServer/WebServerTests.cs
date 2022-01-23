using System;
using System.Net;

namespace Playground.NETCORE.Tests.WebServer
{
	public class WebServerTests : ITestCase
	{
		public bool Enabled { get; } = true;
		public string Name { get; } = "Web Server Tests";

		private bool _shouldClose = false;
		public void Run()
		{
			var listener = new System.Net.Sockets.TcpListener(IPAddress.Any, 12312);
			listener.Start();
			Console.CancelKeyPress += Console_CancelKeyPress;
			while (!_shouldClose)
			{
				using var tcpClient = listener.AcceptTcpClient();
				using var networkStream = tcpClient.GetStream();

				var buffer = new Span<byte>(new byte[5]);
				
				// Loop to receive all the data sent by the client.
				while(networkStream.Read(buffer) != 0)
				{ 
					// Translate data bytes to a ASCII string.
					var data = System.Text.Encoding.ASCII.GetString(buffer.ToArray());
					Console.WriteLine("Received: {0}", data);
					buffer.Clear();
				}
			}

			//System.Net.WebSockets.
			listener.Stop();
		}

		private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			_shouldClose = true;
		}
	}
}