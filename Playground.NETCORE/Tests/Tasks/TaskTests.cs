using System;
using System.Threading.Tasks;

namespace Playground.NETCORE.Tests.Tasks
{
    public class TaskTests : ITestCase
    {
        /// <inheritdoc />
        public bool Enabled { get; } = false;

        /// <inheritdoc />
        public string Name { get; } = "Task Tests";


        /// <inheritdoc />
        public void Run()
        {
            PrintThread("On run");

            Task.Run(() => PrintThread("Inside Task.Run")).ContinueWith(tsk => PrintThread("Continue after Task.Run"));

            var task = new Task(() => PrintThread("Inside new Task()"));
            task.ContinueWith(tsk => PrintThread("Continue after new Task()"));
            Console.WriteLine("After creation of task var.");
            task.Start();
            PrintThread("After  task.Start()");
            TestAsyncMethod().ContinueWith(tsk => PrintThread("Continue after TestAsyncMethod"));

        }

        private void PrintThread(string category)
        {
            Console.WriteLine($"{category}: Thread Id: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }

        private async Task TestAsyncMethod()
        {
            await Task.Delay(1);
            PrintThread("Inside  TestAsyncMethod");
            await Task.Delay(1);
        }
    }
}