using System;
using System.Diagnostics;

namespace Playground.NETCORE.Tests.MethodInfo
{
    public class ExecutionEngine
    {
        public void Execute()
        {
            Console.WriteLine();
            var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
            Console.WriteLine($"Hey i am in: {methodInfo.Name}");
            Console.WriteLine($"The Module Name is: {methodInfo.Module.Name}");
            Debugger.Log(2, "Important!", "Im here you noob!");
            var frame = new StackFrame(1);
            Console.WriteLine($"Caller: {frame.GetMethod().Name}");
            var callersFrame = new StackFrame(2);
            Console.WriteLine($"Caller's Caller: {callersFrame?.GetMethod().Name}");
            Console.WriteLine();
        }
    }
}