using System;

namespace Playground.NETCORE.Tests
{
    public class EnviromentTests : ITestCase
    {
        public bool Enabled { get; } = true;
        public string Name { get; } = "Enviroment Testing";

        public void Run()
        {
            Console.WriteLine(Environment.NewLine + $"Hi {Environment.UserName} welcome to my tests" +
                              Environment.NewLine);
            Console.WriteLine("Stacktrace: " + Environment.StackTrace + Environment.NewLine);


        }
    }
}
