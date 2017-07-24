using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
