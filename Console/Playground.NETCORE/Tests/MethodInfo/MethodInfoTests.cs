using System;

namespace Playground.NETCORE.Tests.MethodInfo
{
    public class MethodInfoTests : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Method Info Tests";

        /// <inheritdoc />
        public void Run()
        {
            Console.WriteLine("Running");
            var executionEngine = new ExecutionEngine();

            executionEngine.Execute();
            Console.WriteLine("Stopping");
        }
    }
}