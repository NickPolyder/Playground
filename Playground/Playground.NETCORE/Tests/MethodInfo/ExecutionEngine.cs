using System;

namespace Playground.NETCORE.Tests.MethodInfo
{
    public class ExecutionEngine
    {
        public void Execute()
        {
            Console.WriteLine();
            var methodInfo = System.Reflection.MethodInfo.GetCurrentMethod();
            Console.WriteLine($"Hey i am in: {methodInfo.Name}");
            Console.WriteLine();
        }
    }
}