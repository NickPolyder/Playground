using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.NETFRAMEWORK.Tests
{
    public static class Proccesses
    {
        public static void ProccessStatus()
        {
            var proccess = System.Diagnostics.Process.GetCurrentProcess();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Proccess Status: {proccess.Id.ToString()} {proccess.ProcessName}");
            Console.WriteLine($"Proccess Private Memory Size: {proccess.PrivateMemorySize64.ToString()}");
            Console.WriteLine($"Proccess Virtual Memory Size: {proccess.VirtualMemorySize64.ToString()}");
            Console.WriteLine("End Proccess");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void ThreadThrowsException()
        {
            Console.WriteLine("About to create and start the new thread");
            var thread = new Thread(ThrowException) { IsBackground = true };
            thread.Start();
            Console.WriteLine("Thread Started");
        }

        public static void ThreadPoolThrowsException()
        {
            Console.WriteLine("About to initialize the threadpool queue");
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    ThrowException();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            Console.WriteLine("ThreadPool Passed");
        }


        private static void ThrowException()
        {
            Console.WriteLine("At Throw Exception");
            throw new Exception("Wanted to test this");
        }
    }
}
