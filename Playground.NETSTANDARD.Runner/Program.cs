using System;
using System.Linq;

namespace Playground.NETSTANDARD.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maxLines = 100;
            var seperators = string.Join("", Enumerable.Repeat("-", maxLines));
            Console.WriteLine("Starting Tests");
            Console.WriteLine(seperators + Environment.NewLine);
            var assemblyTypes = typeof(ITestCase).Assembly.GetTypes();
            var tests = assemblyTypes.Where(tt => !tt.IsInterface && typeof(ITestCase).IsAssignableFrom(tt)).ToList();
            var count = 1;
            var testsCount = tests.Count;
            foreach (var test in tests)
            {
                var instance = _createInstance(test);
                if (instance == null || !instance.Enabled) continue;
                var testofMax = $"({count++} / {testsCount})";
                var getLines = Math.Abs(maxLines - instance.Name.Length - testofMax.Length - 1) / 2;
                var lines = string.Join("", Enumerable.Repeat("-", getLines));
                Console.WriteLine($"{lines} {instance.Name} {testofMax} {lines} {Environment.NewLine}");

                instance.Run();

                Console.WriteLine(Environment.NewLine + seperators + Environment.NewLine);
            }

            Console.WriteLine(Environment.NewLine + seperators + Environment.NewLine);
            Console.WriteLine("Hit Any key.");
            Console.ReadKey();
        }

        private static ITestCase _createInstance(Type type)
        {
            var instance = Activator.CreateInstance(type);
            return instance as ITestCase;
        }
    }
}
