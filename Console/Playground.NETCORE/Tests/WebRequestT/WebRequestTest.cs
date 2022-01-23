using System;
using System.Net;
using Playground.NETCORE;

namespace Playground.NETCORE.Tests.WebRequestT
{
    public class WebRequestTest : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Web Request Tests";

        /// <inheritdoc />
        public void Run()
        {
            Console.WriteLine("Start ");
            var request = (HttpWebRequest)WebRequest.Create("https://stackoverflow.com");
            request.Timeout = 5 * 60 * 1000;
            request.ReadWriteTimeout = 5 * 60 * 1000;

            request.KeepAlive = true;
            var response = request.GetResponse();
            foreach (var key in response.Headers.AllKeys)
            {
                Console.WriteLine($"Header: {key} --> {response.Headers[key]}");
            }

            Console.WriteLine("Stopping... ");
        }
    }
}