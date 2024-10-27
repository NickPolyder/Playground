using System;
using System.Net;

namespace Playground.NETFRAMEWORK.Tests
{
    public class WebRequestTest
    {
        public static void Run()
        {
            Console.WriteLine("Start Web Request Tests");
            var request = (HttpWebRequest)WebRequest.Create("https://stackoverflow.com");
            request.Timeout = 5 * 60 * 1000;
            request.ReadWriteTimeout = 5 * 60 * 1000;

            request.ServicePoint.SetTcpKeepAlive(true, 5 * 1000, 1000);

            var response = request.GetResponse();
            Console.WriteLine($"Max Idle Time --> {request.ServicePoint.MaxIdleTime}");
            foreach (var key in response.Headers.AllKeys)
            {
                Console.WriteLine($"Header: {key} --> {response.Headers[key]}");
            }

            Console.WriteLine("Stopping... Web Request Tests");
        }
    }
}